using UnityEngine;

namespace Items.Save {
    
    public class MenuPlayerPrefsSystem : ISaveSystem<MenuSaveData> {
        
        private const string COINS_KEY = "Coins";
        private const string DIAMANTS_KEY = "Diamants";
        private const string CAR_INDEX_KEY = "Car";
        private const string MAP_INDEX_KEY = "Map";
        
        public void SaveData(MenuSaveData data) {
            PlayerPrefs.SetInt(COINS_KEY, data.CoinsAmount);
            PlayerPrefs.SetInt(DIAMANTS_KEY, data.DiamantsAmount);
            PlayerPrefs.SetInt(CAR_INDEX_KEY, data.ChosenCarIndex);
            PlayerPrefs.SetInt(MAP_INDEX_KEY, data.ChosenMapIndex);
            PlayerPrefs.Save();
        }

        public MenuSaveData LoadData() {
            MenuSaveData saveData = new MenuSaveData();
           
            saveData.CoinsAmount = PlayerPrefs.GetInt(COINS_KEY);
            saveData.DiamantsAmount = PlayerPrefs.GetInt(DIAMANTS_KEY);
            saveData.ChosenCarIndex = PlayerPrefs.GetInt(CAR_INDEX_KEY);
            saveData.ChosenMapIndex = PlayerPrefs.GetInt(MAP_INDEX_KEY );

            return saveData;
        }
        
    }
    
}