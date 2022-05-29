using Items;
using Items.Save;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HUD {
    
    public class GameEndPerformance : MonoBehaviour, IPointerDownHandler {

        [SerializeField] private CurrencyBox currencyBox;
        [SerializeField] private Canvas _gameEndCanvas;
        
        public void ShowGameEndWindow() =>  _gameEndCanvas.enabled = true;
     
        private void SaveLevelProgres() {
            LevelSaveData saveData = new LevelSaveData();
            saveData.CoinsAmount = currencyBox.CoinsAmount;
            saveData.DiamontsAmount = currencyBox.DiamontsAmount;
            saveData.DrivenMeters = 0;
            (new LevelPlayerPrefsSystem()).SaveData(saveData);
        }
             
        public void OnPointerDown(PointerEventData eventData) {
            SaveLevelProgres();
            SceneSwitcher.LoadScene(SceneSwitcher.MENU_SCENE_KEY);
        }
             
    }
         
}  