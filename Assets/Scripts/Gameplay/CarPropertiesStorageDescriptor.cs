using System;
using UI.Changers.CarChanger;
using UnityEngine;

namespace Gameplay {
    
    [Serializable]
    public class CarPropertiesStorageDescriptor {

        [SerializeField] private CarType _type;
        [Header("Engine")] 
        [SerializeField] private float minEngineForce;
        [SerializeField] private float maxEngineForce;
        [Header("FuelTank")] 
        [SerializeField] private float minFuelCapacity;
        [SerializeField] private float maxFuelCapacity;

        public CarType Type => _type;
        public float MINEngineForce => minEngineForce;
        public float MAXEngineForce => maxEngineForce;
        public float MINFuelCapacity => minFuelCapacity;
        public float MAXFuelCapacity => maxFuelCapacity;
        
    }
    
}