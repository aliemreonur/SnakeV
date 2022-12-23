using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _mapSizeText;
    private int _speed;
    private int _mapSize;

    private void Awake()
    {
        _speed = 1;
        _mapSize = 10;
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        _speed = PlayerPrefs.GetInt("GameSpeed");
        _mapSize = PlayerPrefs.GetInt("MapSize");
        _speedText.text = _speed.ToString() + "x";
        _mapSizeText.text = _mapSize.ToString() + "x" + _mapSize.ToString();
    }

    public void IncreaseSpeed()
    {
        _speed++;
        if (_speed > 10)
            _speed = 10;
        SetSpeed();
    }

    public void DecreaseSpeed()
    {
        _speed--;
        if (_speed < 1)
            _speed = 1;
        SetSpeed();
    }

    public void IncreaseMapSize()
    {
        _mapSize++;
        if (_mapSize > 30)
            _mapSize = 30;
        SetMapSize();
    }

    public void DecreaseMapSize()
    {
        _mapSize--;
        if (_mapSize < 10)
            _mapSize = 10;
        SetMapSize();
    }

    private void SetSpeed()
    {
        _speedText.text = _speed.ToString() + "x";
        PlayerPrefs.SetInt("GameSpeed", _speed);
    }

    private void SetMapSize()
    {
        _mapSizeText.text = _mapSize.ToString() +"x" + _mapSize.ToString();
        PlayerPrefs.SetInt("MapSize", _mapSize);
    }
}
