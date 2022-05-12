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

        //Temporary Realization
        private void Start() {
            _pauseButton.onClick.AddListener(() => {
                _pauseWindow.enabled = true;
                Time.timeScale = 0f;
            });
            _resumeButton.onClick.AddListener(() => {
                _pauseWindow.enabled = false;
                Time.timeScale = 1f;
            });
            _exitButton.onClick.AddListener(() => {
                Time.timeScale = 1f;
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

