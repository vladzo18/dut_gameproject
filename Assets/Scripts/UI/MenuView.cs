using System;
using System.Collections.Generic;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.LevelChanger;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class MenuView : MonoBehaviour {
        
        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private List<ChangerSwichButton> _swichButtons;
        [Header("CurrencyBox")]
        [SerializeField] private CurrencyBox _currencyBox;
        [Header("Level Changer")]
        [SerializeField] private Canvas _levelsCanvas;
        [SerializeField] private SnapScroller _levelsScroller;
        [SerializeField] private MapsStorage _mapsStorage;
        [Header("Car Changer")]
        [SerializeField] private Canvas _carsCanvas;
        [SerializeField] private SnapScroller _carsScroller;
        [SerializeField] private CarsStorage _carsStorage;
        [Header("Car Tuner")]
        [SerializeField] private Canvas _tuneCanvas;
        [Header("Other")]
        [SerializeField] private ChangerItemView _changerItemPrefab;
        [SerializeField] private MessageBox _messageBox;

        public event Action OnPlayClick;
        
        public IEnumerable<ChangerSwichButton> SwichButtons => _swichButtons;
        public Canvas LevelsCanvas => _levelsCanvas;
        public Canvas CarsCanvas => _carsCanvas;
        public Canvas TuneCanvas => _tuneCanvas;
        public SnapScroller LevelsScroller => _levelsScroller;
        public MapsStorage MapsStorage => _mapsStorage;
        public SnapScroller CarsScroller => _carsScroller;
        public CarsStorage CarsStorage => _carsStorage;
        public ChangerItemView ChangerItemPrefab => _changerItemPrefab;
        public MessageBox MessageBox => _messageBox;
        public CurrencyBox CurrencyBox => _currencyBox;

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

