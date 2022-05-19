using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.LevelChanger {
    
    [CreateAssetMenu(fileName = "MapsStorage", menuName = "Storages/MapsStorage")]
    public class MapsStorage : ScriptableObject {

        [SerializeField] private List<MapStorageDescriptor> _mapDescriptors;

        public IEnumerable<MapStorageDescriptor> CarDescriptors => _mapDescriptors;
        public MapStorageDescriptor ElementByIndex(int index) => _mapDescriptors[index];
        public int CarCount => _mapDescriptors.Count;

    }

    [Serializable]
    public class MapStorageDescriptor {
      
        [SerializeField] private string _mapName;
        [SerializeField] private Sprite _mapImage;
        [SerializeField] private int _mapCost;
        [SerializeField, TextArea] private string _description;

        public string MapName => _mapName;
        public Sprite MapImage => _mapImage;
        public int MapCost => _mapCost;
        public string Description => _description;
        
    }
    
}