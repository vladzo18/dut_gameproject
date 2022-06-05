using System;
using Items;
using UnityEngine;

namespace Gameplay.Car {

    public class CarDeath : MonoBehaviour {

        [SerializeField] private CarTank _carTank;
        [SerializeField] private float _fuelEndDelayToDeath = 4;
        [SerializeField] private bool _canChechGroundTouchDeth = true;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groungLayer;

        private bool _canDeath;
        private bool _canCheckFuelDeath = true;

        public event Action OnCarDeath;

        private void Start() {
            _carTank.OnEmptyFuelTank += EmptyFuelHandler;
        }

        private void OnDestroy() {
            _carTank.OnEmptyFuelTank -= EmptyFuelHandler;
        }

        private void Update() {
            if (_canDeath) {
                OnCarDeath?.Invoke();
                _canDeath = false;
                _canChechGroundTouchDeth = false;
            }
            
            if (!_canChechGroundTouchDeth) return;
            _canDeath = Physics2D.OverlapCircle(_groundChecker.position, 0.5f, _groungLayer);
        }

        private void EmptyFuelHandler() {
            if (_canCheckFuelDeath) {
                _canCheckFuelDeath = false;
                Invoke(nameof(TryMakeFuelDeath), _fuelEndDelayToDeath);
            }
        }

        public void TryMakeFuelDeath() {
            _canCheckFuelDeath = true;
            if (_carTank.CurrentFuelAmount > 0) return;
            _canDeath = true;
        }
        
    }
    
}