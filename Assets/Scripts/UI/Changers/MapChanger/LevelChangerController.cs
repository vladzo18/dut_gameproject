using UnityEngine;

namespace UI.Changers.LevelChanger {
    
    public class LevelChangerController {

        private readonly ChangerItemView _view;
        private readonly SnapScroller _levelsScroller;
        private readonly MapsStorage _storage;

        public LevelChangerController(ChangerItemView view, MapsStorage storage, SnapScroller scroller) {
            _view = view;
            _storage = storage;
            _levelsScroller = scroller;
            Init();
        }

        public void Init() {
            _levelsScroller.AddPanels(_storage.CarCount);

            int counter = 0;
            foreach (var mapDescriptor in _storage.CarDescriptors) {
                ChangerItemView view =  GameObject.Instantiate(_view, _levelsScroller.GetPanelAt(counter).Rect);
                view.SetBodyImage(mapDescriptor.CarImage);
                view.SetHeadText(mapDescriptor.CarName);
                view.SetItemPrice(mapDescriptor.CarCost.ToString());
                view.SetLockedBoxActivity(mapDescriptor.CarCost > 0);
                _levelsScroller.GetPanelAt(counter).OnPanelClick += OnMapClick;
                
                counter++;
            }
        }

        public void Dispose() {
            for (int i = 0; i < _storage.CarCount; i++) {
                _levelsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
        }
        
        
        private void OnMapClick(ScrollerPanel obj) {
            
        }

    }
    
}