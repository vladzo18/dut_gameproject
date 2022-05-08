using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car {
    
    public class CarMover : MonoBehaviour {
        
        [SerializeField] private List<HingeJoint2D> _wheels;
        [SerializeField] private Rigidbody2D _carRigidbody;
        [SerializeField, Range(100, 250)] private int _maxSpeed = 120;
        [SerializeField, Range(5, 10)] private float _torgue = 5f;
        [SerializeField, Range(0.1f, 2)] private float _accelerationSpeed;

        private const int SPEED_FACTOR = 10;
        private const int MAX_MOTOR_TORQUE = 100;

        private JointMotor2D _motor;
        private float _targetTorgue;
        private Coroutine _currentMoveCoritine;
        private bool _isMoveing;

        private void Start() {
            _motor.maxMotorTorque = MAX_MOTOR_TORQUE;
            UpdateWheelMotors();
        }

        #region ForTestMovement
        private void Update() {
            if (Input.GetKeyDown(KeyCode.D)) {
                MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                MoveLeft();
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
                StopMoveing();
            }
        }
        #endregion

        private void FixedUpdate() {
            if (_isMoveing) {
                _carRigidbody.AddTorque(_targetTorgue, ForceMode2D.Impulse);
            }
        }

        private IEnumerator MoveCorunine(int startSpeed, int targetSpeed, float acceleration) {
            float elapsedTime = 0;

            while (elapsedTime <= acceleration) {
                _motor.motorSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / acceleration);
                UpdateWheelMotors();
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _motor.motorSpeed = targetSpeed;
        }

        private void UpdateWheelMotors() {
            foreach (var wheel in _wheels) {
                wheel.motor = _motor;
            }
        }

        public void MoveRight() {
            _isMoveing = true;
            int targetSpeed = (_maxSpeed * SPEED_FACTOR);   
            if (_currentMoveCoritine != null) {
                StopCoroutine(_currentMoveCoritine);
            }
            _currentMoveCoritine = StartCoroutine(MoveCorunine((int) _motor.motorSpeed, targetSpeed, _accelerationSpeed));
            _targetTorgue = _torgue;
        }

        public void MoveLeft() {
            _isMoveing = true;
            int targetSpeed = -(_maxSpeed * SPEED_FACTOR);
            if (_currentMoveCoritine != null) {
                StopCoroutine(_currentMoveCoritine);
            }
            _currentMoveCoritine = StartCoroutine(MoveCorunine((int) _motor.motorSpeed, targetSpeed, _accelerationSpeed));
            _targetTorgue = -(_torgue);
        }

        public void StopMoveing() {
            _isMoveing = false;
            if (_currentMoveCoritine != null) {
                StopCoroutine(_currentMoveCoritine);
            }
            _currentMoveCoritine = StartCoroutine(MoveCorunine((int) _motor.motorSpeed, 0, _accelerationSpeed));
            _targetTorgue = 0;
        }
    }
    
}