using Gameplay.Car;
using Scripts;
using Scripts.Save;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HUD {
    
    public class GameEndPresenter : MonoBehaviour, IPointerDownHandler {

        [SerializeField] private CoinsPresenter _coinsPresenter;
        [SerializeField] private Canvas _gameEndCanvas;
        [SerializeField] private CarDeath _carDeath;

        private void Start() {
            _carDeath.OnCatDeath += DeathHandler;
        }

        private void OnDestroy() {
            _carDeath.OnCatDeath -= DeathHandler;
        }

        private void DeathHandler() {
            _gameEndCanvas.enabled = true;
        }

        private void SaveLevelProgres() {
            LevelSaveData saveData = new LevelSaveData();
            saveData.CoinsAmount = _coinsPresenter.CoinsAmount;
            saveData.DiamontsAmount = 0;
            (new LevelPlayerPrefsSystem()).SaveData(saveData);
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            SaveLevelProgres();
            SceneSwitcher.LoadScene(SceneSwitcher.MENU_SCENE_KEY);
        }
        
    }
    
}