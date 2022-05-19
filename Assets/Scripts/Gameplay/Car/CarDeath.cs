using System;
using Scripts;
using UnityEngine;

namespace Gameplay.Car {
    
    public class CarDeath : MonoBehaviour {

        [SerializeField] private CarTank _carTank;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groungLayer;
        
        private bool _canDeath;
        
        public event Action OnCatDeath;

        private void Start() {
            _carTank.OnEmptyFuelTank += EmptyFuelHandler;
        }
        
        private void OnDestroy() {
            _carTank.OnEmptyFuelTank -= EmptyFuelHandler;
        }

        private void Update() {
            if (_canDeath) {
                OnCatDeath?.Invoke();
            }
            _canDeath = Physics2D.OverlapCircle(_groundChecker.position, 0.5f, _groungLayer);
        }

        private void EmptyFuelHandler() {
            _canDeath = true;
        }
        
    }
    
}