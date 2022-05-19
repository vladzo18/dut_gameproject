using System;
using System.Collections;
using UnityEngine;

namespace Scripts {
    
    public class CarTank : MonoBehaviour {

        [SerializeField] private CarCollector _carCollector;
        [SerializeField] private CarMover _carMover;
        [SerializeField] private float _fuelMaxAmount;
        [SerializeField, Range(0.5f, 2f)] private float _fuelDecreaseStep;
        [SerializeField, Range(0.005f, 0.05f)] private float _fuelDecreaseingSpeed;
        
        public float CurrentFuelAmount { get; private set; }
        public float FuelMaxAmount => _fuelMaxAmount;

        public event Action OnEmptyFuelTank;
        public event Action<float> OnFuelAmountChanged;

        private void Start() {
            _carCollector.OnFuelCollect += OnFuelTake;
            CurrentFuelAmount = FuelMaxAmount;
            StartCoroutine(FuelСonsumptionRoutine());
        }

        private void OnDestroy() {
            _carCollector.OnFuelCollect -= OnFuelTake;
        }
        
        private IEnumerator FuelСonsumptionRoutine() {
            while (true) {
                yield return new WaitUntil(() => _carMover.IsMoveing);
                
                if (CurrentFuelAmount > 0) {
                    CurrentFuelAmount -= _fuelDecreaseStep;
                    OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
                } else {
                    _carMover.ToggleMovement();
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
        
    }

}

