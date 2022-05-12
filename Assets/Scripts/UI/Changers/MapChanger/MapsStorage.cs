using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.LevelChanger {
    
    [CreateAssetMenu(fileName = "MapsStorage", menuName = "Storages/MapsStorage")]
    public class MapsStorage : ScriptableObject {

        [SerializeField] private List<MapStorageDescriptor> _mapDescriptors;

        public IEnumerable<MapStorageDescriptor> CarDescriptors => _mapDescriptors;
        public int CarCount => _mapDescriptors.Count;

    }

    [Serializable]
    public class MapStorageDescriptor {
      
        [SerializeField] private string _carName;
        [SerializeField] private Sprite _carImage;
        [SerializeField] private int _carCost;

        public string CarName => _carName;
        public Sprite CarImage => _carImage;
        public int CarCost => _carCost;

    }
    
}