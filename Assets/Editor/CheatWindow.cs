using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor {
    
    public class CheatWindow : EditorWindow {

        private const string TITLE_TEXT = "Cheat Window";    
        private const string VISUAL_TREE_ASSET_PATH = "Assets/Editor/CheatWindow.uxml";
        private const string STYLE_SHEET_ASSET_PATH = "Assets/Editor/CheatWindow.uss";
        
        [MenuItem("Custom Tool/" + TITLE_TEXT)]
        public static void ShowExample() {
            CheatWindow window = GetWindow<CheatWindow>();
            window.titleContent = new GUIContent(TITLE_TEXT);
        }

        public void CreateGUI() {
            VisualElement root = rootVisualElement;
        
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUAL_TREE_ASSET_PATH);
            root.Add(visualTree.Instantiate());
        }
        
    }
    
}