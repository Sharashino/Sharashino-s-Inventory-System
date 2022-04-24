using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sh.Inventory.Editors
{
    public class EquipmentPanelWizard : EditorWindow
    {
        private int curPanelsAmount = 0;
        private int panelsAmount = 0;
        private WeaponTypes[] weaponPanelType;
        private ArmorTypes[] armorPanelType;
        private ItemTypes[] equipmentPanelType;
        private int[] equipmentPanelX;
        private int[] equipmentPanelY;
        private Color panelsBackgroundColor = Color.white;

        [MenuItem("Sharashino Tools/New EquipmentPanel")]
        static void Init()
        {
            var wizard = (EquipmentPanelWizard)GetWindow(typeof(EquipmentPanelWizard));
            wizard.titleContent = new GUIContent("EquipmentPanel Wizard");
            wizard.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical("HelpBox");

            panelsBackgroundColor = EditorGUILayout.ColorField("Background color", panelsBackgroundColor);
            panelsAmount = EditorGUILayout.IntSlider("Equipment panels count", panelsAmount, 1, 10);

            if (curPanelsAmount != panelsAmount) // Allocating memory for objects
            {
                curPanelsAmount = panelsAmount;

                equipmentPanelX = new int[panelsAmount];
                equipmentPanelY = new int[panelsAmount];
                equipmentPanelType = new ItemTypes[panelsAmount];
                armorPanelType = new ArmorTypes[panelsAmount];
                weaponPanelType = new WeaponTypes[panelsAmount];
            }

            GUILayout.EndVertical();

            for (int i = 0; i < panelsAmount; i++)
            {
                GUILayout.BeginVertical("HelpBox");

                equipmentPanelType[i] = (ItemTypes)EditorGUILayout.EnumPopup("Slot allowed item: ", equipmentPanelType[i]);

                switch (equipmentPanelType[i])
                {
                    case ItemTypes.Weapon:
                        {
                            weaponPanelType[i] = (WeaponTypes)EditorGUILayout.EnumPopup("Slot allowed weapon: ", weaponPanelType[i]);
                            DrawPanelSizeFields(i);
                            break;
                        }
                    case ItemTypes.Armor:
                        {
                            armorPanelType[i] = (ArmorTypes)EditorGUILayout.EnumPopup("Slot allowed weapon: ", armorPanelType[i]);
                            DrawPanelSizeFields(i);
                            break;
                        }
                    case ItemTypes.Trinket:
                        {
                            DrawPanelSizeFields(i);
                            break;
                        }
                    case ItemTypes.Consumable:
                        {
                            DrawPanelSizeFields(i);
                            break;
                        }
                    case ItemTypes.None:
                        break;
                    default:
                        break;
                }

                GUILayout.EndVertical();
            }

            if (GUILayout.Button("Build panels"))
            {
                var manager = InventoryManager.Instance;
                if(manager == null)

                manager.equipmentPanels = new List<EquipmentPanel>();

                for (int i = 0; i < panelsAmount; i++)
                {
                    var cellHolder = Instantiate(new GameObject());
                    cellHolder.transform.SetParent(manager.InventoryHolder);

                    switch (equipmentPanelType[i])
                    {
                        case ItemTypes.Weapon:
                            {
                                WeaponEquipmentPanel weaponPanel = cellHolder.AddComponent<WeaponEquipmentPanel>();
                                weaponPanel.allowedWeaponType = weaponPanelType[i];
                                weaponPanel.allowedItemType = equipmentPanelType[i];
                                weaponPanel.width = equipmentPanelX[i];
                                weaponPanel.height = equipmentPanelY[i];

                                cellHolder.name = "Equipment panel: " + equipmentPanelType[i] + weaponPanelType[i];
                                manager.equipmentPanels.Add(weaponPanel);
                                break;
                            }
                        case ItemTypes.Armor:
                            {
                                ArmorEquipmentPanel armorPanel = cellHolder.AddComponent<ArmorEquipmentPanel>();

                                armorPanel.allowedArmorType = armorPanelType[i];
                                armorPanel.allowedItemType = equipmentPanelType[i];
                                armorPanel.width = equipmentPanelX[i];
                                armorPanel.height = equipmentPanelY[i];

                                cellHolder.name = "Equipment panel: " + equipmentPanelType[i] + armorPanelType[i];
                                manager.equipmentPanels.Add(armorPanel);
                                break;
                            }
                        case ItemTypes.Trinket:
                            {
                                TrinketEquipmentPanel trinketPanel =
                                    cellHolder.AddComponent<TrinketEquipmentPanel>();

                                trinketPanel.allowedItemType = equipmentPanelType[i];
                                trinketPanel.allowedItemType = equipmentPanelType[i];
                                trinketPanel.width = equipmentPanelX[i];
                                trinketPanel.height = equipmentPanelY[i];

                                cellHolder.name = "Equipment panel: " + equipmentPanelType[i];
                                manager.equipmentPanels.Add(trinketPanel);
                                break;
                            }
                        default:
                            {
                                EquipmentPanel equipmentPanel = cellHolder.AddComponent<ArmorEquipmentPanel>();

                                equipmentPanel.allowedItemType = equipmentPanelType[i];
                                equipmentPanel.allowedItemType = equipmentPanelType[i];
                                equipmentPanel.width = equipmentPanelX[i];
                                equipmentPanel.height = equipmentPanelY[i];

                                cellHolder.name = "Equipment panel: " + equipmentPanelType[i];
                                manager.equipmentPanels.Add(equipmentPanel);
                                break;
                            }
                    }

                    var cellHolderImage = cellHolder.AddComponent<Image>();
                    cellHolderImage.rectTransform.sizeDelta = new Vector2(
                        (manager.cellSize + manager.spacing)* equipmentPanelX[i],
                        (manager.cellSize + manager.spacing) * equipmentPanelY[i]);
                    cellHolderImage.rectTransform.anchoredPosition = Vector2.zero;
                    cellHolderImage.color = panelsBackgroundColor;

                    CleanUp();
                }

                manager.DrawPreview();
            }
        }

        private void DrawPanelSizeFields(int i)
        {
            equipmentPanelX[i] = EditorGUILayout.IntField("Equipment Width size", equipmentPanelX[i]);

            if (equipmentPanelX[i] == 0)
                equipmentPanelX[i] = 1;

            equipmentPanelY[i] = EditorGUILayout.IntField("Equipment Height size", equipmentPanelY[i]);

            if (equipmentPanelY[i] == 0)
                equipmentPanelY[i] = 1;
        }

        public void CleanUp()
        {
            var garbage = GameObject.Find("New Game Object");

            DestroyImmediate(garbage);
        }
    }
}