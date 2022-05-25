using System;
using System.Collections.Generic;
using Scripts.Save;

namespace UI.Changers.LevelChanger {
    
    public class MapsModel {
        
        private List<MapModelItem> _mapStorageDescriptors;
        private MapsStorage _storage;
        
        private BinarySaveSystem _binarySaveSystem;

        public event Action OnModelChanged;
        
        public int ItemsCount => _mapStorageDescriptors.Count;
        
        public MapsModel(MapsStorage storage) {
            _mapStorageDescriptors = new List<MapModelItem>();
            _binarySaveSystem = new BinarySaveSystem(nameof(MapsModel));
            _storage = storage;

            foreach (var mapDescriptor in _storage.MapDescriptors) {
                MapModelItem item = new MapModelItem();
                item.MapStorageDescriptor = mapDescriptor.Type;
                if (mapDescriptor.MapCost == 0) {
                    item.isAvaliable = true;
                }
                _mapStorageDescriptors.Add(item);
            }
        }

        public MapStorageDescriptor GetDescriptorAt(int index) {
            return _storage.ElementByIndex(index);
        }

        public bool GetAvaliabilityStatusAt(int index) {
            return _mapStorageDescriptors[index].isAvaliable;
        }

        public void SetAvaliabilityStatusAt(int index, bool status) {
            _mapStorageDescriptors[index].isAvaliable = status;
            OnModelChanged?.Invoke();
        }

        public void SaveModel() {
            _binarySaveSystem.Save(_mapStorageDescriptors);
        }

        public void LoadModel() {
            List<MapModelItem> list = _binarySaveSystem.Load<List<MapModelItem>>();
            if (list == null) return;
            for (int i = 0; i < list.Count; i++) {
                SetAvaliabilityStatusAt(i, list[i].isAvaliable);
            }
        }
        
        [Serializable]
        private class MapModelItem {
            public MapType MapStorageDescriptor;
            public bool isAvaliable;
        }
    }
    
}