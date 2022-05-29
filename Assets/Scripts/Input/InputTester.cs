using UnityEngine;

namespace Scripts {
    
    public class InputTester : MonoBehaviour  {
        
        [SerializeField] private PedalButton _gasButton;
        [SerializeField] private PedalButton _brakeButton;
        
        #region ForTestMovement
        private void Update() {
            if (Input.GetKeyDown(KeyCode.D)) {
                _gasButton.OnPointerDown(null);
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                _brakeButton.OnPointerDown(null);
            }

            if (Input.GetKeyUp(KeyCode.A) ) {
                _brakeButton.OnPointerUp(null);
            }
            if (Input.GetKeyUp(KeyCode.D)) {
                _gasButton.OnPointerUp(null);
            }
        }
        #endregion
        
    }
    
}