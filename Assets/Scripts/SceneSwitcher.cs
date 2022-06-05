using UnityEngine.SceneManagement;

namespace General {
    
    public static class SceneSwitcher {

        public const string MENU_SCENE_KEY = "Menu";
        public const string GAME_SCENE_KEY = "Game";
        
        public static void LoadScene(string key) {
            SceneManager.LoadScene(key);
        }
        
    }
    
}