using System.Collections.Generic;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertiesChanger {

        private readonly IEnumerable<CarTunerBoxView> _carTunerBoxViews;

        private List<CarTunerBoxController> _tunerBoxControllers;
        
        public CarPropertiesChanger(IEnumerable<CarTunerBoxView> carTunerBoxViews) {
            _tunerBoxControllers = new List<CarTunerBoxController>();
            _carTunerBoxViews = carTunerBoxViews;
        }

        public void Init() {
            foreach (var boxView in _carTunerBoxViews) {
                CarTunerBoxController controller = new CarTunerBoxController(boxView);
                controller.Init();
                _tunerBoxControllers.Add(controller);
            }
        }

        public void Dispose() {
            foreach (var boxController in _tunerBoxControllers) {
               boxController.Dispose();
            }
        }
        
    }
    
}