using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

namespace Gameplay.LevelGeneration {
    
    public class GraundGenerator : MonoBehaviour {

        [SerializeField] private SpriteShapeController _spriteShape;
        [SerializeField] private EdgeCollider2D _groundCollider;
        [SerializeField, Range(2, 10)] private int _distanceBtwnPoints = 4;
        [SerializeField] private GameObject _objectOfObservation;
        [SerializeField] private float _ObservationObjectDistanceToRegeneration = 20;
        [SerializeField] private List<ItemsChank> _itemsChanks;

        private Spline _spline;
        private int _pointsAmount = 50;
        private int _generationCounter;
        private int _groundDeep = 10;
        private List<ItemsChank> _generatedItemsChanks;

        public void SetObjectOfObservation(GameObject o) => _objectOfObservation = o;
        
        private Vector3 GetPossitionAtEnd(int index) => _spline.GetPosition(_spline.GetPointCount() - (index + 1));

        private void Start() {
            _generatedItemsChanks = new List<ItemsChank>();
            _spline = _spriteShape.spline;

            _spline.SetPosition(0, _spline.GetPosition(0) + Vector3.down * _groundDeep);
            _spline.SetPosition(_spline.GetPointCount() - 1, GetPossitionAtEnd(0) + Vector3.down * _groundDeep);
        
            Generate();
        }

        private void Update() {
            if (_objectOfObservation == null)  return;
            
            if (_objectOfObservation.transform.position.x > GetPossitionAtEnd(0).x - _ObservationObjectDistanceToRegeneration) {
                if (_generationCounter == 2) {
                    Clear();
                    _generationCounter = 1;
                }
                Generate();
            }
        }
        
        [ContextMenu("Generate")]
        private void Generate() {
            Vector3 rightDist = (Vector3.right * (_pointsAmount * _distanceBtwnPoints));
            _spline.SetPosition(_spline.GetPointCount() - 1, GetPossitionAtEnd(0) + rightDist);
            _spline.SetPosition(_spline.GetPointCount() - 2, GetPossitionAtEnd(1) + rightDist);

            int newIndex = _spline.GetPointCount() - 4;
            _pointsAmount += newIndex;
        
            for (int i = newIndex; i < _pointsAmount; i++) {
                float xPos = _spline.GetPosition(i + 1).x + _distanceBtwnPoints;
                _spline.InsertPointAt(i + 2, new Vector3(xPos, 4 * Mathf.PerlinNoise(i * Random.Range(5.0f, 15.0f), 0)));
            }

            for (int i = newIndex + 2; i < _pointsAmount + 2; i++) {
                _spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spline.SetLeftTangent(i, new Vector3(-2f, 0, 0));
                _spline.SetRightTangent(i, new Vector3(2f, 0, 0));
            }

            _generationCounter++;
  
            PraceItems();
        }
        
        [ContextMenu("Clear")]
        private void Clear() {
            int amount = _spline.GetPointCount() / 2;
        
            for (int i = 0; i < amount; i++) {
                _spline.RemovePointAt(2);
            }

            _spline.SetPosition(1, new Vector3(_spline.GetPosition(2).x - _distanceBtwnPoints, _spline.GetPosition(2).y));
            _spline.SetPosition(0, new Vector3(_spline.GetPosition(1).x, _spline.GetPosition(0).y));

            _pointsAmount = amount;
            
            ClearItems();
        }

        [ContextMenu("PraceItems")]
        private void PraceItems() {
            for (int i = 1; i < _pointsAmount - 2; i++) {
                if (_spline.GetPosition(i + 2).x < _objectOfObservation.transform.position.x + 10) {
                    continue;
                }
                if (i % 10 == 0) {
                    int index = Random.Range(0, _itemsChanks.Count);
                    ItemsChank go = Instantiate(_itemsChanks[index], new Vector3(_spline.GetPosition(i + 2).x, _spline.GetPosition(i + 2).y + 3f), Quaternion.identity);
                    go.transform.SetParent(this.transform.parent);
                    go.SetObservableColider(_groundCollider);
                    _generatedItemsChanks.Add(go);
                }
            }

        }

        private void ClearItems() {
            List<ItemsChank> deleted = new List<ItemsChank>();
            
            for (int i = 0; i < _generatedItemsChanks.Count; i++) {
                if (_generatedItemsChanks[i].transform.position.x < _objectOfObservation.transform.position.x) {
                    _generatedItemsChanks[i].Clear();
                    deleted.Add(_generatedItemsChanks[i]);
                } 
            }

            foreach (var chank in deleted) {
                _generatedItemsChanks.Remove(chank);
            }
            
        }
        
    }
}
