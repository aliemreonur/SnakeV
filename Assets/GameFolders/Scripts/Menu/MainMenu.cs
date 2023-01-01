using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeV.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadOptions()
        {
            SceneManager.LoadScene(2);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

