﻿using FistVR;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using Sodalite.Api;

namespace NToolbox
{
    [BepInPlugin(Common.PluginInfo.GUID, Common.PluginInfo.NAME, Common.PluginInfo.VERSION)]
    public class NToolbox : BaseUnityPlugin
    {
        public ConfigEntry<bool> LoadItemInteractions;
        public ConfigEntry<bool> LoadPlayerInteractions;
        public ConfigEntry<bool> LoadTnHInteractions;
        public ConfigEntry<bool> LoadSceneInteractions;
        
        /// <summary>
        /// Every button to be on the wrist menu. The scene buttons are seperate 
        /// </summary>
        public readonly Dictionary<string, System.Action> WristMenuButtonsItemInteractions = new()
        {
            {Common.SEPARATOR, Actions.Empty},

            //Item interactions
            { "Gather Items", Actions.GatherButtonClicked },
            { "Reset Traps", Actions.ResetTrapsButtonClicked },
            { "Freeze Guns/Melee", Actions.FreezeFireArmsMeleeButtonClicked },
            { "Freeze Ammo/Mags", Actions.FreezeAmmoMagButtonClicked },
            { "Freeze Attachments", Actions.FreezeAttachmentsButtonClicked },
            { "Unfreeze All", Actions.UnFreezeAllClicked },
            { "Ammo Panel", Actions.SpawnAmmoPanelButtonClicked },
            { "Ammo Weenie", Actions.SpawnAmmoWeenieButtonClicked },
            //trash bin
            //quickbelt fast?
            //sosig spawner
        };

        public readonly Dictionary<string, System.Action> WristMenuButtonsPlayerInteractions = new()
        {
            { Common.SEPARATOR, Actions.Empty },

            //Player body interactions
            { "Restore Full", Actions.RestoreHPButtonClicked },
            { "Restore 10%", Actions.Restore10PercentHPButtonClicked },//testing
            { "Toggle 1-hit", Actions.ToggleOneHitButtonClicked },
            { "Toggle God Mode", Actions.ToggleGodModeButtonClicked },//doesn't work against melee? its toggling hitboxes, so maybe some are disabled?
            { "Kill yourself", Actions.KillPlayerButtonClicked },
            //{ "Toggle Invisibility", Actions.ToggleInvisButtonClicked },//Broken? Test for flat IFF = -1 to see if the check is broken
        };
        
        public readonly Dictionary<string, System.Action> WristMenuButtonsTnHInteractions = new()
        {
            { Common.SEPARATOR, Actions.Empty },

            //Take and Hold interactions
            { "Add token", Actions.AddTokenButtonClicked },
            //{ "End hold", Actions.EndHoldButton },//BUG - Bad things, doesn't mesh well with TnHTweaker
            { "SP - Ammo Reloader", Actions.SpawnAmmoReloaderButton },
            { "SP - Magazine Duplicator", Actions.SpawnMagDupeButton },
            { "SP - Recycler", Actions.SpawnGunRecylcerButton },
            { "Kill patrols", Actions.KillPatrolsButtonClicked },
        };

        public NToolbox()
        {
            //Set config options
            LoadItemInteractions = Config.Bind("WristMenu Options", "LoadItemInteractions", true, "If set to true, will load all wristmenu actions relating to item interactions.");
            LoadPlayerInteractions = Config.Bind("WristMenu Options", "LoadPlayerInteractions", true, "If set to true, will load all wristmenu actions relating to player interactions.");
            LoadTnHInteractions = Config.Bind("WristMenu Options", "LoadTnHInteractions", true, "If set to true, will load all wristmenu actions relating to Take and Hold interactions.");
            LoadSceneInteractions = Config.Bind("WristMenu Options", "LoadSceneInteractions", true, "If set to true, will load all wristmenu actions relating to scene loading.");
        }

        public void Start()
        {
            //Diable TnH leaderboard scoring
            LeaderboardAPI.GetLeaderboardDisableLock();

            //Scene actions
            if (LoadSceneInteractions.Value)
            {
                Dictionary<string, string> SceneList = Actions.SCENE_LIST;
                Logger.LogInfo($"Loading {Actions.SCENE_LIST.Count} scene actions");
                foreach (var scene in SceneList.Reverse())
                {
                    WristMenuAPI.Buttons.Add(new WristMenuButton(scene.Value, () =>
                    {
                        SteamVR_LoadLevel.Begin(scene.Key, false, 0.5f, 0f, 0f, 1f);
                        foreach (var quitReceiver in GM.CurrentSceneSettings.QuitReceivers)
                            quitReceiver.BroadcastMessage("QUIT", SendMessageOptions.DontRequireReceiver);
                    }));
                    Logger.LogDebug($"Loaded scene action {scene.Key}");
                }
                //Add in a header thing for the tnh list
                WristMenuAPI.Buttons.Add(new WristMenuButton(Common.SEPARATOR, Actions.Empty));
            }
            else
            {
                Logger.LogInfo($"Skipping load scene Interactions");
            }


            //Wristmenu actions----------------------------------------------------------------------------------------
            //Take and Hold
            if (LoadTnHInteractions.Value)
            {
                Logger.LogInfo($"Loading {WristMenuButtonsTnHInteractions.Count} TnH Interaction actions");
                foreach (var kvp in WristMenuButtonsTnHInteractions.Reverse())
                {
                    WristMenuAPI.Buttons.Add(new WristMenuButton(kvp.Key, kvp.Value));
                    Logger.LogDebug($"Loaded action {kvp.Key}");
                }
            }
            else Logger.LogInfo($"Skipping load TnH Interactions");

            //Player
            if (LoadPlayerInteractions.Value)
            {
                Logger.LogInfo($"Loading {WristMenuButtonsPlayerInteractions.Count} Player Interaction actions");
                foreach (var kvp in WristMenuButtonsPlayerInteractions.Reverse())
                {
                    WristMenuAPI.Buttons.Add(new WristMenuButton(kvp.Key, kvp.Value));
                    Logger.LogDebug($"Loaded action {kvp.Key}");
                }
            }
            else Logger.LogInfo($"Skipping load player Interactions");

            //Item
            if (LoadItemInteractions.Value)
            {
                Logger.LogInfo($"Loading {WristMenuButtonsItemInteractions.Count} Item Interaction actions");
                foreach (var kvp in WristMenuButtonsItemInteractions.Reverse())
                {
                    WristMenuAPI.Buttons.Add(new WristMenuButton(kvp.Key, kvp.Value));
                    Logger.LogDebug($"Loaded action {kvp.Key}");
                }
            }
            else Logger.LogInfo($"Skipping load item Interactions");

            Logger.LogInfo("Fully loaded NToolbox!");
        }
    }
}