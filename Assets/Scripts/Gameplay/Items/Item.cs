using Items;
using UnityEngine;

namespace Item {
    
    public class Item : MonoBehaviour {

        [SerializeField] private ItemType _itemType;
        [SerializeField] private float _itemAmount;
        [SerializeField] private Animator _animator;

        private const string TAKE_ANIMATION_KEY = "Take";

        public ItemType ItemType => _itemType;
        public float ItemAmount => _itemAmount;

        public void Take() {
            _animator.Play(TAKE_ANIMATION_KEY);
            Invoke(nameof(Determinate), 0.5f);
        }

        private void Determinate() {
            this.gameObject.SetActive(false);
        }
        
    }

}
