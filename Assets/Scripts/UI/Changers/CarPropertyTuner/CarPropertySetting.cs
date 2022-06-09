using System;

namespace UI.Changers.CarPropertyTuner {
    
    [Serializable]
    public class CarPropertySetting {

        public CarProrertyType CarPropertyType { get; set; }
        public int Value { get; set; }
        public int ValueBorder { get; set; }

    }
    
}