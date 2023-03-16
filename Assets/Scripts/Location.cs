using UnityEngine;
using System;

namespace Chess {

    [Serializable]
    public struct Location {
        int x; int y;
        public Location(int x, int y) {
            this.x = x; this.y = y;
        }
        public void Deconstruct(out int x, out int y) {
            x = this.x; y = this.y;
        }
        public override string ToString() {
            return $"({this.x},{this.y})";
        }
    }
}