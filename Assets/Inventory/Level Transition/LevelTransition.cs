using UnityEngine;
using Sh.Inventory.SaveLoad;
using UnityEngine.SceneManagement;

namespace Sh.Inventory
{
    public class LevelTransition : MonoBehaviour
    {
        public int sceneId;

        private void OnTriggerEnter(Collider other)
        {
            FindObjectOfType<SaveData>().SaveLevelPeristence();
            SceneManager.LoadScene(sceneId);
        }
    }
}