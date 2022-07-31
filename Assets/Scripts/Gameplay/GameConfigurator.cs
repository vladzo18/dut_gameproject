using Cinemachine;
using GameInput;
using Gameplay.Car;
using Gameplay.LevelGeneration;
using General;
using HUD;
using Save;
using UI.Changers.CarChanger;
using UI.Changers.MapChanger;
using UnityEngine;
using UnityEngine.U2D;

namespace Gameplay {
    
    public class GameConfigurator : MonoBehaviour, IResettable {

        [SerializeField] private SpriteShapeController _spriteShapeController;
        [SerializeField] private MapsStorage _mapsStorage;
        [SerializeField] private CarsStorage _carsStorage;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GroundGenerator _groundGenerator;
        [SerializeField] private CarInputController _carInputController;
        [SerializeField] private CarHudProvider _carHudProvider;
        [SerializeField] private AudioSource _bgMusicAudioSource;
        [SerializeField] private AudioClip _banderaCarBGMusic;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CarPropertiesStorage _propertiesStorage;

        private CarEntity _carEntity;
        
        private void Start() {
            GameReset.Register(this);

            MenuSaveData data = PlayerPrefsSaver.MenuSaveData.Get();

            MapStorageDescriptor descriptor = _mapsStorage.ElementByType(data.ChosenMapType);
        
            _spriteShapeController.spriteShape = descriptor.SpriteShape;
            _mainCamera.backgroundColor = descriptor.SkyColor;

            CarEntity carPrefab = _carsStorage.ElementByType(data.ChosenCarType).CarEntity;
            _carEntity = Instantiate(carPrefab, _startPoint.position, Quaternion.identity);
        
            _groundGenerator.SetObjectOfObservation(_carEntity.CarBodyTransform.gameObject);
            _camera.Follow = _carEntity.CarBodyTransform;
            _carInputController.SetCarMover(_carEntity.CarMover);

            CarPropertiesConfigurator carPropConfigurator = new CarPropertiesConfigurator(_carEntity, _propertiesStorage);
            carPropConfigurator.Configure();
        
            _carHudProvider.SetCarEntity(_carEntity);

            if (_carEntity.Type == CarType.BanderaCar) {
                _bgMusicAudioSource.clip = _banderaCarBGMusic;
                _bgMusicAudioSource.Play();
            }
        }

        private void OnDestroy() {
            GameReset.Unregister(this);
        }

        public void Reset() {
            _carEntity.CarBodyTransform.position = _startPoint.position;
        }
        
    }
    
}
