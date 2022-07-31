using System;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Car {
    
    [RequireComponent(typeof(Collider2D))]
    public class CarCollector : MonoBehaviour {

        public event Action<float> OnFuelCollect;
        public event Action<float> OnCoinCollect;
        public event Action<float> OnDiamondCollect;
        public event Action<ItemType> OnItemCollect;
        
        private void OnTriggerEnter2D(Collider2D collider) {
            var item = collider.GetComponent<Item>();

            if (item != null) {
                switch (item.ItemType) {
                   case ItemType.Fuel:
                       OnFuelCollect?.Invoke(item.ItemAmount);
                       break;
                   case ItemType.Coin:
                       OnCoinCollect?.Invoke(item.ItemAmount);
                       break;
                   case ItemType.Diamond:
                       OnDiamondCollect?.Invoke(item.ItemAmount);
                       break;
                   case ItemType.None:
                       break;
                   default:
                       throw new ArgumentOutOfRangeException();
                }
                
                OnItemCollect?.Invoke(item.ItemType);
                item.Take();
            }
        }

    }

}
