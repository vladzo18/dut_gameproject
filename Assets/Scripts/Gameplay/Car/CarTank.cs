using System;
using System.Collections;
using Gameplay.Car;
using HUD;
using UnityEngine;

namespace Items {
    
    public class CarTank : MonoBehaviour, IResetable {

        [SerializeField] private CarCollector _carCollector;
        [SerializeField] private MonoBehaviour _carMover;
        [SerializeField] private float _fuelMaxAmount;
        [SerializeField, Range(0.5f, 2f)] private float _fuelDecreaseStep;
        [SerializeField, Range(0.005f, 0.05f)] private float _fuelDecreaseingSpeed;

        private IMover _mover;
        private bool isFuelEmpty;

        public float CurrentFuelAmount { get; private set; }
        public float FuelMaxAmount => _fuelMaxAmount;

        public event Action OnEmptyFuelTank;
        public event Action<float> OnFuelAmountChanged;

        private void Start() {
            GameReset.Register(this);
                
            if (_carMover) {
                _mover = _carMover as IMover;
            }
            
            _carCollector.OnFuelCollect += OnFuelTake;
            
            CurrentFuelAmount = FuelMaxAmount;
            StartCoroutine(FuelСonsumptionRoutine());
        }

        private void OnDestroy() {
            _carCollector.OnFuelCollect -= OnFuelTake;
            GameReset.Unregister(this);
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
                    if (!isFuelEmpty) {
                        _mover.SetMovementAbility(false);
                        OnEmptyFuelTank?.Invoke();
                        isFuelEmpty = true;
                    }
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
            _mover.SetMovementAbility(true);
            isFuelEmpty = false;
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
        }
        
        private void OnValidate() {
            if (!(_carMover is IMover)) {
                _carMover = null;
            }
        }

        public void Reset() {
            CurrentFuelAmount = FuelMaxAmount;
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
        }
        
    }

}

