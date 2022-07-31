using System.Collections.Generic;

namespace General {
    
    public static class GameReset {

        private static List<IResettable> _resetables = new List<IResettable>();

        public static void Register(IResettable resettable) {
            _resetables.Add(resettable);
        }

        public static void Unregister(IResettable resettable) {
            _resetables.Remove(resettable);
        }

        public static void Reset() {
            foreach (var resetable in _resetables) {
                resetable.Reset();
            }
        }
        
    }
    
}