using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace SnakeV.Menu
{
    public class Options : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _edgesText;
        [SerializeField] TextMeshProUGUI _speedText;
        [SerializeField] TextMeshProUGUI _mapSizeText;

        private int _speed;
        private int _mapSize;
        private int _isEdgesOn;

        public void LoadMainMenu()
        {
            SetEdges();
            SetSpeed();
            SetMapSize();

            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }

        private void Start()
        {
            SetStartValues();
        }

        private void SetStartValues()
        {
            SetStartEdges();

            if (PlayerPrefs.HasKey("GameSpeed"))
                _speed = PlayerPrefs.GetInt("GameSpeed");
            else _speed = 50;
            if (PlayerPrefs.HasKey("MapSize"))
                _mapSize = PlayerPrefs.GetInt("MapSize");
            else _mapSize = 15;

            _speedText.text = _speed.ToString();
            _mapSizeText.text = _mapSize.ToString() + "x" + _mapSize.ToString();
        }

        private void SetStartEdges()
        {
            if (PlayerPrefs.HasKey("EdgesOn"))
            {
                _edgesText.text = PlayerPrefs.GetInt("EdgesOn") == 0 ? "OFF" : "ON";
            }
            else
            {
                _edgesText.text = "ON";
                PlayerPrefs.SetInt("EdgesOn", 1);
            }
        }

        public void ChangeEdgePreference()
        {
            _isEdgesOn++;
            _isEdgesOn = _isEdgesOn % 2;
            if (_isEdgesOn == 0)
                _edgesText.text = "OFF";
            else
                _edgesText.text = "ON";
        }

        public void IncreaseSpeed()
        {
            _speed++;
            if (_speed > 100)
                _speed = 100;
            _speedText.text = _speed.ToString();
        }

        public void DecreaseSpeed()
        {
            _speed--;
            if (_speed < 10)
                _speed = 10;
            _speedText.text = _speed.ToString();
        }

        public void IncreaseMapSize()
        {
            _mapSize++;
            if (_mapSize > 30)
                _mapSize = 30;
            _mapSizeText.text = _mapSize.ToString() + "x" + _mapSize.ToString();
        }

        public void DecreaseMapSize()
        {
            _mapSize--;
            if (_mapSize < 10)
                _mapSize = 10;
            _mapSizeText.text = _mapSize.ToString() + "x" + _mapSize.ToString();
        }

        private void SetEdges()
        {
            PlayerPrefs.SetInt("EdgesOn", _isEdgesOn);
        }

        private void SetSpeed()
        {
            PlayerPrefs.SetInt("GameSpeed", _speed);
        }

        private void SetMapSize()
        {
            PlayerPrefs.SetInt("MapSize", _mapSize);
        }
    }


}
