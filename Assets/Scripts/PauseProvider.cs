using UnityEngine;

namespace General {
    public static class PauseProvider {

        public static bool IsPaused { get; private set; }

        public static void ApplyPause() => Time.timeScale = 0f;
        public static void RevokePause() => Time.timeScale = 1f;

    }
}