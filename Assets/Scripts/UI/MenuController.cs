using Scripts;
using UI;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.LevelChanger;
using UnityEngine;

namespace Ui {
    
    public class MenuController : MonoBehaviour {

        [SerializeField] private MenuView _menuView;
        
        private Canvas _activeCanvas;
        
        private void Start() {
            _menuView.OnPlayClick += OnPlayClicked;
            foreach (var button in _menuView.SwichButtons) {
                button.OnButtonClick += OnChangerButtonClick;
            }

            LevelChangerController levelController = new LevelChangerController(
                _menuView.ChangerItemPrefab,
                _menuView.MapsStorage,
                _menuView.LevelsScroller);
            
            CarChangerController carController = new CarChangerController(
                _menuView.ChangerItemPrefab,
                _menuView.CarsStorage,
                _menuView.CarsScroller);
            
            _activeCanvas = _menuView.LevelsCanvas;
            _activeCanvas.enabled = true;
        }
        
        private void OnDestroy() {
            _menuView.OnPlayClick -= OnPlayClicked;
            foreach (var button in _menuView.SwichButtons) {
                button.OnButtonClick += OnChangerButtonClick;
            }
        }

        private void OnChangerButtonClick(ChangerSwichButtonType type) {
            _activeCanvas.enabled = false;
            switch (type) {
                case ChangerSwichButtonType.Levels:
                    _menuView.LevelsCanvas.enabled = true;
                    _activeCanvas = _menuView.LevelsCanvas;
                    break;
                case ChangerSwichButtonType.Cars:
                    _menuView.CarsCanvas.enabled = true;
                    _activeCanvas = _menuView.CarsCanvas;
                    break;
                case ChangerSwichButtonType.Tune:
                    _menuView.TuneCanvas.enabled = true;
                    _activeCanvas = _menuView.TuneCanvas;
                    break;
            }
        }

        private void OnPlayClicked() {
           SceneSwitcher.LoadScene(SceneSwitcher.GAME_SCENE_KEY);
        }
        
    }
    
}


