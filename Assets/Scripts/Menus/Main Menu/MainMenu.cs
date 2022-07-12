using System.Collections;
using System.Linq;
using Audio;
using Core;
using Core.MapData;
using JetBrains.Annotations;
using Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace Menus.Main_Menu {
    public class MainMenu : MonoBehaviour {
        // Animating the ship
        [SerializeField] private GameObject shipMesh;

        // VR handling
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera flatScreenCamera;
        [SerializeField] private Camera uiCamera;
        [SerializeField] private XRRig xrRig;

        [SerializeField] private VRCalibrationDialog vrCalibrationDialog;
        [SerializeField] private NewPlayerWelcomeDialog newPlayerWelcomeDialog;
        [SerializeField] private TitleMenu titleMenu;
        [SerializeField] private TopMenu topMenu;
        [SerializeField] private ProfileMenu profileMenu;
        [SerializeField] private DisconnectionDialog disconnectionDialog;

        private bool _shouldAnimate;

        private static bool FirstRun => Game.Instance.MenuFirstRun;

        private void Start() {
            topMenu.Hide();
            titleMenu.Hide();

            // use this for testing bonkers string conversion issues
            // Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            var lastPlayedVersion = Preferences.Instance.GetString("lastPlayedVersion");
            topMenu.SetPatchNotesUpdated(lastPlayedVersion != Application.version);

            if (FirstRun) shipMesh.transform.position += new Vector3(1.58f, 1.32f, -6.8f);
        }

        private void FixedUpdate() {
            if (_shouldAnimate) {
                // move the ship around a fixed space
                var positionX = MathfExtensions.Oscillate(-0.01f, 0.01f, 8);
                var positionY = MathfExtensions.Oscillate(-0.01f, 0.01f, 12);
                var positionZ = MathfExtensions.Oscillate(0.01f, -0.01f, 5);
                shipMesh.transform.position += new Vector3(positionX, positionY, positionZ);

                // gently rock the ship mesh back and forth
                var rotationAmount = MathfExtensions.Oscillate(-0.12f, 0.12f, 8, 4);
                shipMesh.transform.Rotate(Vector3.forward, rotationAmount);

                // On first run wait for intro song to play then rotate the camera and move the ship into position slowly
                if (Time.time > 8f || !FirstRun)
                    flatScreenCamera.transform.RotateAround(new Vector3(0, 0, -6.5f), Vector3.up, -0.1f);
                if (Time.time < 9f)
                    shipMesh.transform.position += new Vector3(-0.00351f, -0.00293f, 0.0151f); // starting values / number of frames 
            }
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += OnEnvironmentLoadComplete;
            Game.OnVRStatus += OnVRStatus;
            Game.OnGameSettingsApplied += OnGameSettingsApplied;
            InputSystem.onDeviceChange += OnDeviceChange;
            StartCoroutine(MenuLoad());
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnEnvironmentLoadComplete;
            Game.OnVRStatus -= OnVRStatus;
            Game.OnGameSettingsApplied -= OnGameSettingsApplied;
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        public void ShowDisconnectedDialog(string reason) {
            topMenu.gameObject.SetActive(false);
            disconnectionDialog.Open(topMenu);
            disconnectionDialog.Reason = reason;
        }

        // TODO: show time trial with previous level selected

        public void ShowLobby() {
            var lobby = FindObjectOfType<LobbyMenu>(true);
            if (lobby) {
                topMenu.Hide();
                lobby.Open(topMenu);
            }
            else {
                Debug.LogWarning("Failed to find lobby!");
            }
        }

        [UsedImplicitly]
        public void OnResetHMDView(InputValue inputValue) {
            if (xrRig) Game.Instance.ResetHmdView(xrRig, xrRig.transform.parent);
        }

        private void OnVRStatus(bool isVREnabled) {
            // if VR is enabled, we need to swap our active cameras and make UI panels operate in world space
            if (isVREnabled) {
                canvas.renderMode = RenderMode.WorldSpace;
                var canvasRect = canvas.GetComponent<RectTransform>();
                if (canvasRect) {
                    canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1920);
                    canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1080);
                }

                canvas.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                flatScreenCamera.enabled = false;
                uiCamera.enabled = false;
                xrRig.gameObject.SetActive(true);
            }
            else {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                flatScreenCamera.enabled = true;
                uiCamera.enabled = true;
                xrRig.gameObject.SetActive(false);
            }
        }

        private void OnGameSettingsApplied() {
            var playerInput = GetComponent<PlayerInput>();
            playerInput.currentActionMap = playerInput.actions
                .FindActionMap(Preferences.Instance.GetString("controlSchemeType") == "arcade" ? "GlobalArcade" : "Global");
        }

        private IEnumerator MenuLoad() {
            // input jank - disable and re-enable
            var playerInput = GetComponent<PlayerInput>();
            playerInput.user.ActivateControlScheme("Everything");
            playerInput.enabled = false;

            // load engine if not already 
            if (!FindObjectOfType<Engine>()) yield return SceneManager.LoadSceneAsync("Engine", LoadSceneMode.Additive);

            var sceneEnvironment = Environment.PlanetOrbitTop;

            if (!FirstRun) {
                // If it's not the first run, switch up the title screen :D
                var environmentIndex = Random.Range(0, 6);
                switch (environmentIndex) {
                    case 0:
                        sceneEnvironment = Environment.PlanetOrbitBottom;
                        break;
                    case 1:
                        sceneEnvironment = Environment.PlanetOrbitTop;
                        break;
                    case 2:
                        sceneEnvironment = Environment.SunriseClear;
                        break;
                    case 3:
                        sceneEnvironment = Environment.NoonClear;
                        break;
                    case 4:
                        sceneEnvironment = Environment.SunsetClear;
                        break;
                }
            }

            yield return SceneManager.LoadSceneAsync(sceneEnvironment.SceneToLoad, LoadSceneMode.Additive);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            MusicManager.Instance.PlayMusic(MusicTrack.MainMenu, FirstRun, false, false);
            Game.Instance.SetFlatScreenCameraControllerActive(false);

            FloatingOrigin.Instance.FocalTransform = transform;

            // enable input and forcefully pair ALL devices (I have no idea why we have to do this)
            playerInput.enabled = true;
            foreach (var inputDevice in InputSystem.devices)
                if (!playerInput.devices.Contains(inputDevice))
                    InputUser.PerformPairingWithDevice(inputDevice, playerInput.user);

            yield return new WaitForSecondsRealtime(0.1f);
            Game.Instance.FadeFromBlack();
            _shouldAnimate = true;
            ShowStartingPanel();
        }

        private void ShowStartingPanel() {
            topMenu.Hide();

            var lastPlayedVersion = Preferences.Instance.GetString("lastPlayedVersion");
            var showVrCalibrationDialog = Preferences.Instance.GetVector3("hmdPosition").Equals(Vector3.zero);
            var showWelcomeDialog = lastPlayedVersion == "none";

            // TODO: go back to last known panel on game mode quit? (e.g. time trial etc)
            // (handling the offline server stuff will probably be pain!)

            if (FirstRun) {
                // show main title screen logo and set up various show-once dialogs... 
                MenuBase startingPanel = topMenu;

                if (Game.Instance.IsVREnabled && showVrCalibrationDialog) {
                    startingPanel = vrCalibrationDialog;
                    vrCalibrationDialog.showWelcomeDialogNext = showWelcomeDialog;
                }
                else if (showWelcomeDialog) {
                    startingPanel = newPlayerWelcomeDialog;
                    newPlayerWelcomeDialog.SetCaller(profileMenu);
                }

                titleMenu.nextMenu = startingPanel;
                titleMenu.gameObject.SetActive(true);
            }
            else {
                topMenu.gameObject.SetActive(true);
            }

            Preferences.Instance.SetString("lastPlayedVersion", Application.version);
            Preferences.Instance.Save();
        }

        private void OnEnvironmentLoadComplete(Scene scene, LoadSceneMode mode) {
            SceneManager.SetActiveScene(scene);
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change) {
            var playerInput = GetComponent<PlayerInput>();
            if (change == InputDeviceChange.Added) InputUser.PerformPairingWithDevice(device, playerInput.user);
            if (change == InputDeviceChange.Removed) playerInput.user.UnpairDevice(device);
        }
    }
}