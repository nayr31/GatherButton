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
    [BepInDependency(Common.PluginInfo.SODALITE_GUID)]
    public class NToolbox : BaseUnityPlugin
    {
        public static ObjectIDList ObjectIDs { get; private set; }
        public readonly ConfigEntry<bool> LoadWristMenu;

        public NToolbox() 
        {
            //Set config option
            LoadWristMenu = Config.Bind("WristMenu Options", "LoadWristMenu", false, "If set to true, will load all wristmenu actions. I don't recommend using this, please don't.");
        }

        public void Start()
        {
            //Diable TnH leaderboard scoring
            LeaderboardAPI.GetLeaderboardDisableLock();

            ObjectIDs = new ObjectIDList(Common.OBJECT_ID_LIST_FILENAME);
            
            NPanel nPanel = new NPanel();
            WristMenuAPI.Buttons.Add(new WristMenuButton("NTool Panel", nPanel.Spawn));

            if (LoadWristMenu.Value) nPanel.LoadWristMenu();
        }
    }
}