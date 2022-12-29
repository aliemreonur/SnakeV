using UnityEngine.SceneManagement;
using SnakeV.Utilities;
using UnityEngine;

namespace SnakeV.Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsEdgesOn;
        public float GameSpeed { get; private set; }
        public int BridgeSpawnTime => _bridgeSpawnTime;
        [SerializeField] private int _bridgeSpawnTime = 20;

        public void GameLost()
        {
            UIManager.Instance.ActivateLostPanel();
        }

        public void GameWon()
        {
            
        }

        //use a scneemanager instead?
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnEnable()
        {
            PlayerController.Instance.OnPlayerDeath += GameLost;

        }

        private void OnDisable()
        {
            PlayerController.Instance.OnPlayerDeath -= GameLost;
        }

        protected void SetGameSpeed()
        {

        }

    }
}

