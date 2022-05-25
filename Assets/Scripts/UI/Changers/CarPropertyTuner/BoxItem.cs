using UnityEngine;
using UnityEngine.UI;

namespace UI.Changers.CarPropertyTuner {
    
    public class BoxItem : MonoBehaviour {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _activeItem;
        [SerializeField] private Sprite _nonactiveItem;
        
        public void SetItemActivityStatus(bool status) {
            _image.sprite = status ? _activeItem : _nonactiveItem;
        }
        
    }
    
}