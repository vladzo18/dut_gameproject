using System;
using Scripts;
using Scripts.Save;
using UI;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.LevelChanger;
using UnityEngine;

namespace Ui {
    
    public class MenuController : MonoBehaviour {

        [SerializeField] private MenuView _menuView;
        
        private Canvas _activeCanvas;

        private LevelChanger _levelChanger;
        private CarChanger _carChanger;

        private ISaveSystem<MenuSaveData> _saveSystem;
        
        private void Start() {
            _saveSystem = new MenuPlayerPrefsSystem();
            _menuView.OnPlayClick += OnPlayClicked;
            foreach (var button in _menuView.SwichButtons) {
                button.OnButtonClick += OnChangerButtonClick;
            }

            LoadState();
            TryToLoadLeverProgresData();
            
            _levelChanger = new LevelChanger(
                _menuView.ChangerItemPrefab,
                _menuView.MapsStorage,
                _menuView.LevelsScroller,
                _menuView.MessageBox);
            
            _carChanger = new CarChanger(
                _menuView.ChangerItemPrefab,
                _menuView.CarsStorage,
                _menuView.CarsScroller, 
                _menuView.MessageBox);
            
            _activeCanvas = _menuView.LevelsCanvas;
            _activeCanvas.enabled = true;
        }
        
        private void OnDestroy() {
            SaveState();
            _menuView.OnPlayClick -= OnPlayClicked;
            foreach (var button in _menuView.SwichButtons) {
                button.OnButtonClick += OnChangerButtonClick;
            }
            _levelChanger.Dispose();
            _carChanger.Dispose();
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

        private void SaveState() {
            MenuSaveData saveData = new MenuSaveData();
            saveData.CoinsAmount = _menuView.CurrencyBox.CurrentCoins;
            saveData.DiamantsAmount = _menuView.CurrencyBox.CurrentDiamonts;
            _saveSystem.SaveData(saveData);
        }

        private void LoadState() {
            MenuSaveData saveData = _saveSystem.LoadData();
            _menuView.CurrencyBox.AddCoins(saveData.CoinsAmount);
            _menuView.CurrencyBox.AddDiamonts(saveData.DiamantsAmount);
        }
        
        private void OnPlayClicked() { 
            SaveState();
            SceneSwitcher.LoadScene(SceneSwitcher.GAME_SCENE_KEY);
        }

        private void TryToLoadLeverProgresData() {
            LevelSaveData saveData = (new LevelPlayerPrefsSystem()).LoadData();
            if (saveData == null) return;

            _menuView.CurrencyBox.AddCoins(saveData.CoinsAmount);
            _menuView.CurrencyBox.AddDiamonts(saveData.DiamontsAmount);
        }
        
    }
    
}


