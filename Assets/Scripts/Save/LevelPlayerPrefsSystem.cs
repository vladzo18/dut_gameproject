using UnityEngine;

namespace Scripts.Save {
    
    public class LevelPlayerPrefsSystem : ISaveSystem<LevelSaveData> {

        private const string TAKED_COINS_KEY = "levelTakedCoins";
        private const string TAKED_DIAMANTS_KEY = "levelTakedDiamants";
        
        public void SaveData(LevelSaveData data) {
            PlayerPrefs.SetInt(TAKED_COINS_KEY, data.CoinsAmount);
            PlayerPrefs.SetInt(TAKED_DIAMANTS_KEY, data.DiamontsAmount);
        }

        public LevelSaveData LoadData() {
            if (!PlayerPrefs.HasKey(TAKED_COINS_KEY) || !PlayerPrefs.HasKey(TAKED_DIAMANTS_KEY)) {
                return null;
            }
            
            LevelSaveData saveData = new LevelSaveData();
           
            saveData.CoinsAmount = PlayerPrefs.GetInt(TAKED_COINS_KEY);
            saveData.DiamontsAmount = PlayerPrefs.GetInt(TAKED_DIAMANTS_KEY);
            PlayerPrefs.DeleteKey(TAKED_COINS_KEY);
            PlayerPrefs.DeleteKey(TAKED_DIAMANTS_KEY);

            return saveData;
        }
        
    }
    
}