using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

namespace Gameplay.LevelGeneration {
    
    public class GroundGenerator : MonoBehaviour, IResettable {

        [SerializeField] private SpriteShapeController _spriteShape;
        [SerializeField] private EdgeCollider2D _groundCollider;
        [SerializeField, Range(2, 10)] private int _distanceBtwnPoints = 4;
        [SerializeField] private GameObject _objectOfObservation;
        [SerializeField] private float _ObservationObjectDistanceToRegeneration = 20;
        [SerializeField] private List<ItemsChunk> _itemsChanks;

        private Vector2 _startPoss;
        private bool _wasShifted;
        
        private Spline _spline;
        private int _pointsAmount = 50;
        private int _generationCounter;
        private int _groundDeep = 10;
        private List<ItemsChunk> _generatedItemsChunks;

        public void SetObjectOfObservation(GameObject o) => _objectOfObservation = o;
        
        private Vector3 GetPositionAtEnd(int index) => _spline.GetPosition(_spline.GetPointCount() - (index + 1));

        private void Start() {
            GameReset.Register(this);
            
            _generatedItemsChunks = new List<ItemsChunk>();
            _spline = _spriteShape.spline;
            _startPoss = _spline.GetPosition(0);

            _spline.SetPosition(0, _spline.GetPosition(0) + Vector3.down * _groundDeep);
            _spline.SetPosition(_spline.GetPointCount() - 1, GetPositionAtEnd(0) + Vector3.down * _groundDeep);
        
            Generate();
        }

        private void Update() {
            if (!_objectOfObservation)  return;
            
            if (_objectOfObservation.transform.position.x > GetPositionAtEnd(0).x - _ObservationObjectDistanceToRegeneration) {
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
            _spline.SetPosition(_spline.GetPointCount() - 1, GetPositionAtEnd(0) + rightDist);
            _spline.SetPosition(_spline.GetPointCount() - 2, GetPositionAtEnd(1) + rightDist);

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
  
            PlaceItems();
        }
        
        [ContextMenu("Clear")]
        private void Clear() {
            _wasShifted = true;
            int amount = _spline.GetPointCount() / 2;
        
            for (int i = 0; i < amount; i++) {
                _spline.RemovePointAt(2);
            }

            _spline.SetPosition(1, new Vector3(_spline.GetPosition(2).x - _distanceBtwnPoints, _spline.GetPosition(2).y));
            _spline.SetPosition(0, new Vector3(_spline.GetPosition(1).x, _spline.GetPosition(0).y));

            _pointsAmount = amount;
            
            ClearItems();
        }

        [ContextMenu("PlaceItems")]
        private void PlaceItems() {
            for (int i = 1; i < _pointsAmount - 2; i++) {
                if (_spline.GetPosition(i + 2).x < _objectOfObservation.transform.position.x + 10) {
                    continue;
                }
                if (i % 10 == 0) {
                    int index = Random.Range(0, _itemsChanks.Count);
                    ItemsChunk go = Instantiate(_itemsChanks[index], new Vector3(_spline.GetPosition(i + 2).x, _spline.GetPosition(i + 2).y + 3f), Quaternion.identity);
                    go.transform.SetParent(this.transform.parent);
                    go.SetObservableCollider(_groundCollider);
                    _generatedItemsChunks.Add(go);
                }
            }

        }

        private void ClearItems() {
            List<ItemsChunk> deleted = new List<ItemsChunk>();
            
            for (int i = 0; i < _generatedItemsChunks.Count; i++) {
                if (_generatedItemsChunks[i].transform.position.x < _objectOfObservation.transform.position.x) {
                    _generatedItemsChunks[i].Clear();
                    deleted.Add(_generatedItemsChunks[i]);
                } 
            }

            foreach (var chank in deleted) {
                _generatedItemsChunks.Remove(chank);
            }
            
        }

        private void ClearAllItems() {
            for (int i = 0; i < _generatedItemsChunks.Count; i++) {
                _generatedItemsChunks[i].Clear();
                _generatedItemsChunks.Remove(_generatedItemsChunks[i]);
            }
        }
        
        private void OnDestroy() => GameReset.Unregister(this);
        
        [ContextMenu("Reset")]
        public void Reset() {
            ClearAllItems();
            
            if (_wasShifted) {
                float offset = Mathf.Abs(_startPoss.x - _spline.GetPosition(0).x);
                
                for (int i = 0; i < _spline.GetPointCount(); i++) {
                    _spline.SetPosition(i, new Vector3(_spline.GetPosition(i).x - offset, _spline.GetPosition(i).y));
                }

                _wasShifted = false;
            }
            
            PlaceItems();
        }
        
    }
}
