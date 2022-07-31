using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.MapChanger {
    
    [CreateAssetMenu(fileName = "MapsStorage", menuName = "Storages/MapsStorage")]
    public class MapsStorage : ScriptableObject {

        [SerializeField] private List<MapStorageDescriptor> _mapDescriptors;

        public IEnumerable<MapStorageDescriptor> MapDescriptors => _mapDescriptors;
        public MapStorageDescriptor ElementByIndex(int index) => _mapDescriptors[index];
        public MapStorageDescriptor ElementByType(MapType type) => _mapDescriptors.Find(d => d.Type == type);

    }
    
}