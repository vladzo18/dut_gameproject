using System.Collections.Generic;
using UI.Changers.CarChanger;
using UnityEngine;

namespace Gameplay {
    
    [CreateAssetMenu(fileName = "CarPropertiesStorage", menuName = "Storages/CarPropertiesStorage")]
    public class CarPropertiesStorage : ScriptableObject {

        [SerializeField] private List<CarPropertiesStorageDescriptor> _descriptors;

        public CarPropertiesStorageDescriptor GetDescriptorByType(CarType type) => _descriptors.Find(d => d.Type == type);
        
    }
}