using UnityEngine;

namespace UI.Changers.CarChanger {
    
    public class CarChangerController {

        private readonly ChangerItemView _view;
        private readonly SnapScroller _carsScroller;
        private readonly CarsStorage _storage;

        public CarChangerController(ChangerItemView view, CarsStorage storage, SnapScroller scroller) {
            _view = view;
            _storage = storage;
            _carsScroller = scroller;
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
                
                counter++;
            }
        }

        public void Dispose() {
            for (int i = 0; i < _storage.CarCount; i++) {
                _carsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
        }
        
        
        private void OnMapClick(ScrollerPanel obj) {
            
        }

    }
    
}