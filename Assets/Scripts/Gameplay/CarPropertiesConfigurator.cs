using Gameplay.Car;
using UI.Changers.CarPropertyTuner;
using UnityEngine;

namespace Gameplay {
    
    public class CarPropertiesConfigurator {

        private readonly CarEntity _carEntity;
        private readonly CarPropertiesStorage _propertiesStorage;
        
        public CarPropertiesConfigurator(CarEntity carEntity, CarPropertiesStorage storage) {
            _carEntity = carEntity;
            _propertiesStorage = storage;
        }

        public void Configure() {
            CarPropertySettings propertySettings = new CarPropertySettings(_carEntity.Type);
            if (!propertySettings.TryLoadState()) return;

            CarPropertiesStorageDescriptor descriptor = _propertiesStorage.GetDescriptorByType(_carEntity.Type);
            if (descriptor == null) return;

            int fuelValue = propertySettings.GetSettingByType(CarProrertyType.FuelCapacity).Value;
            float fuelCapasity = Mathf.Lerp(descriptor.MINFuelCapasity, descriptor.MAXFuelCapasity, fuelValue / 10.0f);
            _carEntity.CarTank.SetFuelMaxAmount(fuelCapasity);
            
            int edgineValue = propertySettings.GetSettingByType(CarProrertyType.EdgineForce).Value;
            float edgineForce = Mathf.Lerp(descriptor.MINEdgineForce, descriptor.MAXEdgineForce, edgineValue / 10.0f);
            _carEntity.CarMover.SetEdgineForce(edgineForce);
        }

    }
    
}