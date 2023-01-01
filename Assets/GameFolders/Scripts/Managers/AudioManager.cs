using UnityEngine;
using SnakeV.Utilities;

namespace SnakeV.Core
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip _eatFoodClip;
        [SerializeField] private AudioClip _crashClip;
        private PlayerController _playerController;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                Debug.Log("The collectable has no audio source attached!");
            _playerController = PlayerController.Instance;
        }

        public void OnSnakeAte()
        {
            PlayClip(_eatFoodClip);
        }

        public void OnSnakeCrash()
        {
            PlayClip(_crashClip);
        }

        private void PlayClip(AudioClip _audioClip)
        {
            if (_audioClip != null)
                _audioSource.PlayOneShot(_audioClip);
        }

        private void OnEnable()
        {
            _playerController.OnPlayerAte += OnSnakeAte;
            _playerController.OnPlayerDeath += OnSnakeCrash;
        }

        private void OnDisable()
        {
            _playerController.OnPlayerAte -= OnSnakeAte;
            _playerController.OnPlayerDeath -= OnSnakeCrash;
        }
    }

}


