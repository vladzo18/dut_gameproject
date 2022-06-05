using System.Collections;
using General;
using Items.Save;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HUD {
    
    public class GameEndPerformance : MonoBehaviour, IPointerDownHandler {
        
        [SerializeField] private Canvas _gameEndCanvas;
        [SerializeField, Range(0, 5)] private float _performBoxShosingDelay = 0.65f;
        [SerializeField] private PerformBox _coinsPerform;
        [SerializeField] private PerformBox _diamontsPerform;
        [SerializeField] private PerformBox _metersPerform;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _showPerformSound;
        
        private bool canEndGame;
        
        public void ShowGameEndWindow(LevelSaveData saveData) {
            _gameEndCanvas.enabled = true;
            _coinsPerform.SetPerformBox(saveData.CoinsAmount);
            _diamontsPerform.SetPerformBox(saveData.DiamontsAmount);
            _metersPerform.SetPerformBox(saveData.DrivenMeters);
            StartCoroutine(ShowPerformBoxesRoutine());
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            if (canEndGame) {
                SceneSwitcher.LoadScene(SceneSwitcher.MENU_SCENE_KEY);
            }
        }

        private IEnumerator ShowPerformBoxesRoutine() {
            yield return new WaitForSeconds(_performBoxShosingDelay);
            _audioSource.PlayOneShot(_showPerformSound);
            _coinsPerform.Show();
            yield return new WaitForSeconds(_performBoxShosingDelay);
            _audioSource.PlayOneShot(_showPerformSound);
            _diamontsPerform.Show();
            yield return new WaitForSeconds(_performBoxShosingDelay);
            _audioSource.PlayOneShot(_showPerformSound);
            _metersPerform.Show();
            yield return new WaitForSeconds(_performBoxShosingDelay);
            canEndGame = true;
        }
             
    }
         
}  