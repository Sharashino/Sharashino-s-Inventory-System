﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Sh.Inventory.Editors
{
    public class InventoryWizard : EditorWindow
    {
        #region variables

        //Inventory
        private int SizeX, SizeY;
        private int CellSize;
        private int CellSpacing;

        private Sprite CellImage;
        private Sprite InventoryBackground;

        private Color normalCellColor = Color.white, hoveredCellColor = Color.grey, blockedCellCover = Color.red;
        private Color inventoryBackgroundColor = Color.white;

        private Color stackTextColor = Color.gray;
        private Font stackTextFont;

        //Loot window
        private Vector2 inventoryRectSize;
        private int LootSizeX, LootSizeY;

        //Equipment panels
        private int equipmentPanelsCount;
        private int currentEquipmentPanelsCount;
        
        private int[] equipmentPanelX;
        private int[] equipmentPanelY;
        
        private ItemTypes[] equipmentPanelType;
        private ArmorTypes[] armorPanelType;
        private WeaponTypes[] weaponPanelType;

        #endregion

        private InventoryManager inventory;

        public int tabIndex = 0;
        public string[] tabHeaders = new string[] { "Inventory", "Equipment panels" };
        public Transform inventoryTransform;

        [MenuItem("Sharashino Tools/New Inventory")]
        static void Init()
        {
            var wizard = (InventoryWizard)GetWindow(typeof(InventoryWizard));
            wizard.titleContent = new GUIContent("Inventory Wizard");
            wizard.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Inventory & Cell size", EditorStyles.centeredGreyMiniLabel);

            GUILayout.BeginVertical("HelpBox"); GUILayout.BeginVertical("GroupBox");

            SizeX = EditorGUILayout.IntSlider("Inventory Width size", SizeX, 1, 100);
            SizeY = EditorGUILayout.IntSlider("Inventory Height size", SizeY, 1, 100);

            normalCellColor = EditorGUILayout.ColorField("Normal cell color", normalCellColor);
            hoveredCellColor = EditorGUILayout.ColorField("Hovered cell color", hoveredCellColor);
            blockedCellCover = EditorGUILayout.ColorField("Blocked cell color", blockedCellCover);

            stackTextFont = (Font)EditorGUILayout.ObjectField("Stack text font", stackTextFont, typeof(Font), false);
            stackTextColor = EditorGUILayout.ColorField("Stack text color", stackTextColor);

            EditorGUILayout.LabelField("");

            CellSize = EditorGUILayout.IntSlider("Cell size", CellSize, 1, 200);
            CellImage = (Sprite)EditorGUILayout.ObjectField("Cell image", CellImage, typeof(Sprite), false);

            inventoryBackgroundColor = EditorGUILayout.ColorField("Background color", inventoryBackgroundColor);
            InventoryBackground = (Sprite)EditorGUILayout.ObjectField("Inventory background", InventoryBackground, typeof(Sprite), false);

            if (GUILayout.Button("Build inventory"))
            {
                var eventSystem = Instantiate(new GameObject());
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
                eventSystem.AddComponent<BaseInput>();
                eventSystem.name = "Event System";

                CleanUp();

                if (CellImage == null)
                {
                    EditorUtility.DisplayDialog("Can't spawn that", "Please add a cell image!", "Okay...");
                    return;
                }

                var obj = Instantiate(new GameObject());
                obj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                obj.AddComponent<CanvasScaler>();
                obj.AddComponent<GraphicRaycaster>();
                obj.AddComponent<InventoryBase>();
                eventSystem.gameObject.transform.parent = obj.transform;

                obj.name = "Inventory Canvas";

                var cellHolder = Instantiate(new GameObject());
                cellHolder.transform.SetParent(obj.transform);
                cellHolder.name = "Inventory";

                inventoryTransform = obj.transform;

                var cellHolderImage = cellHolder.AddComponent<Image>();
                cellHolderImage.rectTransform.sizeDelta = new Vector2((CellSize + CellSpacing) * SizeX, (CellSize + CellSpacing) * SizeY);
                cellHolderImage.rectTransform.anchoredPosition = Vector2.zero;
                cellHolderImage.color = inventoryBackgroundColor;

                inventoryRectSize = cellHolderImage.rectTransform.sizeDelta;

                var inventory = cellHolder.AddComponent<InventoryManager>();
                inventory.cellSize = CellSize;
                inventory.spacing = CellSpacing;
                inventory.column = SizeX;
                inventory.row = SizeY;

                inventory.normalCellColor = normalCellColor;
                inventory.blockedCellColor = blockedCellCover;
                inventory.hoveredCellColor = hoveredCellColor;

                var imgObj = Instantiate(new GameObject());
                imgObj.AddComponent<Image>().sprite = CellImage;
                imgObj.GetComponent<RectTransform>().sizeDelta = new Vector2(CellSize, CellSize);
                imgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                imgObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                imgObj.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                imgObj.transform.SetParent(obj.transform);

                imgObj.GetComponent<Image>().type = Image.Type.Sliced;
                imgObj.GetComponent<Image>().color = Color.white;

                imgObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 3000);
                imgObj.AddComponent<GridSlot>();

                imgObj.name = "Utility object";

                var stackText = Instantiate(new GameObject());
                stackText.transform.SetParent(imgObj.transform);

                var stackTextComponent = stackText.AddComponent<Text>();
                stackTextComponent.alignment = TextAnchor.LowerLeft;
                stackTextComponent.raycastTarget = false;
                stackTextComponent.rectTransform.anchorMin = new Vector2(0, 0);
                stackTextComponent.rectTransform.anchorMax = new Vector2(0, 0);
                stackTextComponent.rectTransform.anchoredPosition = new Vector2(53, 53);
                stackTextComponent.color = stackTextColor;
                if (stackTextFont != null)
                    stackTextComponent.font = stackTextFont;

                inventory.cell = imgObj.GetComponent<Image>();
                inventory.DrawPreview();

                this.inventory = inventory;
                    
                CleanUp(); CleanUp(); CleanUp(); CleanUp();

                GUILayout.EndVertical();
                GUILayout.EndVertical();
            }
        }

        public void CleanUp()
        {
            var garbage = GameObject.Find("New Game Object");

            DestroyImmediate(garbage);
        }
    }
}