using System.Collections.Generic;
using Save;
using UI.Changers.CarChanger;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertySettings {

        private List<CarPropertySetting> _propertySettings;
        private readonly CarType _carType;
        private readonly ObjectPref<List<CarPropertySetting>> _objectPref;
        
        public CarPropertySettings(CarType type) {
            _propertySettings = new List<CarPropertySetting>();
            _carType = type;
            _objectPref = new ObjectPref<List<CarPropertySetting>>(nameof(CarPropertySettings) + _carType, _propertySettings);
        }

        public CarPropertySetting AddSetting(CarProrertyType propertyType, int value = 0) {
            CarPropertySetting setting = new CarPropertySetting {
                CarPropertyType = propertyType, 
                Value = value
            };
            _propertySettings.Add(setting);
            return setting;
        }

        public CarPropertySetting GetSettingByType(CarProrertyType propertyType) {
            return _propertySettings.Find(s => s.CarPropertyType == propertyType);
        }

        public void SaveState() => _objectPref.Set(_propertySettings);
        
        public bool TryLoadState() {
            List<CarPropertySetting> loadedList = _objectPref.Get();
            
            if (loadedList.Count > 0) {
                _propertySettings = loadedList;
                return true;
            } else {
                return false;
            }
        }
        
    }
    
}