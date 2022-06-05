using General;
using UnityEngine;

namespace Gameplay.Car {
    
    public class CarMover : MonoBehaviour, IMover, IResetable {
        
        [SerializeField] private Rigidbody2D _firstWheel;
        [SerializeField] private Rigidbody2D _secondeWheel;
        [SerializeField] private Rigidbody2D _carRigidbody;
        [Header("Сharacteristics")]
        [SerializeField, Range(0, 10)] private float _tarqueForce = 5;
        [SerializeField] private float _eggineForce = 50;
        
        private const float SPEEDUP_STEP = 0.35f;
        
        private float _targetSpeed;
        private float _wheelSpeed;
        private bool _canMove = true;

        public bool IsMoveing { get; private set; }
        public float CurrentEgineSpeed { get; private set; }
        public float MaxSpeed => _wheelSpeed;

        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);

        private void FixedUpdate() {
            if (_canMove && IsMoveing) {
                _firstWheel.AddTorque(_targetSpeed, ForceMode2D.Force);
                _secondeWheel.AddTorque(_targetSpeed, ForceMode2D.Force);
                
                if (CurrentEgineSpeed <= _wheelSpeed) {
                    CurrentEgineSpeed += SPEEDUP_STEP;
                }

                float dir = Mathf.Clamp01(-_targetSpeed);
                _carRigidbody.AddForce(transform.right.normalized * (dir * _eggineForce), ForceMode2D.Force);
                _carRigidbody.AddTorque(_tarqueForce * dir, ForceMode2D.Impulse);
            }
        }
        
        public void MoveRight() {
            if (!_canMove) return;
            IsMoveing = true;
            _targetSpeed = -(_wheelSpeed);
        }

        public void MoveLeft() {
            if (!_canMove) return;
            IsMoveing = true;
           _targetSpeed = (_wheelSpeed);
        }

        public void StopMoveing() {
            IsMoveing = false;
            CurrentEgineSpeed = 0;
        }

        public void SetMovementAbility(bool isAble) => _canMove = isAble;
        
        public void SetEdgineForce(float value) {
            _eggineForce = value;
            _wheelSpeed = _eggineForce / 3f;
        }

        public void Reset() {
            _carRigidbody.velocity = Vector2.zero;
            _carRigidbody.rotation = 0;
            _firstWheel.angularVelocity = 0;
            _secondeWheel.angularVelocity = 0;
            _targetSpeed = 0;
            _canMove = true;
            IsMoveing = false;
            CurrentEgineSpeed = 0;
        }
        
    }

}