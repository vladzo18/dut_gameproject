using System;
using General;
using Save;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.CarPropertyTuner;
using UI.Changers.MapChanger;
using UnityEngine;

namespace UI {
    
    public class MenuController {

        private readonly MenuView _menuView;
        
        private Canvas _activeCanvas;

        private readonly MapsChanger _mapsChanger;
        private readonly CarChanger _carChanger;
        private readonly CarPropertiesChanger _propertiesChanger;

        public MenuController(MenuView menuView, MapsChanger mapsChanger, CarChanger carChanger, CarPropertiesChanger propertiesChanger) {
            _menuView = menuView;
            _mapsChanger = mapsChanger;
            _carChanger = carChanger;
            _propertiesChanger = propertiesChanger;
        }

        public void Init() {
            _menuView.OnPlayClick += OnPlayClicked;
            foreach (var button in _menuView.SwitchButtons) {
                button.OnButtonClick += OnChangerButtonClick;
            }
            _mapsChanger.OnMapChanged += MapChangedHandler;
            _mapsChanger.OnMapBuy += BuyCarOrMap;
            _carChanger.OnCarBuy += BuyCarOrMap;
            _carChanger.OnCarChanged += CarChangedHandler;
            _activeCanvas = _menuView.LevelsCanvas;
            _activeCanvas.enabled = true;
            _propertiesChanger.OnPointsChanged += PointsChangedHandler;
            _propertiesChanger.OnUpgradeBought += OnUpgradeBoughtHandler;
        }
        
        public void Dispose() {
            _menuView.OnPlayClick -= OnPlayClicked;
            _mapsChanger.OnMapChanged -= MapChangedHandler;
            _carChanger.OnCarChanged -= CarChangedHandler;
            _propertiesChanger.OnPointsChanged -= PointsChangedHandler;
            _propertiesChanger.OnUpgradeBought -= OnUpgradeBoughtHandler;
            _carChanger.OnCarBuy -= BuyCarOrMap;
            _mapsChanger.OnMapBuy -= BuyCarOrMap;
            foreach (var button in _menuView.SwitchButtons) {
                button.OnButtonClick -= OnChangerButtonClick;
            }
        }
        
        public void SaveState() {
            var saveData = new MenuSaveData {
                CoinsAmount =  _menuView.CurrencyBox.CurrentCoins,
                DiamondsAmount = _menuView.CurrencyBox.CurrentDiamonds,
                ChosenCarType = _carChanger.CurrentCarType,
                ChosenMapType = _mapsChanger.CurrentMapType
            };
            PlayerPrefsSaver.MenuSaveData.Set(saveData);
        }

        public void LoadState() {
            var saveData = PlayerPrefsSaver.MenuSaveData.Get();
            _menuView.CurrencyBox.AddCoins(saveData.CoinsAmount);
            _menuView.CurrencyBox.AddDiamonds(saveData.DiamondsAmount);
            TryToLoadLevelProgressData();
        }

        private void OnChangerButtonClick(ChangerSwitchButtonType type) {
            _activeCanvas.enabled = false;
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
            
            switch (type) {
                case ChangerSwitchButtonType.Levels:
                    _menuView.LevelsCanvas.enabled = true;
                    _activeCanvas = _menuView.LevelsCanvas;
                    break;
                case ChangerSwitchButtonType.Cars:
                    _menuView.CarsCanvas.enabled = true;
                    _activeCanvas = _menuView.CarsCanvas;
                    break;
                case ChangerSwitchButtonType.Tune:
                    _menuView.TuneCanvas.enabled = true;
                    _activeCanvas = _menuView.TuneCanvas;
                    break;
                case ChangerSwitchButtonType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private void OnUpgradeBoughtHandler() => _menuView.UIAudioSource.PlayOneShot(_menuView.BuySound);
        
        private void BuyCarOrMap() => _menuView.UIAudioSource.PlayOneShot(_menuView.BuySound);
        
        private void PointsChangedHandler() => _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
        
        private void MapChangedHandler(int index) => _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
        
        private void CarChangedHandler(int index) => _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
        
        private void OnPlayClicked() {
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
            SceneSwitcher.LoadScene(SceneSwitcher.GAME_SCENE_KEY);
        }

        private void TryToLoadLevelProgressData() {
            var saveData = PlayerPrefsSaver.LevelSaveData.Get();
            _menuView.CurrencyBox.AddCoins(saveData.CoinsAmount);
            _menuView.CurrencyBox.AddDiamonds(saveData.DiamondsAmount);
            PlayerPrefsSaver.LevelSaveData.Delete();
        }
        
    }
    
}


