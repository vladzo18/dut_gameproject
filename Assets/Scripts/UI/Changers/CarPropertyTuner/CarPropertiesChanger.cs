using System;
using System.Collections.Generic;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertiesChanger {

        private readonly IEnumerable<CarTunerBoxView> _carTunerBoxViews;

        private List<CarTunerBoxController> _tunerBoxControllers;

        public event Action OnPointsChanged;
        
        public CarPropertiesChanger(IEnumerable<CarTunerBoxView> carTunerBoxViews) {
            _tunerBoxControllers = new List<CarTunerBoxController>();
            _carTunerBoxViews = carTunerBoxViews;
        }

        public void Init() {
            foreach (var boxView in _carTunerBoxViews) {
                CarTunerBoxController controller = new CarTunerBoxController(boxView);
                controller.Init();
                _tunerBoxControllers.Add(controller);
                boxView.OnUpgradeDownCick += PointsChangedClickHandler;
                boxView.OnUpgradeUpCick += PointsChangedClickHandler;
            }
        }
        
        public void Dispose() {
            foreach (var boxController in _tunerBoxControllers) {
               boxController.Dispose();
            }
            foreach (var boxView in _carTunerBoxViews) {
                boxView.OnUpgradeDownCick -= PointsChangedClickHandler;
                boxView.OnUpgradeUpCick -= PointsChangedClickHandler;
            }
        }

        private void PointsChangedClickHandler() => OnPointsChanged?.Invoke();

    }
    
}