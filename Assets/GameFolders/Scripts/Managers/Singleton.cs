using UnityEngine;

namespace SnakeV.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        public static T Instance;

        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}


