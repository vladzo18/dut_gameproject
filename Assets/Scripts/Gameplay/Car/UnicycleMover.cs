using HUD;
using UnityEngine;

namespace Gameplay.Car {
    
    public class UnicycleMover : MonoBehaviour, IMover, IResetable {
        
        [SerializeField] private Rigidbody2D _wheel;
        [SerializeField] private Rigidbody2D _carRigidbody;
        [SerializeField, Range(10, 40)] private float _maxSpeed;
        
        private float _targetSpeed;
        private bool _canMove = true;

        public bool IsMoveing { get; private set; }
        
        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);
        
        private void FixedUpdate() {
            if (!_canMove) return;
            
            if (IsMoveing) {
                _wheel.AddTorque(-_targetSpeed, ForceMode2D.Force);
            }
            
            if (Mathf.Abs(_carRigidbody.rotation) > 2) {
                int dir = _carRigidbody.rotation > 0 ? -1 : 1;
                _carRigidbody.AddTorque(dir * 100, ForceMode2D.Force);
            } else {
                _carRigidbody.rotation = 0;
            }
        }

        public void MoveRight() {
            if (!_canMove) return;
            IsMoveing = true;
            _targetSpeed = (_maxSpeed);
        }

        public void MoveLeft() {
            if (!_canMove) return;
            IsMoveing = true;
            _targetSpeed = -(_maxSpeed);
        }

        public void StopMoveing() {
            IsMoveing = false;
        }

        public void SetMovementAbility(bool isAble) => _canMove = isAble;
        
        public void Reset() {
            _carRigidbody.velocity = Vector2.zero;
            _carRigidbody.rotation = 0;
            _wheel.angularVelocity = 0;
            _targetSpeed = 0;
            _canMove = true;
            IsMoveing = false;
        }
        
    }
    
}