using UI.Changers.CarChanger;
using UI.Changers.CarPropertyTuner;
using UI.Changers.MapChanger;
using UnityEngine;

namespace UI {

    public class MenuStartup : MonoBehaviour {

        [SerializeField] private MenuView _menuView;

        private MenuController _menuController;
        private MapsChanger _mapsChanger;
        private CarChanger _carChanger;
        private CarPropertiesChanger _propertiesChanger;

        private MapsModel _mapsModel;
        private CarsModel _carsModel;
        private CarPropertySettings _currentPropertySettings;

        private void Start() {
            Initialize();
            _carChanger.OnCarChanged += OnCarChangedHandler;
        }

        private void OnDestroy() {
            Dispose();
            _carChanger.OnCarChanged -= OnCarChangedHandler;
        }
        
        private void Initialize() {
            _mapsModel = new MapsModel(_menuView.MapsStorage);
            _mapsChanger = new MapsChanger(
                _menuView.ChangerItemPrefab,
                _mapsModel,
                _menuView.LevelsScroller,
                _menuView.BuyMessageBox, 
                _menuView.CurrencyBox);
            _mapsChanger.Init();
            
            _carsModel = new CarsModel(_menuView.CarsStorage);
            _carChanger = new CarChanger(
                _menuView.ChangerItemPrefab,
                _carsModel,
                _menuView.CarsScroller, 
                _menuView.BuyMessageBox,
                _menuView.CurrencyBox);
            _carChanger.Init();
            
            _currentPropertySettings = new CarPropertySettings(_carChanger.CurrentCarType);
            _propertiesChanger = new CarPropertiesChanger(
                _menuView.CarTunerBoxViews, 
                _currentPropertySettings,
                _menuView.BuyMessageBox,
                _menuView.CurrencyBox);
            _propertiesChanger.Init();
            
            _menuController = new MenuController(
                _menuView, 
                _mapsChanger, 
                _carChanger, 
                _propertiesChanger);
            _menuController.Init();
            _menuController.LoadState();
        }

        private void Dispose() {
            _mapsChanger.Dispose();
            _carChanger.Dispose();
            _propertiesChanger.Dispose();
            _menuController.Dispose();
            _menuController.SaveState();
        }
        
        private void OnCarChangedHandler(int obj) {
            _currentPropertySettings.SaveState();
            _currentPropertySettings = new CarPropertySettings(_carChanger.CurrentCarType);
            _propertiesChanger.ChangeCarPropertySettings(_currentPropertySettings);
        }
        
    }
    
}