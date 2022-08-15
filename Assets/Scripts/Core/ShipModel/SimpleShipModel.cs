using System.Collections;
using System.Collections.Generic;
using Core.Player;
using Core.ShipModel.Feedback.interfaces;
using Core.ShipModel.ShipIndicator;
using Misc;
using UnityEngine;
using VFX;
#if !NO_PAID_ASSETS
using GPUInstancer;
#endif

namespace Core.ShipModel {
    /**
     * Provide some basic ship functionality expected from all mesh objects in an override-able fashion.
     */
    public class SimpleShipModel : MonoBehaviour, IShipModel {
        [SerializeField] private GameObject drawableContainer;
        [SerializeField] private ThrusterController thrusterController;
        [SerializeField] private Light shipLights;
        [SerializeField] private SmokeEmitter smokeEmitter;
        [SerializeField] public CapsuleCollider foliageCollider;

        [SerializeField] private List<MeshRenderer> primaryColorMeshes = new();
        [SerializeField] private List<MeshRenderer> accentColorMeshes = new();

        [SerializeField] private AudioSource engineBoostAudioSource;
        [SerializeField] private AudioSource externalBoostAudioSource;
        [SerializeField] private AudioSource externalBoostThrusterAudioSource;
        [SerializeField] private AudioSource simpleToggleAudioSource;
        [SerializeField] private AudioSource assistActivateAudioSource;
        [SerializeField] private AudioSource assistDeactivateAudioSource;
        [SerializeField] private AudioSource velocityLimitActivateAudioSource;
        [SerializeField] private AudioSource velocityLimitDeactivateAudioSource;

        private Coroutine _boostCoroutine;

        public virtual void Start() {
            // Init GPU Instance removal colliders 
#if !NO_PAID_ASSETS
            if (FindObjectOfType<GPUInstancerDetailManager>()) {
                var grassInstanceRemover = foliageCollider.gameObject.AddComponent<GPUInstancerInstanceRemover>();
                grassInstanceRemover.selectedColliders = new List<Collider> { foliageCollider };
                grassInstanceRemover.removeFromDetailManagers = true;
                grassInstanceRemover.removeFromPrefabManagers = false;
                grassInstanceRemover.removeFromTreeManagers = false;
                grassInstanceRemover.offset = 10;
                grassInstanceRemover.removeAtUpdate = true;
                grassInstanceRemover.useBounds = true;
            }

            if (FindObjectOfType<GPUInstancerTreeManager>()) {
                var treeInstanceRemover = foliageCollider.gameObject.AddComponent<GPUInstancerInstanceRemover>();
                treeInstanceRemover.selectedColliders = new List<Collider> { foliageCollider };
                treeInstanceRemover.removeFromDetailManagers = false;
                treeInstanceRemover.removeFromPrefabManagers = false;
                treeInstanceRemover.removeFromTreeManagers = true;
                treeInstanceRemover.removeAtUpdate = true;
                treeInstanceRemover.useBounds = true;
            }
#endif
        }

        public virtual void FixedUpdate() {
            ShipShake.Update();
        }

        public virtual void OnEnable() {
            Game.OnPauseToggle += PauseAudio;
            Game.OnRestart += Restart;
            ShipShake = new ShipShake(transform, transform.root.GetComponentInChildren<ShipCameraRig>());
        }

        public virtual void OnDisable() {
            Game.OnPauseToggle -= PauseAudio;
            Game.OnRestart -= Restart;
        }

        public ShipShake ShipShake { get; private set; }

        public MonoBehaviour Entity() {
            return this;
        }

        public void SetVisible(bool visible) {
            drawableContainer.SetActive(visible);
        }

        public void SetIsLocalPlayer(bool isLocalPlayer) {
            engineBoostAudioSource.priority = isLocalPlayer ? 0 : 128;
            externalBoostAudioSource.priority = isLocalPlayer ? 0 : 128;
            externalBoostThrusterAudioSource.priority = isLocalPlayer ? 0 : 128;
            simpleToggleAudioSource.priority = isLocalPlayer ? 1 : 128;
            assistActivateAudioSource.priority = isLocalPlayer ? 1 : 128;
            assistDeactivateAudioSource.priority = isLocalPlayer ? 1 : 128;
            velocityLimitActivateAudioSource.priority = isLocalPlayer ? 1 : 128;
            velocityLimitDeactivateAudioSource.priority = isLocalPlayer ? 1 : 128;
        }

        #region IShip Basic Functions

        public virtual void SetLights(bool active) {
            simpleToggleAudioSource.Play();
            shipLights.enabled = !shipLights.enabled;

            // ensure that the local player ship lights take priority over all others
            var player = GetComponentInParent<ShipPlayer>();
            if (player && player.isLocalPlayer) shipLights.renderMode = LightRenderMode.ForcePixel;
        }

        public virtual void SetAssist(AssistToggleType assistToggleType, bool active) {
            if (active) assistActivateAudioSource.Play();
            else assistDeactivateAudioSource.Play();
        }

        public virtual void SetVelocityLimiter(bool active) {
            if (active) velocityLimitActivateAudioSource.Play();
            else velocityLimitDeactivateAudioSource.Play();
        }

        public void Boost(float boostTime) {
            IEnumerator AnimateBoost() {
                ShipShake.Shake(1, 0.005f, false, new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1)));
                yield return new WaitForSeconds(1);
                externalBoostThrusterAudioSource.Play();
                ShipShake.Shake(boostTime - 1, 0.01f, true);
                thrusterController.AnimateBoostThrusters();
            }

            engineBoostAudioSource.Play();
            externalBoostAudioSource.Play();
            _boostCoroutine = StartCoroutine(AnimateBoost());
        }

        #endregion

        #region Rolling Updates

        public virtual void OnShipIndicatorUpdate(IShipIndicatorData shipIndicatorData) {
            /* Indicators are entirely model-specific and should be implemented. */
        }

        public virtual void OnShipMotionUpdate(IShipMotionData shipMotionData) {
            // TODO: I literally have no idea where this came from or why, maybe do some digging here?
            // At least it's localised now ...
            var torqueVec = new Vector3(
                shipMotionData.CurrentAngularTorqueNormalised.x,
                MathfExtensions.Remap(-0.8f, 0.8f, -1, 1, shipMotionData.CurrentAngularTorqueNormalised.y),
                MathfExtensions.Remap(-0.3f, 0.3f, -1, 1, shipMotionData.CurrentAngularTorqueNormalised.z)
            );

            thrusterController.UpdateThrusters(shipMotionData.CurrentLateralForce, torqueVec);
            smokeEmitter.UpdateThrustTrail(shipMotionData.CurrentLateralVelocity, shipMotionData.MaxLateralVelocity,
                shipMotionData.CurrentLateralForceNormalised);
            foliageCollider.radius = MathfExtensions.Remap(0, shipMotionData.MaxLateralVelocity / 2, 4, 15, shipMotionData.CurrentLateralVelocity.magnitude);
        }

        #endregion

        #region User Preferences

        public virtual void SetPrimaryColor(string htmlColor) {
            var color = ParseColor(htmlColor);
            primaryColorMeshes.ForEach(mesh => {
                var mat = mesh.material;
                mat.color = color;
            });
        }

        public virtual void SetAccentColor(string htmlColor) {
            var color = ParseColor(htmlColor);
            accentColorMeshes.ForEach(mesh => {
                var mat = mesh.material;
                mat.color = color;
            });
        }

        public virtual void SetThrusterColor(string htmlColor) {
            var color = ParseColor(htmlColor);
            foreach (var thruster in GetComponentsInChildren<Thruster>()) thruster.ThrustColor = color;
        }

        /**
         * Set the color of the trails which occur under boost
         */
        public virtual void SetTrailColor(string htmlColor) {
            var trailColor = ParseColor(htmlColor);
            smokeEmitter.UpdateColor(trailColor);
        }

        /**
         * Set the color of the ship head-lights
         */
        public virtual void SetHeadLightsColor(string htmlColor) {
            var color = ParseColor(htmlColor);
            foreach (var shipLight in GetComponentsInChildren<Light>())
                if (shipLight.type == LightType.Spot)
                    shipLight.color = color;
        }

        #endregion

        #region Internal Helper

        protected void PauseAudio(bool paused) {
            if (Game.Instance.SessionType != SessionType.Singleplayer) return;
            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
                if (paused) audioSource.Pause();
                else audioSource.UnPause();
        }

        protected Color ParseColor(string htmlColor) {
            if (ColorUtility.TryParseHtmlString(htmlColor, out var color)) return color;
            color = Color.red;
            Debug.LogWarning("Failed to parse html color " + htmlColor);

            return color;
        }

        private void Restart() {
            ShipShake.Reset();
            engineBoostAudioSource.Stop();
            externalBoostAudioSource.Stop();
            externalBoostThrusterAudioSource.Stop();
            if (_boostCoroutine != null) StopCoroutine(_boostCoroutine);
        }

        #endregion
    }
}