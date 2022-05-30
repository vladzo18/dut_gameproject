using Items;
using Items.Save;
using UI;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.CarPropertyTuner;
using UI.Changers.LevelChanger;
using UnityEngine;

namespace Ui {
    
    public class MenuController : MonoBehaviour {

        [SerializeField] private MenuView _menuView;
        
        private Canvas _activeCanvas;

        private MapsChanger _mapsChanger;
        private CarChanger _carChanger;
        private CarPropertiesChanger _propertiesChanger;

        private MapsModel _mapsModel;
        private CarsModel _carsModel;

        private ISaveSystem<MenuSaveData> _saveSystem;

        private int _currentMapIndex;
        private int _currentCarIndex;
        
        private void Start() {
            _saveSystem = new MenuPlayerPrefsSystem();
            _menuView.OnPlayClick += OnPlayClicked;
            foreach (var button in _menuView.SwichButtons) {
                button.OnButtonClick += OnChangerButtonClick;
            }

            LoadState();
            TryToLoadLeverProgresData();
            
            _mapsModel = new MapsModel(_menuView.MapsStorage);
            _mapsModel.LoadModel();
            _carsModel = new CarsModel(_menuView.CarsStorage);
            _carsModel.LoadModel();
            
            _mapsChanger = new MapsChanger(
                _menuView.ChangerItemPrefab,
                _mapsModel,
                _menuView.LevelsScroller,
                _menuView.MessageBox, 
                _menuView.CurrencyBox);
            _mapsChanger.Init(_currentMapIndex);
            
            _carChanger = new CarChanger(
                _menuView.ChangerItemPrefab,
                _carsModel,
                _menuView.CarsScroller, 
                _menuView.MessageBox,
                _menuView.CurrencyBox);
            _carChanger.Init(_currentCarIndex);
            
            _propertiesChanger = new CarPropertiesChanger(_menuView.CarTunerBoxViews);
            _propertiesChanger.Init();
            
            _mapsChanger.OnMapChanged += MapChangedHandler;
            _carChanger.OnCarChanged += CarChangedHandler;
            _activeCanvas = _menuView.LevelsCanvas;
            _activeCanvas.enabled = true;
            _propertiesChanger.OnPointsChanged += PointsChangedHandler;
            _carChanger.OnCarBuy += BuyCamOrMapChanger;
            _mapsChanger.OnMapBuy += BuyCamOrMapChanger;
        }

        private void BuyCamOrMapChanger() {
            _menuView.UIAudioSource.PlayOneShot(_menuView.BuySound);
        }

        private void PointsChangedHandler() {
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
        }

        private void OnDestroy() {
            SaveState();
            _menuView.OnPlayClick -= OnPlayClicked;
            _mapsChanger.OnMapChanged -= MapChangedHandler;
            _carChanger.OnCarChanged -= CarChangedHandler;
            _propertiesChanger.OnPointsChanged -= PointsChangedHandler;
            _carChanger.OnCarBuy -= BuyCamOrMapChanger;
            _mapsChanger.OnMapBuy -= BuyCamOrMapChanger;
            foreach (var button in _menuView.SwichButtons) {
                button.OnButtonClick -= OnChangerButtonClick;
            }
            _mapsChanger.Dispose();
            _carChanger.Dispose();
            _propertiesChanger.Dispose();
        }

        private void OnChangerButtonClick(ChangerSwichButtonType type) {
            _activeCanvas.enabled = false;
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
            
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
        
        private void MapChangedHandler(int index) {
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
            _currentMapIndex = index;
        }
        
        private void CarChangedHandler(int index) {
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
            _currentCarIndex = index;
        }
        
        private void SaveState() {
            MenuSaveData saveData = new MenuSaveData();
            saveData.CoinsAmount = _menuView.CurrencyBox.CurrentCoins;
            saveData.DiamantsAmount = _menuView.CurrencyBox.CurrentDiamonts;
            saveData.ChosenMapIndex = _currentMapIndex;
            saveData.ChosenCarIndex = _currentCarIndex;
            _saveSystem.SaveData(saveData);
            _mapsModel.SaveModel();
            _carsModel.SaveModel();
        }

        private void LoadState() {
            MenuSaveData saveData = _saveSystem.LoadData();
            _menuView.CurrencyBox.AddCoins(saveData.CoinsAmount);
            _menuView.CurrencyBox.AddDiamonts(saveData.DiamantsAmount);
            _currentMapIndex = saveData.ChosenMapIndex;
            _currentCarIndex = saveData.ChosenCarIndex;
        }
        
        private void OnPlayClicked() {
            _menuView.UIAudioSource.PlayOneShot(_menuView.ClickSound);
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


