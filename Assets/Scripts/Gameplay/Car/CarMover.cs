using Gameplay.Car;
using HUD;
using UnityEngine;

namespace Items {
    
    public class CarMover : MonoBehaviour, IMover, IResetable {
        
        [SerializeField] private Rigidbody2D _firstWheel;
        [SerializeField] private Rigidbody2D _secondeWheel;
        [SerializeField] private Rigidbody2D _carRigidbody;
        [SerializeField, Range(25, 50)] private float _maxSpeed;

        private const float SPEED_DIVIDER = 2.5f;
        private const float TORQUE_DIVIDER = 8;
        
        private float _targetSpeed;
        private bool _canMove = true;

        public bool IsMoveing { get; private set; }

        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);

        private void FixedUpdate() {
            if (_canMove && IsMoveing) {
                _firstWheel.AddTorque(_targetSpeed, ForceMode2D.Force);
                _secondeWheel.AddTorque(_targetSpeed, ForceMode2D.Force);
                
                _carRigidbody.AddForce(transform.right.normalized * -_targetSpeed / SPEED_DIVIDER, ForceMode2D.Force);
                _carRigidbody.AddTorque(-_targetSpeed / TORQUE_DIVIDER, ForceMode2D.Impulse);
            }
            
        }
        
        public void MoveRight() {
            if (!_canMove) return;
            IsMoveing = true;
            _targetSpeed = -(_maxSpeed);
        }

        public void MoveLeft() {
            if (!_canMove) return;
            IsMoveing = true;
           _targetSpeed = (_maxSpeed);
        }

        public void StopMoveing() {
            IsMoveing = false;
        }

        public void SetMovementAbility(bool isAble) => _canMove = isAble;

        public void Reset() {
            _carRigidbody.velocity = Vector2.zero;
            _carRigidbody.rotation = 0;
            _firstWheel.angularVelocity = 0;
            _secondeWheel.angularVelocity = 0;
            _targetSpeed = 0;
            _canMove = true;
            IsMoveing = false;
        }
        
    }

}