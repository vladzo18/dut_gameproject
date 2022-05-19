using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.CarChanger {
    
    public class CarChanger {

        private readonly ChangerItemView _view;
        private readonly SnapScroller _carsScroller;
        private readonly CarsStorage _storage;
        private readonly MessageBox _messageBox;
        
        private Dictionary<ScrollerPanel, int> _changerDataIndexes;

        public CarChanger(ChangerItemView view, CarsStorage storage, SnapScroller scroller, MessageBox messageBox) {
            _changerDataIndexes = new Dictionary<ScrollerPanel, int>();
            _view = view;
            _storage = storage;
            _carsScroller = scroller;
            _messageBox = messageBox;
            Init();
        }

        public void Init() {
            _carsScroller.AddPanels(_storage.CarCount);

            int counter = 0;
            foreach (var mapDescriptor in _storage.CarDescriptors) {
                ChangerItemView view =  GameObject.Instantiate(_view, _carsScroller.GetPanelAt(counter).Rect);
                view.SetBodyImage(mapDescriptor.CarImage);
                view.SetHeadText(mapDescriptor.CarName);
                view.SetItemPrice(mapDescriptor.CarCost.ToString());
                view.SetLockedBoxActivity(mapDescriptor.CarCost > 0);
                _carsScroller.GetPanelAt(counter).OnPanelClick += OnMapClick;
                
                _changerDataIndexes.Add(_carsScroller.GetPanelAt(counter), counter);
                
                counter++;
            }
        }

        public void Dispose() {
            for (int i = 0; i < _storage.CarCount; i++) {
                _carsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
        }
        
        
        private void OnMapClick(ScrollerPanel panel) {
            if (_carsScroller.IsScrolling || _carsScroller.ActivePanel != panel) return;
            
           CarStorageDescriptor descr = _storage.ElementByIndex(_changerDataIndexes[panel]);

            if (descr.CarCost > 0) {
                _messageBox.SetImage(descr.CarImage);
                _messageBox.SetPrice(descr.CarCost.ToString());
                _messageBox.SetTitle(descr.CarName);
                _messageBox.SetContent(descr.Description);
                
                _messageBox.ShowMessageBox();
            }
        }

    }
    
}