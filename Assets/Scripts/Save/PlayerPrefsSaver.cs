using UI.Changers.CarChanger;
using UI.Changers.MapChanger;

namespace Save {
    
    public static class PlayerPrefsSaver {
        
        private static readonly string MenuSaveDataKey = $"{nameof(MenuSaveData)}Key";
        private static readonly string LevelSaveDataKey = $"{nameof(LevelSaveData)}Key";
        
        public static ObjectPref<MenuSaveData> MenuSaveData { get; private set; }
        public static ObjectPref<LevelSaveData> LevelSaveData { get; private set; }
        
        static PlayerPrefsSaver() {
            MenuSaveData = new ObjectPref<MenuSaveData>(MenuSaveDataKey, new MenuSaveData {
                ChosenCarType = CarType.RacingCar,
                ChosenMapType = MapType.Countryside
            });
            LevelSaveData = new ObjectPref<LevelSaveData>(LevelSaveDataKey, new LevelSaveData());
        }
        
    }
    
}