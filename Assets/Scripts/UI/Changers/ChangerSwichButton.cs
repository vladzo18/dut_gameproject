using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Changers {
    
    public class ChangerSwichButton : MonoBehaviour {

        [SerializeField] private Button _button;
        [SerializeField] private ChangerSwichButtonType _type;
        
        public event Action<ChangerSwichButtonType> OnButtonClick;

        private void Start() {
           _button.onClick.AddListener(OnClickHandler);
        }

        private void OnDestroy() {
            _button.onClick.RemoveListener(OnClickHandler);
        }

        private void OnClickHandler() => OnButtonClick?.Invoke(_type);

    }
}
