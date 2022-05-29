using Cinemachine;
using Gameplay.Car;
using Gameplay.LevelGeneration;
using HUD;
using Scripts;
using Scripts.Save;
using UI.Changers.CarChanger;
using UI.Changers.LevelChanger;
using UnityEngine;
using UnityEngine.U2D;

namespace Gameplay {
    
    public class GemeConfigurator : MonoBehaviour {

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
    
        private void Start() {
            MenuSaveData data = (new MenuPlayerPrefsSystem()).LoadData();

            MapStorageDescriptor descriptor = _mapsStorage.ElementByIndex(data.ChosenMapIndex);
        
            _spriteShapeController.spriteShape = descriptor.SpriteShape;
            _mainCamera.backgroundColor = descriptor.SkyColor;

            CarEntity carPrefab = _carsStorage.ElementByIndex(data.ChosenCarIndex).CarEntity;
            CarEntity carEntity = Instantiate(carPrefab, _startPoint.position, Quaternion.identity);
        
            _graundGenerator.SetObjectOfObservation(carEntity.CarBodyTransform.gameObject);
            _camera.Follow = carEntity.CarBodyTransform;
            _carInputController.SetCarMover(carEntity.CarMover);
            carEntity.CarSound.SetAudioSource(_audioSource);
        
            carHudProvider.SetCarEntity(carEntity);
        }
    
    }
    
}
