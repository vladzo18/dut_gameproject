using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.LevelChanger {
    
    public class LevelChanger {

        private readonly ChangerItemView _view;
        private readonly SnapScroller _levelsScroller;
        private readonly MapsStorage _storage;
        private readonly MessageBox _messageBox;

        private Dictionary<ScrollerPanel, int> _changerDataIndexes;

        public LevelChanger(ChangerItemView view, MapsStorage storage, SnapScroller scroller, MessageBox messageBox) {
            _changerDataIndexes = new Dictionary<ScrollerPanel, int>();
            _view = view;
            _storage = storage;
            _levelsScroller = scroller;
            _messageBox = messageBox;
            Init();
        }

        public void Init() {
            _levelsScroller.AddPanels(_storage.CarCount);

            int counter = 0;
            foreach (var mapDescriptor in _storage.CarDescriptors) {
                ChangerItemView view = GameObject.Instantiate(_view, _levelsScroller.GetPanelAt(counter).Rect);
                view.SetBodyImage(mapDescriptor.MapImage);
                view.SetHeadText(mapDescriptor.MapName);
                view.SetItemPrice(mapDescriptor.MapCost.ToString());
                view.SetLockedBoxActivity(mapDescriptor.MapCost > 0);
                _levelsScroller.GetPanelAt(counter).OnPanelClick += OnMapClick;
                
                _changerDataIndexes.Add(_levelsScroller.GetPanelAt(counter), counter);
                
                counter++;
            }
        }

        public void Dispose() {
            for (int i = 0; i < _storage.CarCount; i++) {
                _levelsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
        }
        
        private void OnMapClick(ScrollerPanel panel) {
            if (_levelsScroller.IsScrolling || _levelsScroller.ActivePanel != panel) return;
            
            MapStorageDescriptor descr = _storage.ElementByIndex(_changerDataIndexes[panel]);

            if (descr.MapCost > 0) {
                _messageBox.SetImage(descr.MapImage);
                _messageBox.SetPrice(descr.MapCost.ToString());
                _messageBox.SetTitle(descr.MapName);
                _messageBox.SetContent(descr.Description);
                
               _messageBox.ShowMessageBox();
            }
        }

    }
    
}