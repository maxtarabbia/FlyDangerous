﻿using System;
using Audio;
using Core;
using Core.Player;
using Menus.Pause_Menu;
using UnityEngine;

namespace Menus.Main_Menu {
    public class ConnectingDialog : MenuBase {
        [SerializeField] private DisconnectionDialog disconnectionDialog;

        private LobbyMenu _lobbyMenu;
        
        public async void Connect(LobbyMenu lobbyMenu, string address, string portText = "", string password = "") {
            _lobbyMenu = lobbyMenu;
            if (FdNetworkManager.Instance.OnlineService != null) {
                await FdNetworkManager.Instance.OnlineService.JoinLobby(address);
                FdNetworkManager.Instance.NetworkAddress = address;
            }
            else {
                ushort port = Convert.ToUInt16(Int16.Parse(portText));
                FdNetworkManager.Instance.NetworkAddress = address;
                FdNetworkManager.Instance.NetworkPort = port;
            }
            
            FdNetworkManager.Instance.StartClient();
            FdNetworkManager.Instance.joinGameRequestMessage = new FdNetworkManager.JoinGameRequestMessage {
                password = password,
                version = Application.version
            };
        }
        
        private void Start() {
            FdNetworkManager.OnClientConnected += HandleClientConnected;
            FdNetworkManager.OnClientDisconnected += HandleFailedConnection;
            FdNetworkManager.OnClientConnectionRejected += HandleClientRejected;
        }
        
        private void OnDestroy() {
            FdNetworkManager.OnClientConnected -= HandleClientConnected;
            FdNetworkManager.OnClientDisconnected -= HandleFailedConnection;
            FdNetworkManager.OnClientConnectionRejected -= HandleClientRejected;
        }

        private void HandleClientConnected(FdNetworkManager.JoinGameSuccessMessage successMessage) {
            Hide();
            
            // if the server has created a lobby player for us, show the lobby
            if (successMessage.showLobby) {
                Game.Instance.SessionStatus = SessionStatus.LobbyMenu;
                _lobbyMenu.Open(caller);
                _lobbyMenu.JoinPlayer();
                
                var localPlayer = LobbyPlayer.FindLocal;
                if (localPlayer) {
                    localPlayer.UpdateLobby(successMessage.levelData, successMessage.maxPlayers);
                }
            }
        }

        private void HandleFailedConnection() {
            disconnectionDialog.Open(caller);
            disconnectionDialog.Reason = "Failed to connect to server";
            Hide();
        }

        private void HandleClientRejected(string reason) {
            Hide();
            disconnectionDialog.Open(caller);
            disconnectionDialog.Reason = reason;
        }
    }
}