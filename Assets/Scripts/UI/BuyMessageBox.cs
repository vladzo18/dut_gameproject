using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class BuyMessageBox : MonoBehaviour {
        
        [SerializeField] private Canvas _messageBoxCanvas;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _submitButtonn;
        [SerializeField] private Button _overlayButton;
        [SerializeField] private Animator _animator;

        public event Action OnClose;
        public event Action<int> OnBuyButtonClick;

        private void Start() {
            _submitButtonn.onClick.AddListener(OnClick);
            _overlayButton.onClick.AddListener(HideMessageBox);
        }

        private void OnDestroy() {
            _submitButtonn.onClick.RemoveListener(OnClick);
            _overlayButton.onClick.RemoveListener(HideMessageBox);
        }

        private void OnClick() => OnBuyButtonClick?.Invoke(int.Parse(_priceText.text));

        public void SetTitle(string text) => _title.text = text;

        public void SetContent(string text) => _content.text = text;

        public void SetPrice(string text) => _priceText.text = text;

        public void SetImage(Sprite sprite) {
            _image.sprite = sprite;
            _image.color = Color.white;
        }

        public void ShowMessageBox() {
            _messageBoxCanvas.enabled = true;
            _animator.Play("ShowDown");
        }

        public void HideMessageBox() {
            OnClose?.Invoke();
            StartCoroutine(HideMessageBoxRoutine());
        }

        public void Clear() {
            _title.text = _content.text = _priceText.text = string.Empty;
            _image.color = new Color(0, 0, 0, 0);
        }

        private IEnumerator HideMessageBoxRoutine() {
            _animator.Play("HideUp");
            yield return new WaitForSeconds(0.15f);
            _messageBoxCanvas.enabled = false;
        }
        
    }
}