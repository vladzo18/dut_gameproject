using UI.Changers.CarChanger;
using UnityEngine;

namespace Gameplay.Car {
    
    public class CarEntity : MonoBehaviour {

        [SerializeField] private CarType _type;
        [SerializeField] private Transform _carBodyTransform;
        [SerializeField] private MonoBehaviour _carMover;
        [SerializeField] private CarCollector _carCollector;
        [SerializeField] private CarTank _carTank;
        [SerializeField] private CarDistanceCounter _carDistanceCounter;
        [SerializeField] private CarDeath _carDeath;
        [SerializeField] private CarSound _carSound;

        public CarType Type => _type;
        public Transform CarBodyTransform => _carBodyTransform;
        public IMover CarMover => _carMover as IMover;
        public CarCollector CarCollector => _carCollector;
        public CarTank CarTank => _carTank;
        public CarDistanceCounter CarDistanceCounter => _carDistanceCounter;
        public CarDeath CarDeath => _carDeath;
        public CarSound CarSound => _carSound;

        private void OnValidate() {
            if (!(_carMover is IMover)) {
                _carMover = null;
            }
        }
        
    }
    
}