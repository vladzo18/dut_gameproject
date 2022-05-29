using Gameplay.Car;
using UnityEngine;

namespace Items {
    
    public class CarInputController : MonoBehaviour {

        [SerializeField] private MonoBehaviour _carMover;
        [SerializeField] private PedalButton _gasButton;
        [SerializeField] private PedalButton _brakeButton;

        private IMover _mover;
        
        public void SetCarMover(IMover carMover) {
            _mover = carMover;
            _carMover = carMover as MonoBehaviour;
        }

        private void Start() {
            if (_carMover) {
                _mover = _carMover as IMover;
            }
            _gasButton.OnPutPedal += OnGas;
            _brakeButton.OnPutPedal += OnBrake;
            _gasButton.OnUnputPedal += OnStop;
            _brakeButton.OnUnputPedal += OnStop;
        }
    
        private void OnDestroy() {
            _gasButton.OnPutPedal -= OnGas;
            _brakeButton.OnPutPedal -= OnBrake;
            _gasButton.OnUnputPedal -= OnStop;
            _brakeButton.OnUnputPedal -= OnStop;
        }

        private void OnGas() {
            _mover.MoveRight();
        }
    
        private void OnBrake() {
            _mover.MoveLeft();
        }

        private void OnStop() {
            _mover.StopMoveing();
        }
        
        private void OnValidate() {
            if (!(_carMover is IMover)) {
                _carMover = null;
            }
        }

    }

}

