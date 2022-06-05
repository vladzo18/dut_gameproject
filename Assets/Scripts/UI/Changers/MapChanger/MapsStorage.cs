using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.LevelChanger {
    
    [CreateAssetMenu(fileName = "MapsStorage", menuName = "Storages/MapsStorage")]
    public class MapsStorage : ScriptableObject {

        [SerializeField] private List<MapStorageDescriptor> _mapDescriptors;

        public IEnumerable<MapStorageDescriptor> MapDescriptors => _mapDescriptors;
        public MapStorageDescriptor ElementByIndex(int index) => _mapDescriptors[index];
        public int MapCount => _mapDescriptors.Count;

    }
    
}