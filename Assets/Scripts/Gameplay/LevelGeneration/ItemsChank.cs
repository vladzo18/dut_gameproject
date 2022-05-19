using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.LevelGeneration {
    
    public class ItemsChank : MonoBehaviour {

        [SerializeField] private float _heightAboveGround = 1;
        
        private List<Item.Item> _items;
        private Collider2D _collider;

        private void OnEnable() {
            _items = this.GetComponentsInChildren<Item.Item>().ToList();
        }

        public void SetObservableColider(Collider2D collider) {
            _collider = collider;
        }

        private void Update() {
            if (_collider == null) return;
            foreach (var item in _items) {
                Vector2 closestPoint = _collider.ClosestPoint(item.transform.position);
                item.transform.position = new Vector3(item.transform.position.x,closestPoint.y + _heightAboveGround);
            }
        }

        public void Clear() {
            Destroy(this.gameObject);
        }
        
    }
    
}
