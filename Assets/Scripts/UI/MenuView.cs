using System;
using System.Collections.Generic;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.LevelChanger;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class MenuView : MonoBehaviour {

        [SerializeField] private Button _playButton;
        [SerializeField] private List<ChangerSwichButton> _swichButtons;
        [SerializeField] private Canvas _levelsCanvas;
        [SerializeField] private Canvas _carsCanvas;
        [SerializeField] private Canvas _tuneCanvas;
        [SerializeField] private SnapScroller _levelsScroller;
        [SerializeField] private MapsStorage _mapsStorage;
        [SerializeField] private SnapScroller _carsScroller;
        [SerializeField] private CarsStorage _carsStorage;
        [SerializeField] private ChangerItemView _changerItemPrefab;

        public IEnumerable<ChangerSwichButton> SwichButtons => _swichButtons;
        public Canvas LevelsCanvas => _levelsCanvas;
        public Canvas CarsCanvas => _carsCanvas;
        public Canvas TuneCanvas => _tuneCanvas;
        public SnapScroller LevelsScroller => _levelsScroller;
        public MapsStorage MapsStorage => _mapsStorage;
        public SnapScroller CarsScroller => _carsScroller;
        public CarsStorage CarsStorage => _carsStorage;
        public ChangerItemView ChangerItemPrefab => _changerItemPrefab;

        public event Action OnPlayClick;

        private void Start() {
            _playButton.onClick.AddListener(PlayButtonClicked);
        }
        
        private void OnDestroy() {
            _playButton.onClick.RemoveListener(PlayButtonClicked);
        }
        
        private void PlayButtonClicked() {
            OnPlayClick?.Invoke();
        }
        
    }
    
}

