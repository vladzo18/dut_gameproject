using General;
using UnityEngine;

namespace Gameplay.Car {
    
    public class UnicycleMover : MonoBehaviour, IMover, IResetable {
        
        [SerializeField] private Rigidbody2D _wheel;
        [SerializeField] private Rigidbody2D _carRigidbody;
        [SerializeField] private float _edgineForce;
        
        private const float SPEEDUP_STEP = 0.35f;
        
        private float _targetSpeed;
        private bool _canMove = true;

        public bool IsMoveing { get; private set; }
        public float CurrentEgineSpeed { get; private set; }
        public float MaxSpeed => _edgineForce;
        
        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);
        
        private void FixedUpdate() {
            if (!_canMove) return;
            
            if (IsMoveing) {
                _wheel.AddTorque(-_targetSpeed, ForceMode2D.Force);
                
                if (CurrentEgineSpeed <= _edgineForce) {
                    CurrentEgineSpeed += SPEEDUP_STEP;
                }
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
            _targetSpeed = (_edgineForce);
        }

        public void MoveLeft() {
            if (!_canMove) return;
            IsMoveing = true;
            _targetSpeed = -(_edgineForce);
        }

        public void StopMoveing() {
            IsMoveing = false;
        }

        public void SetMovementAbility(bool isAble) => _canMove = isAble;
        
        public void SetEdgineForce(float value) {
            _edgineForce = value;
        }

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