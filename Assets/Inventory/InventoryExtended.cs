using UnityEngine;
using UnityEditor;
using Sh.Inventory.SaveLoad;

namespace Sh.Inventory.Editors
{
    [CustomEditor(typeof(InventoryBase))]
    public class InventoryExtended : Editor
    {
        InventoryBase inventory;

        private void OnEnable()
        {
            inventory = FindObjectOfType<InventoryBase>();
        }

        public override void OnInspectorGUI()
        {
            Editor editor = Editor.CreateEditor(inventory);
            editor.DrawDefaultInspector();

            if (GUILayout.Button("Clear scene persistent data"))
            {
                FindObjectOfType<SaveData>().ClearScenePersistence();
            }
        }
    }
}