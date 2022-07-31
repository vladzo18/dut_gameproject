using System;
using System.Collections.Generic;
using Save;

namespace UI.Changers.MapChanger {
    
    public class MapsModel {
        
        private readonly MapsStorage _storage;
        private readonly ObjectPref<State> _objectPref;
        
        private State _modelState;

        public event Action OnModelChanged;
        
        public int ItemsCount => _modelState.carsAvailability.Count;
        public MapType CurrentMapType => _modelState.currentMapType;
        
        public MapsModel(MapsStorage storage) {
            _modelState = new State();
            _storage = storage;

            foreach (var mapDescriptor in _storage.MapDescriptors) {
                _modelState.carsAvailability.Add(mapDescriptor.MapCost == 0);
            }
            _objectPref = new ObjectPref<State>(nameof(MapsModel), _modelState);
        }

        public MapStorageDescriptor GetDescriptorAt(int index) {
            return _storage.ElementByIndex(index);
        }

        public bool GetAvailabilityStatusAt(int index) {
            return _modelState.carsAvailability[index];
        }

        public void SetAvailabilityStatusAt(int index, bool status) {
            _modelState.carsAvailability[index] = status;
            OnModelChanged?.Invoke();
        }

        public void SetCurrentMapType(MapType mapType) => _modelState.currentMapType = mapType;

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
            public MapType currentMapType = MapType.Countryside;
        }
        
    }
    
}