using System;
using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Car {
    
    [RequireComponent(typeof(Collider2D))]
    public class CarCollector : MonoBehaviour {

        public event Action<float> OnFuelCollect;
        public event Action<float> OnCoinCollect;
        public event Action<float> OnDiamontCollect;
        public event Action<ItemType> OnItemColect;
        
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
                   case ItemType.Diamant:
                       OnDiamontCollect?.Invoke(item.ItemAmount);
                       break;
                }
                
                OnItemColect?.Invoke(item.ItemType);
                item.Take();
            }
        }

    }

}
