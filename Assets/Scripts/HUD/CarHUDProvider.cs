using Gameplay.Car;
using Items.Save;
using UnityEngine;

namespace HUD {
    
    public class CarHUDProvider : MonoBehaviour {

        [SerializeField] private CurrencyBox _currencyBox;
        [SerializeField] private FuelBar _fuelBar;
        [SerializeField] private GameEndPerformance _gameEndPerformance;
        [SerializeField] private MeterCountBar _meterCountBar;
        [SerializeField] private PauseMenu _pauseMenu;

        [SerializeField] private CarEntity _carEntity;
        
        private bool _carEntityWasSetted;
        
        public void SetCarEntity(CarEntity entity) {
            _carEntity = entity;
            _carEntityWasSetted = true;
            BindWitnCarEntity();
            _pauseMenu.OnGameExit += GameExitHandler;
        }
        
        private void Start() {
            if (!_carEntityWasSetted) {
                BindWitnCarEntity();
                _pauseMenu.OnGameExit += GameExitHandler;
            }
        }
        
        private void OnDestroy() {
            if (_carEntity) {
                DisposeCarEntityBinding();
                _pauseMenu.OnGameExit -= GameExitHandler;
            }
        }
        
        private void BindWitnCarEntity () {
            _carEntity.CarCollector.OnCoinCollect += CoinCollectHandler;
            _carEntity.CarCollector.OnDiamontCollect += DiamontsCollectHandler;
            _carEntity.CarDistanceCounter.OnMeterCountChanged += MeterCountChangedHandler;
            
            _carEntity.CarTank.OnFuelAmountChanged += FuelAmountChangedHandler;
            _fuelBar.MaxFuelAmount = _carEntity.CarTank.FuelMaxAmount;
            _fuelBar.SetFuelValue(_carEntity.CarTank.CurrentFuelAmount);

            _carEntity.CarDeath.OnCarDeath += CarDeathHandler;
        }

        private void DisposeCarEntityBinding() {
            _carEntity.CarCollector.OnCoinCollect -= CoinCollectHandler;
            _carEntity.CarCollector.OnDiamontCollect -= DiamontsCollectHandler;
            _carEntity.CarDistanceCounter.OnMeterCountChanged -= MeterCountChangedHandler;
            _carEntity.CarTank.OnFuelAmountChanged -= FuelAmountChangedHandler;
            _carEntity.CarDeath.OnCarDeath -= CarDeathHandler;
        }

        private void CoinCollectHandler(float value) => _currencyBox.AddCoins(value);
        private void DiamontsCollectHandler(float value) => _currencyBox.AddDiamants(value);
        private void MeterCountChangedHandler() => _meterCountBar.SetMeters(_carEntity.CarDistanceCounter.MeterCount);
        private void FuelAmountChangedHandler(float value) => _fuelBar.SetFuelValue(value);
        
        private void CarDeathHandler() {
            LevelSaveData saveData = SaveAndGetLevelProgres();
            _gameEndPerformance.ShowGameEndWindow(saveData);
        }
        
        private void GameExitHandler() {
            SaveAndGetLevelProgres();
        }
        
        private LevelSaveData SaveAndGetLevelProgres() {
            LevelSaveData saveData = new LevelSaveData();
            saveData.CoinsAmount = _currencyBox.CoinsAmount;
            saveData.DiamontsAmount = _currencyBox.DiamontsAmount;
            saveData.DrivenMeters = _meterCountBar.CurrentMetersCount;
            (new LevelPlayerPrefsSystem()).SaveData(saveData);
            return saveData;
        }
    }
    
}