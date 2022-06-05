using System;
using Gameplay.Car;
using UnityEngine;

namespace UI.Changers.CarChanger {
    
    [Serializable]
    public class CarStorageDescriptor {

        [SerializeField] private CarType _carType;
        [SerializeField] private string _carName;
        [SerializeField] private Sprite _carImage;
        [SerializeField] private int _carCost;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private CarEntity _carPrefab;

        public CarType CarType => _carType;
        public string CarName => _carName;
        public Sprite CarImage => _carImage;
        public int CarCost => _carCost;
        public string Description => _description;
        public CarEntity CarEntity => _carPrefab;
        
    }
    
}