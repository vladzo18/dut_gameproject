using System.Collections.Generic;

namespace General {
    
    public class GameReset {

        private static List<IResetable> _resetables = new List<IResetable>();

        public static void Register(IResetable resetable) {
            _resetables.Add(resetable);
        }

        public static void Unregister(IResetable resetable) {
            _resetables.Remove(resetable);
        }

        public static void Reset() {
            foreach (var resetable in _resetables) {
                resetable.Reset();
            }
        }
        
    }
    
}