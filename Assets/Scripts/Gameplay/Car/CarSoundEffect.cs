using UnityEngine;

namespace Gameplay.Car {
    
    public class CarSoundEffect : MonoBehaviour {

        [SerializeField] private MonoBehaviour _mover;
        [SerializeField, Range(0, 1f)] private float _maxEgineSoundVolume = 0.35f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _egineSound;

        private IMover _carMover;
        
        private void Start() {
            if (_mover) _carMover = _mover as IMover;
            _audioSource.clip = _egineSound;
            _audioSource.volume = 0;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        private void Update() {
            if (_carMover.IsMoving) {
                float engineSpeedPercent = _carMover.CurrentEngineSpeed / _carMover.MaxSpeed;
                _audioSource.volume = Mathf.Lerp(0, _maxEgineSoundVolume, engineSpeedPercent);
            } else {
                _audioSource.volume = 0;
            }
        }

        private void OnValidate() {
            if (!(_mover is IMover)) {
                _mover = null;
            }
        }
        
    }
    
}