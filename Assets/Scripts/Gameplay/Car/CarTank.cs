using System;
using System.Collections;
using General;
using UnityEngine;

namespace Gameplay.Car {
    
    public class CarTank : MonoBehaviour, IResettable {

        [SerializeField] private CarCollector _carCollector;
        [SerializeField] private MonoBehaviour _carMover;
        [SerializeField] private float _fuelMaxAmount;
        [SerializeField, Range(0.5f, 2f)] private float _fuelDecreaseStep;
        [SerializeField, Range(0.005f, 0.05f)] private float _fuelDecreaseingSpeed;

        private Coroutine _fuelСonsumptionCoroutine;
        private IMover _mover;
        private bool _isFuelEmpty;

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
            _fuelСonsumptionCoroutine = StartCoroutine(FuelСonsumptionRoutine());
        }

        private void OnDestroy() {
            _carCollector.OnFuelCollect -= OnFuelTake;
            StopCoroutine(_fuelСonsumptionCoroutine);
            GameReset.Unregister(this);
        }
        
        public void Reset() {
            CurrentFuelAmount = FuelMaxAmount;
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
        }

        public void SetFuelMaxAmount(float value) {
            _fuelMaxAmount = value;
        }
        
        private IEnumerator FuelСonsumptionRoutine() {
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
            
            while (true) {
                yield return new WaitUntil(() => _mover.IsMoving);
                
                if (CurrentFuelAmount > 0) {
                    float nextFuelAmount = CurrentFuelAmount - _fuelDecreaseStep;
                    CurrentFuelAmount = nextFuelAmount >= 0 ? nextFuelAmount : 0;
                    OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
                } else {
                    if (!_isFuelEmpty) {
                        _mover.SetMovementAbility(false);
                        OnEmptyFuelTank?.Invoke();
                        _isFuelEmpty = true;
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
            _isFuelEmpty = false;
            OnFuelAmountChanged?.Invoke(CurrentFuelAmount);
        }
        
        private void OnValidate() {
            if (!(_carMover is IMover)) {
                _carMover = null;
            }
        }

    }

}

