
using UnityEngine.SceneManagement;
using SnakeV.Utilities;

namespace SnakeV.Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsEdgesOn;

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

    }
}

