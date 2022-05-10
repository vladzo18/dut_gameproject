using UnityEngine;

namespace Car {
    
    public class CarInputController : MonoBehaviour {

        [SerializeField] private CarMover _carMover;
        [SerializeField] private PedalButton _gasButton;
        [SerializeField] private PedalButton _brakeButton;
        
        private void Start() {
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
            _carMover.MoveRight();
        }
    
        private void OnBrake() {
            _carMover.MoveLeft();
        }

        private void OnStop() {
            _carMover.StopMoveing();
        }

    }

}

