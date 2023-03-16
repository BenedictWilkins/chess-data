using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    [ExecuteInEditMode]
    public class Controller : MonoBehaviour {
        
        [ReadOnly]
        public string id = System.Guid.NewGuid().ToString();
        // prefabs
        public GameObject Board; 
        public ChessmenCollection Chessmen = new ChessmenCollection();

        [Range(0.4f,1.0f), Tooltip("Scale of pieces.")]
        public float ChessmanScale = 1f;
        [Range(0.6f,1.4f), Tooltip("Height of pieces.")]
        public float ChessmanHeight = 1f;

        [Serializable] // so that editor serialisation works and is not forgotten on play...
        protected class ChessmenDict : SerializableDictionary<Location, Chessman> {
            public new void Add(Location key, Chessman val) {
                var (x,y) = key;
                if (!(x.InRange(0, Chess.Board.CELLS_X) && 
                    y.InRange(0, Chess.Board.CELLS_Y))) {
                    throw new InvalidOperationException($"Location ({x},{y}) is out of bounds.");
                } else {
                    base.Add(key, val);
                }
            }
        }
        [SerializeField]
        protected ChessmenDict _chessmen = new ChessmenDict();

        public void Awake() {

        }

        public void Clear() {
            foreach(var kv in _chessmen) {
                if (kv.Value) {
                    DestroyImmediate(kv.Value.gameObject);
                }   
            }
            _chessmen.Clear();
        }

        public void Update() {
            Debug.Log(_chessmen.Count());
            foreach(var kv in _chessmen) {
                
            }
        }

        public void AddChessman(ChessmanType type, Location location) {
            // create chessman
            var (x,y) = location;
            if (!_chessmen.ContainsKey(location)) {
                GameObject go = Chessmen.InstantiateChessman(type);
                Chessman chessman = go.GetComponent<Chessman>();
                _chessmen.Add(location, chessman);
                //chessman.AttachController(this, location);
                PositionChessManAt(location);
                ScaleChessmanAt(location);        
            } else {
                throw new InvalidOperationException($"Cannot add a piece {type} to location ({x},{y}) as a piece already exists there.");
            }
        }

        public void PositionChessManAt(Location location) {
            var (x,y) = location;
            GameObject chessman = _chessmen[location].gameObject;
            Chess.Board board = Board.GetComponent<Chess.Board>();
            Bounds bounds = board.GetBoardBounds();
            Vector3 corner = bounds.center - bounds.extents;
            Vector3 position =  corner + Vector3.Scale((2f * bounds.extents), new Vector3((float)x/(float)Chess.Board.CELLS_X, 1, (float)y/(float)Chess.Board.CELLS_Y));
            chessman.transform.position = position + board.GetCellSize()/2f;
        }
        
        public void ScaleChessmanAt(Location location) {
            if (_chessmen.ContainsKey(location)) { 
                GameObject chessman = _chessmen[location].gameObject;
                Chess.Board board = Board.GetComponent<Chess.Board>();
                Bounds bounds = board.GetBoardBounds();
                Vector3 scale = Vector3.Scale(bounds.size, Chess.Board.GetCellCount().Invert()); // cell size
                scale = ChessmanScale * Vector3.Scale(scale, chessman.GetActualSize().Invert());
                scale[1] = scale[0] * ChessmanHeight; // scale chessman height with its width...
                chessman.transform.localScale = Vector3.Scale(chessman.transform.localScale, scale);
            }
        }
    }
}