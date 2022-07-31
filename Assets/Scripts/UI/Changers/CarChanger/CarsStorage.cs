using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.CarChanger {
    
    [CreateAssetMenu(fileName = "CarsStorage", menuName = "Storages/CarsStorage")]
    public class CarsStorage : ScriptableObject {

        [SerializeField] private List<CarStorageDescriptor> _carDescriptors;

        public IEnumerable<CarStorageDescriptor> CarDescriptors => _carDescriptors;
        public CarStorageDescriptor ElementByIndex(int index) => _carDescriptors[index];
        public CarStorageDescriptor ElementByType(CarType carType) => _carDescriptors.Find(cd => cd.CarType == carType);
        
    }
}