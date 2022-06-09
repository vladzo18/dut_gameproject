using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Changers {
    
    public class ChangerSwitchButton : MonoBehaviour {

        [SerializeField] private Button _button;
        [SerializeField] private ChangerSwitchButtonType _type;
        
        public event Action<ChangerSwitchButtonType> OnButtonClick;

        private void Start() {
           _button.onClick.AddListener(OnClickHandler);
        }

        private void OnDestroy() {
            _button.onClick.RemoveListener(OnClickHandler);
        }

        private void OnClickHandler() => OnButtonClick?.Invoke(_type);

    }
}
