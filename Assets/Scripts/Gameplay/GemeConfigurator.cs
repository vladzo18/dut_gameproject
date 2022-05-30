using Cinemachine;
using Gameplay.Car;
using Gameplay.LevelGeneration;
using HUD;
using Items;
using Items.Save;
using UI.Changers.CarChanger;
using UI.Changers.LevelChanger;
using UnityEngine;
using UnityEngine.U2D;

namespace Gameplay {
    
    public class GemeConfigurator : MonoBehaviour, IResetable {

        [SerializeField] private SpriteShapeController _spriteShapeController;
        [SerializeField] private MapsStorage _mapsStorage;
        [SerializeField] private CarsStorage _carsStorage;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GraundGenerator _graundGenerator;
        [SerializeField] private CarInputController _carInputController;
        [SerializeField] private CarHUDProvider carHudProvider;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Camera _mainCamera;

        private CarEntity _carEntity;
        
        private void Start() {
            GameReset.Register(this);
            
            MenuSaveData data = (new MenuPlayerPrefsSystem()).LoadData();

            MapStorageDescriptor descriptor = _mapsStorage.ElementByIndex(data.ChosenMapIndex);
        
            _spriteShapeController.spriteShape = descriptor.SpriteShape;
            _mainCamera.backgroundColor = descriptor.SkyColor;

            CarEntity carPrefab = _carsStorage.ElementByIndex(data.ChosenCarIndex).CarEntity;
            _carEntity = Instantiate(carPrefab, _startPoint.position, Quaternion.identity);
        
            _graundGenerator.SetObjectOfObservation(_carEntity.CarBodyTransform.gameObject);
            _camera.Follow = _carEntity.CarBodyTransform;
            _carInputController.SetCarMover(_carEntity.CarMover);
            _carEntity.CarSound.SetAudioSource(_audioSource);
        
            carHudProvider.SetCarEntity(_carEntity);
        }

        private void OnDestroy() {
            GameReset.Unregister(this);
        }

        public void Reset() {
            _carEntity.CarBodyTransform.position = _startPoint.position;
        }
        
    }
    
}
