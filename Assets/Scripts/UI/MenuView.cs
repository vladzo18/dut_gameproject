using System;
using System.Collections.Generic;
using UI.Changers;
using UI.Changers.CarChanger;
using UI.Changers.CarPropertyTuner;
using UI.Changers.MapChanger;
using UI.Changers.Scroller;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class MenuView : MonoBehaviour {
        
        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private List<ChangerSwitchButton> switchButtons;
        [Header("CurrencyBox")]
        [SerializeField] private CurrencyBox _currencyBox;
        [Header("Level Changer")]
        [SerializeField] private Canvas _levelsCanvas;
        [SerializeField] private CustomScroller _levelsScroller;
        [SerializeField] private MapsStorage _mapsStorage;
        [Header("Car Changer")]
        [SerializeField] private Canvas _carsCanvas;
        [SerializeField] private CustomScroller _carsScroller;
        [SerializeField] private CarsStorage _carsStorage;
        [Header("Car Tuner")]
        [SerializeField] private Canvas _tuneCanvas;
        [SerializeField] private List<CarTunerBoxView> _carTunerBoxViews;
        [Header("Other")]
        [SerializeField] private ChangerItemView _changerItemPrefab;
        [SerializeField] private BuyMessageBox buyMessageBox;
        [Header("Audio")]
        [SerializeField] private AudioSource _uiAudioSource;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _buySound;

        public event Action OnPlayClick;
        
        public IEnumerable<ChangerSwitchButton> SwitchButtons => switchButtons;
        public IEnumerable<CarTunerBoxView> CarTunerBoxViews => _carTunerBoxViews;
        public Canvas LevelsCanvas => _levelsCanvas;
        public Canvas CarsCanvas => _carsCanvas;
        public Canvas TuneCanvas => _tuneCanvas;
        public CustomScroller LevelsScroller => _levelsScroller;
        public MapsStorage MapsStorage => _mapsStorage;
        public CustomScroller CarsScroller => _carsScroller;
        public CarsStorage CarsStorage => _carsStorage;
        public ChangerItemView ChangerItemPrefab => _changerItemPrefab;
        public BuyMessageBox BuyMessageBox => buyMessageBox;
        public CurrencyBox CurrencyBox => _currencyBox;
        public AudioSource UIAudioSource => _uiAudioSource;
        public AudioClip ClickSound => _clickSound;
        public AudioClip BuySound => _buySound;

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

