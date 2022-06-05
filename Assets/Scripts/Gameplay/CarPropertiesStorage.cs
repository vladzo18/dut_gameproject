using System;
using System.Collections.Generic;
using UI.Changers.CarChanger;
using UnityEngine;

namespace Gameplay {
    
    [CreateAssetMenu(fileName = "CarPropertiesStorage", menuName = "Storages/CarPropertiesStorage")]
    public class CarPropertiesStorage : ScriptableObject {

        [SerializeField] private List<CarPropertiesStorageDescriptor> _descriptors;

        public CarPropertiesStorageDescriptor GetDescriptorByType(CarType type) {
            return _descriptors.Find(d => d.Type == type);
        }

    }

    [Serializable]
    public class CarPropertiesStorageDescriptor {

        [SerializeField] private CarType _type;
        [Header("Edgine")] 
        [SerializeField] private float _minEdgineForce;
        [SerializeField] private float _maxEdgineForce;
        [Header("FuelTank")] 
        [SerializeField] private float _minFuelCapasity;
        [SerializeField] private float _maxFuelCapasity;

        public CarType Type => _type;
        public float MINEdgineForce => _minEdgineForce;
        public float MAXEdgineForce => _maxEdgineForce;
        public float MINFuelCapasity => _minFuelCapasity;
        public float MAXFuelCapasity => _maxFuelCapasity;
        
    }
    
}