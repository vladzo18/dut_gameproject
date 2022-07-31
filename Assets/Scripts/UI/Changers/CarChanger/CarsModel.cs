using System;
using System.Collections.Generic;
using Save;

namespace UI.Changers.CarChanger {
    
    public class CarsModel {
        
        private readonly CarsStorage _storage;
        private readonly ObjectPref<State> _objectPref;
        
        private State _modelState;

        public event Action OnModelChanged;
        
        public int ItemsCount => _modelState.carsAvailability.Count;
        public CarType CurrentCarType => _modelState.currentCarType;
        
        public CarsModel(CarsStorage storage) {
            _modelState = new State();
            _storage = storage;

            foreach (var mapDescriptor in _storage.CarDescriptors) {
                _modelState.carsAvailability.Add(mapDescriptor.CarCost == 0);
            }
            _objectPref = new ObjectPref<State>(nameof(CarsModel), _modelState);
        }

        public CarStorageDescriptor GetDescriptorAt(int index) => _storage.ElementByIndex(index);
        
        public bool GetAvailabilityStatusAt(int index) => _modelState.carsAvailability[index];
        
        public void SetAvailabilityStatusAt(int index, bool status) {
            _modelState.carsAvailability[index] = status;
            OnModelChanged?.Invoke();
        }

        public void SetCurrentCarType(CarType carType) => _modelState.currentCarType = carType;

        public void SaveModel() => _objectPref.Set(_modelState);
        
        public void LoadModel() {
            var loadedState = _objectPref.Get();
            if (loadedState == null) return;
            _modelState = loadedState;
            OnModelChanged?.Invoke();
        }

        [Serializable]
        private class State {
            public List<bool> carsAvailability = new List<bool>();
            public CarType currentCarType = CarType.RedCar;
        }
        
    }
    
}