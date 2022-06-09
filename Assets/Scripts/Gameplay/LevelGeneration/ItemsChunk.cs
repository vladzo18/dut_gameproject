using System.Collections.Generic;
using System.Linq;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.LevelGeneration {
    
    public class ItemsChunk : MonoBehaviour {

        [SerializeField] private float _heightAboveGround = 1;
        
        private List<Item> _items;
        private Collider2D _collider;

        private void OnEnable() {
            _items = this.GetComponentsInChildren<Item>().ToList();
        }

        public void SetObservableCollider(Collider2D collider) {
            _collider = collider;
        }

        private void Update() {
            if (!_collider) return;
            
            foreach (var item in _items) {
                var position = item.transform.position;
                Vector2 closestPoint = _collider.ClosestPoint(position);
                position = new Vector3(position.x,closestPoint.y + _heightAboveGround);
                item.transform.position = position;
            }
        }

        public void Clear() {
            Destroy(this.gameObject);
        }
        
    }
    
}
