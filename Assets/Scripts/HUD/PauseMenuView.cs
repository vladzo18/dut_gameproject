using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    
    public class PauseMenuView : MonoBehaviour {

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Canvas _pauseWindow;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _clickSound;

        //Temporary Realization
        private void Start() {
            _pauseButton.onClick.AddListener(() => {
                _pauseWindow.enabled = true;
                Time.timeScale = 0f;
                _audioSource.PlayOneShot(_clickSound);
            });
            _resumeButton.onClick.AddListener(() => {
                _pauseWindow.enabled = false;
                Time.timeScale = 1f;
                _audioSource.PlayOneShot(_clickSound);
            });
            _exitButton.onClick.AddListener(() => {
                Time.timeScale = 1f;
                _audioSource.PlayOneShot(_clickSound);
                SceneSwitcher.LoadScene(SceneSwitcher.MENU_SCENE_KEY);
            });
        }

        private void OnDestroy() {
            _pauseButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

    }

}

