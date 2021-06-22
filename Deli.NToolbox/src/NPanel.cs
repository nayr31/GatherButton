﻿using FistVR;
using Sodalite;
using Sodalite.Api;
using Sodalite.UiWidgets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using Sodalite.Utilities;

namespace NToolbox
{
    public class NPanel
    {
        private LockablePanel _NPanel;
        private GridLayoutWidget _menu;
        private GridLayoutWidget _itemTools;
        private GridLayoutWidget _playerTools;
        private GridLayoutWidget _tnhTools;
        private GridLayoutWidget _powerupTools;
        private GridLayoutWidget _sceneTools;
        private GridLayoutWidget _miscTools;

        private List<string> _miscList = new List<string>(32);
        private List<ButtonWidget> _buttonList = new List<ButtonWidget>(16);
        private int _miscOffset = 0;

        public NPanel()
        {
            for (int i = 0; i < 30; i++)
                _miscList[i] = i.ToString();

            _NPanel = new LockablePanel();
            _NPanel.Configure += ConfigureTools;
            _NPanel.TextureOverride = SodaliteUtils.LoadTextureFromBytes(Assembly.GetExecutingAssembly().GetResource("panel.png"));
        } 
        

        public void ConfigureTools(GameObject panel)
        {
            GameObject canvas = panel.transform.Find("OptionsCanvas_0_Main/Canvas").gameObject;


            _menu = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "Item interactions";
                    button.AddButtonListener(SwitchToItem);
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "Player interactions";
                    button.AddButtonListener(SwitchToPlayer);
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "TnH Stuff";
                    button.AddButtonListener(SwitchToTnH);
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "Powerups";
                    button.AddButtonListener(SwitchToPower);
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "Scene Selection";
                    button.AddButtonListener(SwitchToScene);
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "Misc Tools";
                    button.AddButtonListener(SwitchToMisc);
                    button.RectTransform.localRotation = Quaternion.identity;
                });
            });

            _itemTools = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                AddBack(widget);
                AddBatch(widget, Tools.ITEM);
            });
            _itemTools.gameObject.SetActive(false);

            _playerTools = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                AddBack(widget);
                AddBatch(widget, Tools.PLAYER);
            });
            _playerTools.gameObject.SetActive(false);

            _tnhTools = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                AddBack(widget);
                AddBatch(widget, Tools.TNH);
            });
            _tnhTools.gameObject.SetActive(false);

            _powerupTools = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                AddBack(widget);
                foreach (var kvp in Common.DOG_LIST)
                {
                    widget.AddChild((ButtonWidget button) => {
                        button.ButtonText.text = kvp.Value;
                        button.AddButtonListener(() => Actions.SpawnItemByItemIdLeftHand(kvp.Key, true));
                        button.RectTransform.localRotation = Quaternion.identity;
                    });
                }
            });
            _powerupTools.gameObject.SetActive(false);

            _sceneTools = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                AddBack(widget);
                foreach (var kvp in Common.SCENE_LIST)
                {
                    widget.AddChild((ButtonWidget button) => {
                        button.ButtonText.text = kvp.Value;
                        button.AddButtonListener(() =>
                        {
                            SteamVR_LoadLevel.Begin(kvp.Key, false, 0.5f, 0f, 0f, 1f);
                            foreach (var quitReceiver in GM.CurrentSceneSettings.QuitReceivers)
                                quitReceiver.BroadcastMessage("QUIT", SendMessageOptions.DontRequireReceiver);
                        });
                    button.RectTransform.localRotation = Quaternion.identity;
                    });
                }
            });
            _sceneTools.gameObject.SetActive(false);

            _miscTools = UiWidget.CreateAndConfigureWidget(canvas, (GridLayoutWidget widget) =>
            {
                // Fill our parent and set pivot to top middle
                widget.RectTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                widget.RectTransform.localPosition = Vector3.zero;
                widget.RectTransform.localRotation = Quaternion.identity;
                widget.RectTransform.anchoredPosition = Vector2.zero;
                widget.RectTransform.sizeDelta = new Vector2(37f / 0.07f, 24f / 0.07f);
                widget.RectTransform.pivot = new Vector2(0.5f, 1f);
                // Adjust our grid settings
                widget.LayoutGroup.cellSize = new Vector2(171, 50);
                widget.LayoutGroup.spacing = Vector2.one * 4;
                widget.LayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
                widget.LayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                widget.LayoutGroup.childAlignment = TextAnchor.UpperCenter;
                widget.LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                widget.LayoutGroup.constraintCount = 3;

                AddBack(widget);

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "----Down----";
                    button.AddButtonListener(() => { moveRef(false); });
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = "----Up----";
                    button.AddButtonListener(() => { moveRef(true); });
                    button.RectTransform.localRotation = Quaternion.identity;
                });

                for(int i=0; i<9; i++)
                {
                    widget.AddChild((ButtonWidget button) => {
                        button.ButtonText.text = _miscList[i];
                        button.AddButtonListener(() => {  });
                        button.RectTransform.localRotation = Quaternion.identity;
                        _buttonList[i] = button;
                    });
                }

            });
            _miscTools.gameObject.SetActive(false);


            for (int i = 0; i < NToolbox.ObjectIDs.Length; i++)
            {
                
            }
        }

        private void moveRef(bool isUp)
        {
            if ((isUp && _miscOffset - 3 >= 0) || (!isUp && _miscOffset + 3 <= _miscList.Count)) { 
                _miscOffset += isUp ? (-3) : 3;
                for(int i = 0; i < 9; i++)
                    _buttonList[i].ButtonText.text = _miscList[i + _miscOffset];
            }
        }

        private void SwitchPage(GridLayoutWidget page)
        {
            _menu.gameObject.SetActive(false);
            page.gameObject.SetActive(true);
        }

        private void SwitchToItem() => SwitchPage(_itemTools);
      
        private void SwitchToPlayer() => SwitchPage(_playerTools);

        private void SwitchToTnH() => SwitchPage(_tnhTools);

        private void SwitchToPower() => SwitchPage(_powerupTools);
      
        private void SwitchToScene() => SwitchPage(_sceneTools);

        private void SwitchToMisc() => SwitchPage(_miscTools);

        private void AddBatch(GridLayoutWidget widget, Dictionary<string, Action> dict)
        {
            foreach (var kvp in dict)
            {
                widget.AddChild((ButtonWidget button) => {
                    button.ButtonText.text = kvp.Key;
                    button.AddButtonListener(kvp.Value);
                    button.RectTransform.localRotation = Quaternion.identity;
                });
            }
        }

        private void AddBack(GridLayoutWidget widget)
        {
            widget.AddChild((ButtonWidget button) => {
                button.ButtonText.text = "----Back----";
                button.AddButtonListener(() => 
                {
                    widget.gameObject.SetActive(false);
                    _menu.gameObject.SetActive(true);
                });
                button.RectTransform.localRotation = Quaternion.identity;
            });
        }

        public void Spawn()
        {
            FVRWristMenu wristMenu = WristMenuAPI.Instance;
            if (wristMenu is null || !wristMenu) return;
            GameObject panel = _NPanel.GetOrCreatePanel();
            wristMenu.m_currentHand.RetrieveObject(panel.GetComponent<FVRPhysicalObject>());
        }

        private void AddSeparator() => WristMenuAPI.Buttons.Add(new WristMenuButton(Common.SEPARATOR, Actions.Empty));

        public void LoadWristMenu()//legacy stuff for reasons i guess
        {
            Dictionary<string, string> SceneList = Common.SCENE_LIST;
            foreach (var scene in SceneList.Reverse())
            {
                WristMenuAPI.Buttons.Add(new WristMenuButton(scene.Value, () =>
                {
                    SteamVR_LoadLevel.Begin(scene.Key, false, 0.5f, 0f, 0f, 1f);
                    foreach (var quitReceiver in GM.CurrentSceneSettings.QuitReceivers)
                        quitReceiver.BroadcastMessage("QUIT", SendMessageOptions.DontRequireReceiver);
                }));
            }
            //Add in a header thing for the tnh list
            AddSeparator();

            //Wristmenu actions----------------------------------------------------------------------------------------
            //Take and Hold
            foreach (var kvp in Tools.TNH.Reverse())
                WristMenuAPI.Buttons.Add(new WristMenuButton(kvp.Key, kvp.Value));

            AddSeparator();

            //Player
            foreach (var kvp in Tools.PLAYER.Reverse())
                WristMenuAPI.Buttons.Add(new WristMenuButton(kvp.Key, kvp.Value));

            AddSeparator();

            //Item
            foreach (var kvp in Tools.ITEM.Reverse())
                WristMenuAPI.Buttons.Add(new WristMenuButton(kvp.Key, kvp.Value));

            AddSeparator();
        }
    }
}
