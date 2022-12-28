using UnityEngine;
using SnakeV.Abstracts;

namespace SnakeV.Core
{
    public class PlayerDeathDetector : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IControllable>(out IControllable controllable))
            {
                controllable.Death();
            }
        }
        
    }
}


