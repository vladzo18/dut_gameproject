using System;
using System.Collections;
using Gameplay.Car;
using UnityEngine;

namespace Items {
    
    public class CarTank : MonoBehaviour {

        [SerializeField] private CarCollector _carCollector;
        [SerializeField] private MonoBehaviour _carMover;
        [SerializeField] private float _fuelMaxAmount;
        [SerializeField, Range(0.5f, 2f)] private float _fuelDecreaseStep;
        [SerializeField, Range(0.005f, 0.05f)] private float _fuelDecreaseingSpeed;

        private IMover _mover;
        
        public float CurrentFuelAmount { get; private set; }
        public float FuelMaxAmount => _fuelMaxAmount;

        public event Action OnEmptyFuelTank;
        public event Action<float> OnFuelAmountChanged;

        private void Start() {
            if (_carMover) {
                _mover = _carMover as IMover;
            }
            
            _carCollector.OnFuelCollect += OnFuelTake;
            
            CurrentFuelAmount = FuelMaxAmount;
            StartCoroutine(FuelСonsumptionRoutine());
        }

        private void OnDestroy() {
            _carCollector.OnFuelCollect -= OnFuelTake;
        }
        
        private IEnumerator FuelСonsumptionRoutine() {
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
            
            while (true) {
                yield return new WaitUntil(() => _mover.IsMoveing);
                
                if (CurrentFuelAmount > 0) {
                    float nextFuelAmount = CurrentFuelAmount - _fuelDecreaseStep;
                    CurrentFuelAmount = nextFuelAmount >= 0 ? nextFuelAmount : 0;
                    OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
                } else {
                    _mover.ToggleMovement();
                    OnEmptyFuelTank?.Invoke();
                }
                
                yield return new WaitForSeconds(_fuelDecreaseingSpeed);
            }
        }
        
        private void OnFuelTake(float amount) {
            if ( CurrentFuelAmount + amount > FuelMaxAmount) {
                CurrentFuelAmount = FuelMaxAmount;
            } else {
                CurrentFuelAmount += amount;
            }
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
        }
        
        private void OnValidate() {
            if (!(_carMover is IMover)) {
                _carMover = null;
            }
        }
        
    }

}

