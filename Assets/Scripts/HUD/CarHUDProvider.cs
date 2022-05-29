using Gameplay.Car;
using UnityEngine;

namespace HUD {
    
    public class CarHUDProvider : MonoBehaviour {

        [SerializeField] private CurrencyBox currencyBox;
        [SerializeField] private FuelBar fuelBar;
        [SerializeField] private GameEndPerformance gameEndPerformance;
        [SerializeField] private MeterCountBar meterCountBar;

        [SerializeField] private CarEntity _carEntity;

        private bool _carEntityWasSetted;
        
        public void SetCarEntity(CarEntity entity) {
            _carEntity = entity;
            _carEntityWasSetted = true;
            BindWitnCarEntity();
        }
        
        private void Start() {
            if (!_carEntityWasSetted) {
                BindWitnCarEntity();
            }
        }
        
        private void OnDestroy() {
            if (_carEntity) {
                DisposeCarEntityBinding();
            }
        }

        private void BindWitnCarEntity () {
            _carEntity.CarCollector.OnCoinCollect += CoinCollectHandler;
            _carEntity.CarCollector.OnDiamontCollect += DiamontsCollectHandler;
            _carEntity.CarDistanceCounter.OnMeterCountChanged += MeterCountChangedHandler;
            
            _carEntity.CarTank.OnFuelAmountChanged += FuelAmountChangedHandler;
            fuelBar.MaxFuelAmount = _carEntity.CarTank.FuelMaxAmount;
            fuelBar.SetFuelValue(_carEntity.CarTank.CurrentFuelAmount);

            _carEntity.CarDeath.OnCatDeath += CatDeathHandler;
        }

        private void DisposeCarEntityBinding() {
            _carEntity.CarCollector.OnCoinCollect -= CoinCollectHandler;
            _carEntity.CarCollector.OnDiamontCollect -= DiamontsCollectHandler;
            _carEntity.CarDistanceCounter.OnMeterCountChanged -= MeterCountChangedHandler;
            _carEntity.CarTank.OnFuelAmountChanged -= FuelAmountChangedHandler;
            _carEntity.CarDeath.OnCatDeath -= CatDeathHandler;
        }

        private void CoinCollectHandler(float value) => currencyBox.AddCoins(value);
        private void DiamontsCollectHandler(float value) => currencyBox.AddDiamants(value);
        private void MeterCountChangedHandler() => meterCountBar.AddOneMeter(_carEntity.CarDistanceCounter.MeterCount);
        private void FuelAmountChangedHandler(float value) => fuelBar.SetFuelValue(value);
        private void CatDeathHandler() => gameEndPerformance.ShowGameEndWindow();
        
    }
    
}