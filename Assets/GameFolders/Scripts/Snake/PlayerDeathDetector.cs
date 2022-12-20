using UnityEngine;

namespace SnakeV.Core
{
    public class PlayerDeathDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController.Instance.Death();
            }
        }
    }
}


