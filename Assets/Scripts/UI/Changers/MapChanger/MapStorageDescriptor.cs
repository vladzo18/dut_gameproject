using System;
using UnityEngine;
using UnityEngine.U2D;

namespace UI.Changers.MapChanger {
    
    [Serializable]
    public class MapStorageDescriptor {
        
        [SerializeField] private MapType _type;
        [SerializeField] private string _mapName;
        [SerializeField] private Sprite _mapImage;
        [SerializeField] private int _mapCost;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private SpriteShape _spriteShape;
        [SerializeField] private Color _skyColor;
        
        public MapType Type => _type;
        public string MapName => _mapName;
        public Sprite MapImage => _mapImage;
        public int MapCost => _mapCost;
        public string Description => _description;
        public SpriteShape SpriteShape => _spriteShape;
        public Color SkyColor => _skyColor;
        
    }
    
}