﻿using System.Linq;
using Core;
using Core.Player;
using JetBrains.Annotations;
using Menus.Main_Menu.Components;
using Mirror;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.Main_Menu {
    public class LobbyMenu : MenuBase {

        [Header("UI")]
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private MultiPlayerMenu topMenu;
        [SerializeField] private UIButton startButton;
        [SerializeField] private Text headerText;

        [SerializeField] private Button loadCustomButton;
        [SerializeField] private LobbyConfigurationPanel lobbyConfigurationPanel;

        [SerializeField] private InputField chatSendMessageInput;
        [SerializeField] private Text chatMessageBox;
        [SerializeField] private ScrollRect chatScrollRect;
        
        public UIButton StartButton => startButton;

        protected override void OnOpen() {
            var localPlayer = FdPlayer.FindLocalLobbyPlayer;
            if (localPlayer) {
                lobbyConfigurationPanel.IsHost = localPlayer.isHost;
            }
        }

        protected override void OnClose() {
            FdNetworkManager.Instance.ShutdownNetwork();
        }

        public void JoinPlayer() {
            headerText.text = "MULTIPLAYER LOBBY";
        }

        public void StartHost() {
            headerText.text = "HOSTING LOBBY";
            // TODO: Use UI for maxConnections
            NetworkServer.dontListen = false;
            FdNetworkManager.Instance.StartHost();
            if (FdNetworkManager.Instance.HasMultiplayerServices) {
                Debug.Log("Online service active");
                FdNetworkManager.Instance.OnlineService!.Multiplayer!.CreateLobby();
            }
        }

        public void StartGame() {
            var localLobbyPlayer = FdPlayer.FindLocalLobbyPlayer;
            var lobbyLevelData = lobbyConfigurationPanel.LobbyLevelData;
            if (localLobbyPlayer) {
                // host will attempt to start game if all are ready
                if (localLobbyPlayer.isHost) {
                    var lobbyPlayers = FindObjectsOfType<LobbyPlayer>();
                    if (lobbyPlayers.All(lobbyPlayer => lobbyPlayer.isReady)) {
                        FdNetworkManager.Instance.StartGameLoadSequence(SessionType.Multiplayer, lobbyLevelData);
                    }
                    localLobbyPlayer.SendChatMessage("<HOST WANTS TO START THE GAME>");
                }
                else {
                    localLobbyPlayer.ToggleReady();   
                }
            }
        }

        public void CloseLobby([CanBeNull] string reason = null) {
            if (!string.IsNullOrEmpty(reason)) {
                FdNetworkManager.Instance.ShutdownNetwork();
                PlayCancelSound();
                mainMenu.ShowDisconnectedDialog(reason);
                Hide();
            }
            else {
                Cancel();
            }
        }

        public void SendChatMessage(string message) {
            // var message = chatSendMessageInput.text;
            var player = FdPlayer.FindLocalLobbyPlayer;
            if (player && message != "") {
                player.SendChatMessage(message);
            }

            chatSendMessageInput.text = "";
            chatSendMessageInput.ActivateInputField();
        }

        public void ReceiveChatMessage(string message) {
            chatMessageBox.text += message + "\n";
            // force to bottom
            chatScrollRect.verticalNormalizedPosition = 0f;
        }
    }
}