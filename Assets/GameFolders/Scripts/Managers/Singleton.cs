
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

    /*
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        
        protected void SingletonThisObj(T entity)
        {
            if (Instance == null)
            {
                Instance = entity;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
        */

}


