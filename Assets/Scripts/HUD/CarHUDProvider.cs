using Gameplay.Car;
using Save;
using UnityEngine;

namespace HUD {
    
    public class CarHudProvider : MonoBehaviour {

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
            BindWithCarEntity();
            _pauseMenu.OnGameExit += GameExitHandler;
        }
        
        private void Start() {
            if (!_carEntityWasSetted) {
                BindWithCarEntity();
                _pauseMenu.OnGameExit += GameExitHandler;
            }
        }
        
        private void OnDestroy() {
            if (_carEntity) {
                DisposeCarEntityBinding();
                _pauseMenu.OnGameExit -= GameExitHandler;
            }
        }
        
        private void BindWithCarEntity () {
            _carEntity.CarCollector.OnCoinCollect += CoinCollectHandler;
            _carEntity.CarCollector.OnDiamondCollect += DiamondsCollectHandler;
            _carEntity.CarDistanceCounter.OnMeterCountChanged += MeterCountChangedHandler;
            
            _carEntity.CarTank.OnFuelAmountChanged += FuelAmountChangedHandler;
            _fuelBar.MaxFuelAmount = _carEntity.CarTank.FuelMaxAmount;
            _fuelBar.SetFuelValue(_carEntity.CarTank.CurrentFuelAmount);

            _carEntity.CarDeath.OnCarDeath += CarDeathHandler;
        }

        private void DisposeCarEntityBinding() {
            _carEntity.CarCollector.OnCoinCollect -= CoinCollectHandler;
            _carEntity.CarCollector.OnDiamondCollect -= DiamondsCollectHandler;
            _carEntity.CarDistanceCounter.OnMeterCountChanged -= MeterCountChangedHandler;
            _carEntity.CarTank.OnFuelAmountChanged -= FuelAmountChangedHandler;
            _carEntity.CarDeath.OnCarDeath -= CarDeathHandler;
        }

        private void CoinCollectHandler(float value) => _currencyBox.AddCoins(value);
        private void DiamondsCollectHandler(float value) => _currencyBox.AddDiamonds(value);
        private void MeterCountChangedHandler() => _meterCountBar.SetMeters(_carEntity.CarDistanceCounter.MeterCount);
        private void FuelAmountChangedHandler(float value) => _fuelBar.SetFuelValue(value);
        
        private void CarDeathHandler() {
            var saveData = SaveAndGetLevelProgress();
            _gameEndPerformance.ShowGameEndWindow(saveData);
        }
        
        private void GameExitHandler() {
            SaveAndGetLevelProgress();
        }
        
        private LevelSaveData SaveAndGetLevelProgress() {
            var saveData = new LevelSaveData {
                CoinsAmount = _currencyBox.CoinsAmount,
                DiamondsAmount = _currencyBox.DiamondsAmount,
                DrivenMeters = _meterCountBar.CurrentMetersCount
            };
            PlayerPrefsSaver.LevelSaveData.Set(saveData);
            return saveData;
        }
    }
    
}