using BepInEx;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Gorilla_PC_UI
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private bool showGUI = true;
        private Rect guiWindowRect = new Rect(10, 10, 600, 400);
        private int selectedTab = 0;
        private List<Player> playerList = new List<Player>();
        private string[] tabNames = { "Server", "App", "Leader Board", "Installed Mods", "Player", "Credits" };
        private string newname;

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.F1))
            {
                showGUI = !showGUI;
            }
            GUI.enabled = showGUI;
        }

        private void OnGUI()
        {
            
            if (showGUI)
            {
                GUI.Window(0, guiWindowRect, MyGUIWindow, "Gorilla PC UI");
            }
        }

        private void MyGUIWindow(int windowID)
        {
            if (showGUI)
            {

                GUI.DragWindow(new Rect(0, 0, guiWindowRect.width, 20));

                DrawTabs();

                switch (selectedTab)
                {
                    case 0:
                        DrawTabContent1();
                        break;
                    case 1:
                        DrawTabContent2();
                        break;
                    case 2:
                        DrawTabContent3();
                        break;
                    case 3:
                        DrawTabContent4();
                        break;
                    case 4:
                        DrawTabContent5();
                        break;
                    case 5:
                        DrawTabContent6();
                        break;
                    default:
                        break;
                }
            }
        }

            private void DrawTabs()
            {
                GUILayout.BeginHorizontal();
                for (int i = 0; i < tabNames.Length; i++)
                {
                    if (GUILayout.Toggle(selectedTab == i, tabNames[i], "Button"))
                    {
                        selectedTab = i;
                    }
                }
                GUILayout.EndHorizontal();
            }

            private void DrawTabContent1()
            {
                if (GUI.Button(new Rect(10, 50, 180, 30), "Leave Lobby"))
                {
                    PhotonNetwork.Disconnect();
                }
                if (GUI.Button(new Rect(10, 90, 180, 30), "Join Random"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }

            }

            private void DrawTabContent2()
            {
                if (GUI.Button(new Rect(10, 50, 180, 30), "Quit"))
                {
                    Application.Quit();
                }
            }

            private void DrawTabContent3()
            {
                UpdatePlayerList();

                GUILayout.Label("Player List:");
                foreach (var player in playerList)
                {
                    GUILayout.Label("Player Name: " + player.NickName);
                }
            }

            private void UpdatePlayerList()
            {
                playerList.Clear();

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    playerList.Add(player);
                }
            }

            private void DrawTabContent4()
            {
                GUILayout.Label("Loaded Plugins:", GetLabelStyle());

                var plugins = BepInEx.Bootstrap.Chainloader.PluginInfos;

                foreach (var kvp in plugins)
                {
                    BepInPlugin bepinPluginAttribute = kvp.Value.Metadata;

                    GUILayout.Label($"Name: {bepinPluginAttribute.Name}, GUID: {bepinPluginAttribute.GUID}, Version: {bepinPluginAttribute.Version}", GetLabelStyle());
                }
            }

        private void DrawTabContent6()
        {
            GUI.Label(new Rect(10, 50, 50, 50), "Tyre Made This ");
        }

        private void DrawTabContent5()
            {
            newname = GUI.TextArea(new Rect(10, 50, 200, 20), newname);
            if (GUI.Button(new Rect(10, 90, 180, 30), "Set Name"))
            {
                PhotonNetwork.NickName = newname;
                
            }
            GUI.Label(new Rect(10, 130, 200, 200), "I Dont Even Know If This Works");
        }

            private GUIStyle GetLabelStyle()
            {
                GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.fontSize = 14;
                return labelStyle;
            }
        
    }
}
