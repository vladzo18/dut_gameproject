using Items;
using UnityEngine;

namespace Gameplay.Items {
    
    public class Item : MonoBehaviour {

        [SerializeField] private ItemType _itemType;
        [SerializeField] private float _itemAmount;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;

        private const string TAKE_ANIMATION_KEY = "Take";

        public ItemType ItemType => _itemType;
        public float ItemAmount => _itemAmount;

        public void Take() {
            _collider.enabled = false;
            _animator.Play(TAKE_ANIMATION_KEY);
            Invoke(nameof(Determinate), 0.5f);
        }

        private void Determinate() {
            this.gameObject.SetActive(false);
        }
        
    }

}
