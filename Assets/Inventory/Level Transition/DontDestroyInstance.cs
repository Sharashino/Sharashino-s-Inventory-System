using UnityEngine;

namespace Sh.Inventory.SaveLoad
{
    public class DontDestroyInstance : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
