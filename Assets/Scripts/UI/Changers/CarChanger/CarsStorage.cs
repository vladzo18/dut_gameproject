using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.CarChanger {
    
    [CreateAssetMenu(fileName = "CarsStorage", menuName = "Storages/CarsStorage")]
    public class CarsStorage : ScriptableObject {

        [SerializeField] private List<CarStorageDescriptor> _carDescriptors;

        public IEnumerable<CarStorageDescriptor> CarDescriptors => _carDescriptors;
        public CarStorageDescriptor ElementByIndex(int index) => _carDescriptors[index];
        public int CarCount => _carDescriptors.Count;
        
    }

    [Serializable]
    public class CarStorageDescriptor {
      
        [SerializeField] private string _carName;
        [SerializeField] private Sprite _carImage;
        [SerializeField] private int _carCost;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private GameObject _carPrefab;
        
        public string CarName => _carName;
        public Sprite CarImage => _carImage;
        public int CarCost => _carCost;
        public string Description => _description;
        public GameObject CarPrefab => _carPrefab;
        
    }
    
}