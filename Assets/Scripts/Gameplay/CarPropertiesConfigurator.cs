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
            float fuelCapacity = Mathf.Lerp(descriptor.MINFuelCapacity, descriptor.MAXFuelCapacity, fuelValue / 10.0f);
            _carEntity.CarTank.SetFuelMaxAmount(fuelCapacity);
            
            int engineValue = propertySettings.GetSettingByType(CarProrertyType.EngineForce).Value;
            float engineForce = Mathf.Lerp(descriptor.MINEngineForce, descriptor.MAXEngineForce, engineValue / 10.0f);
            _carEntity.CarMover.SetEngineForce(engineForce);
        }

    }
    
}