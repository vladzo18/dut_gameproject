using System.Collections.Generic;
using Items.Save;
using UI.Changers.CarChanger;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertySettings {

        private List<CarPropertySetting> _propertySettings;
        private CarType _carType;
        
        public CarPropertySettings(CarType type) {
            _propertySettings = new List<CarPropertySetting>();
            _carType = type;
        }

        public CarPropertySetting AddSetting(CarProrertyType prorertyType, int value = 0) {
            CarPropertySetting setting = new CarPropertySetting();
            setting.CarProrertyType = prorertyType;
            setting.Value = value;
            _propertySettings.Add(setting);
            return setting;
        }

        public CarPropertySetting GetSettingByType(CarProrertyType prorertyType) {
            return _propertySettings.Find(s => s.CarProrertyType == prorertyType);
        }

        public void SaveState() {
            BinarySaveSystem saveSystem = new BinarySaveSystem(nameof(CarPropertySettings) + _carType);
            saveSystem.Save(_propertySettings);
        }

        public bool TryLoadState() {
            BinarySaveSystem saveSystem = new BinarySaveSystem(nameof(CarPropertySettings) + _carType);
            List<CarPropertySetting> loadedList = saveSystem.Load<List<CarPropertySetting>>();
            
            if (loadedList != null) {
                _propertySettings = loadedList;
                return true;
            } else {
                return false;
            }
        }
        
    }
    
}