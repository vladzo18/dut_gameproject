using General;
using UI;
using UI.Changers.CarChanger;
using UI.Changers.MapChanger;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Editor {

    public class CheatWindow : EditorWindow {

        private const string TITLE_TEXT = "Cheat Window";
        private const string VISUAL_TREE_ASSET_PATH = "Assets/Editor/CheatWindow.uxml";

        private VisualElement _root;
        
        [MenuItem("Custom Tool/" + TITLE_TEXT)]
        public static void Init() {
            CheatWindow window = GetWindow<CheatWindow>();
            window.titleContent = new GUIContent(TITLE_TEXT);
            window.minSize = new Vector2(200, 150);
            window.maxSize = new Vector2(400, 290);
            window.Show();
        }

        public void CreateGUI() {
            _root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUAL_TREE_ASSET_PATH);
            _root.Add(visualTree.Instantiate());
            
            _root.Query<Button>(className: "coins_add_btn").First().clicked += OnCoinsAddBtnClick;
            _root.Query<Button>(className: "maps_reset_btn").First().clicked += OnMapsResetBtnClick;
            _root.Query<Button>(className: "cars_reset_btn").First().clicked += OnCarsResetBtnClick;
        }

        private T GetElementFromActiveScene<T>() where T : class {
            var sceneObj= SceneManager.GetActiveScene().GetRootGameObjects();
            T currencyBox = null;
            
            foreach (var o in sceneObj) {
                currencyBox = o.GetComponentInChildren<T>();
                if (currencyBox != null) {
                    break;
                }
            }

            return currencyBox;
        }
        
        private void OnCoinsAddBtnClick() {
            if (!Application.isPlaying || SceneManager.GetActiveScene().name != SceneSwitcher.MENU_SCENE_KEY) {
                this.ShowNotification(new GUIContent("You need to be in plaing mode and in Menu scene"), 2.5f);
                return;
            }
            
            CurrencyBox currencyBox = GetElementFromActiveScene<CurrencyBox>();

            currencyBox.AddCoins(10000);
        }

        private void OnMapsResetBtnClick() {
            if (Application.isPlaying) {
                this.ShowNotification(new GUIContent("You need to be in editor mode"));
                return;
            }
            
           if (PlayerPrefs.HasKey(nameof(MapsModel))) {
              PlayerPrefs.DeleteKey(nameof(MapsModel));
              this.ShowNotification(new GUIContent("Finished!"));
           } else {
               this.ShowNotification(new GUIContent("Already deleted!"));
           }
        }
        
        private void OnCarsResetBtnClick() {
            if (Application.isPlaying) {
                this.ShowNotification(new GUIContent("You need to be in editor mode"));
                return;
            }
            
            if (PlayerPrefs.HasKey(nameof(CarsModel))) {
                PlayerPrefs.DeleteKey(nameof(CarsModel));
                this.ShowNotification(new GUIContent("Finished!"));
            } else {
                this.ShowNotification(new GUIContent("Already deleted!"));
            }
        }
        
        private void OnDestroy() {
            _root.Query<Button>(className: "coins_add_btn").First().clicked -= OnCoinsAddBtnClick;
            _root.Query<Button>(className: "maps_reset_btn").First().clicked -= OnMapsResetBtnClick;
            _root.Query<Button>(className: "cars_reset_btn").First().clicked -= OnCarsResetBtnClick;
        }
        
    }
    
}