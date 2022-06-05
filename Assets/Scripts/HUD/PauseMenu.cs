using System;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    
    public class PauseMenu : MonoBehaviour {

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Canvas _pauseWindow;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _clickSound;

        public event Action OnGameExit;
        
        private void Start() {
            _pauseButton.onClick.AddListener(() => {
                _pauseWindow.enabled = true;
                PauseProvider.ApplyPause();
                _audioSource.PlayOneShot(_clickSound);
            });
            _resumeButton.onClick.AddListener(() => {
                _pauseWindow.enabled = false;
                PauseProvider.RevokePause();
                _audioSource.PlayOneShot(_clickSound);
            });
            _restartButton.onClick.AddListener(() => {
                PauseProvider.RevokePause();
                _audioSource.PlayOneShot(_clickSound);
                GameReset.Reset();
                _pauseWindow.enabled = false;
            });
            _exitButton.onClick.AddListener(() => {
                _audioSource.PlayOneShot(_clickSound);
                PauseProvider.RevokePause();
                OnGameExit?.Invoke();
                SceneSwitcher.LoadScene(SceneSwitcher.MENU_SCENE_KEY);
            });
        }

        private void OnDestroy() {
            _pauseButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

    }

}

