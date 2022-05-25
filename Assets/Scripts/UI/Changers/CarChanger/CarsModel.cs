using System;
using System.Collections.Generic;
using Scripts.Save;
using UI.Changers.CarChanger;

namespace UI.Changers.LevelChanger {
    
    public class CarsModel {
        
        private List<CarModelItem> _carStorageDescriptors;
        private CarsStorage _storage;
        
        private BinarySaveSystem _binarySaveSystem;

        public event Action OnModelChanged;
        
        public int ItemsCount => _carStorageDescriptors.Count;
        
        public CarsModel(CarsStorage storage) {
            _carStorageDescriptors = new List<CarModelItem>();
            _binarySaveSystem = new BinarySaveSystem(nameof(CarsModel));
            _storage = storage;

            foreach (var mapDescriptor in _storage.CarDescriptors) {
                CarModelItem item = new CarModelItem();
                item.CarStorageDescriptor = mapDescriptor.CarType;
                if (mapDescriptor.CarCost == 0) {
                    item.isAvaliable = true;
                }
                _carStorageDescriptors.Add(item);
            }
        }

        public CarStorageDescriptor GetDescriptorAt(int index) {
            return _storage.ElementByIndex(index);
        }

        public bool GetAvaliabilityStatusAt(int index) {
            return _carStorageDescriptors[index].isAvaliable;
        }

        public void SetAvaliabilityStatusAt(int index, bool status) {
            _carStorageDescriptors[index].isAvaliable = status;
            OnModelChanged?.Invoke();
        }

        public void SaveModel() {
            _binarySaveSystem.Save(_carStorageDescriptors);
        }

        public void LoadModel() {
            List<CarModelItem> list = _binarySaveSystem.Load<List<CarModelItem>>();
            if (list == null) return;
            for (int i = 0; i < list.Count; i++) {
                SetAvaliabilityStatusAt(i, list[i].isAvaliable);
            }
        }
        
        [Serializable]
        private class CarModelItem {
            public CarType CarStorageDescriptor;
            public bool isAvaliable;
        }
    }
    
}