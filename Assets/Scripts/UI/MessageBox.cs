using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    
    public class MessageBox : MonoBehaviour, IPointerDownHandler {

        [SerializeField] private Canvas _messageBoxCanvas;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;
        [SerializeField] private Image _image;
        [SerializeField] private Button _submitButtonn;
        [SerializeField] private TMP_Text _priceText;

        public event Action OnShow;
        public event Action OnClose;
        public event Action<int> OnTryBuyClick;

        private void Start() {
            _submitButtonn.onClick.AddListener(OnClick);
        }
        
        private void OnDestroy() {
            _submitButtonn.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => OnTryBuyClick?.Invoke(Int32.Parse(_priceText.text));

        public void SetTitle(string text) => _title.text = text;
        
        public void SetContent(string text) => _content.text = text;
        
        public void SetPrice(string text) => _priceText.text = text;

        public void SetImage(Sprite sprite) {
            _image.sprite = sprite;
            _image.color = Color.white;
        }

        public void ShowMessageBox() {
            _messageBoxCanvas.enabled = true;
        }

        public void HideMessageBox() {
            _messageBoxCanvas.enabled = false;
        }

        public void Clear() {
            _title.text = "";
            _content.text = "";
            _priceText.text = "";
            _image.color = new Color(0, 0, 0, 0);
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            HideMessageBox();
        }
        
    }
    
}
