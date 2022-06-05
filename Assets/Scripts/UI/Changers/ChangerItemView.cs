using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Changers {
    
    public class ChangerItemView : MonoBehaviour{

        [SerializeField] private TMP_Text _head;
        [SerializeField] private Image _body;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private GameObject _lockedBox;
        
        public void SetHeadText(string text) => _head.text = text;
        public void SetItemPrice(string text) => _price.text = text;
        public void SetBodyImage(Sprite sprite) => _body.sprite = sprite;
        public void SetLockedBoxActivity(bool isActive) => _lockedBox.SetActive(isActive);

    }
    
}

