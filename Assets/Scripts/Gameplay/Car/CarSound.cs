using Items;
using UnityEngine;

namespace Gameplay.Car {
    
    public class CarSound : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CarCollector _carCollector;
        [SerializeField] private AudioClip _itemCollectSound;
        [SerializeField] private AudioClip _fuelCollectSound;

        public void SetAudioSource(AudioSource source) => _audioSource = source;
        
        private void Start() {
            _carCollector.OnItemColect += ItemCollectHandler;
        }

        private void OnDestroy() {
            _carCollector.OnItemColect -= ItemCollectHandler;
        }

        private void ItemCollectHandler(ItemType type) {
            switch (type) {
                case ItemType.Diamant:
                case ItemType.Coin:
                    _audioSource.PlayOneShot(_itemCollectSound);
                    break;
                case ItemType.Fuel:
                    _audioSource.PlayOneShot(_fuelCollectSound);
                    break;
            }
          
        }
    }
    
}
