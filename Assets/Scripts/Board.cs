using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess { 


    [ExecuteInEditMode]
    public class Board : MonoBehaviour {
    
        public const int CELLS_X = 8;
        public const int CELLS_Y = 8;

        public static Vector3 GetCellCount() {
            return new Vector3(CELLS_X, 1, CELLS_Y);
        }

        public const float BOARD_WIDTH = 10;
        public const float BOARD_HEIGHT = 10;

        // this is computed by looking at the object geometry
        //protected Vector3 up = Vector3.up; 

        [MinMaxSlider(-1,1)]
        public Vector2 BoardWidth;
        [MinMaxSlider(-1,1)]
        public Vector2 BoardHeight;

        public Vector3 GetCellSize() {
            Vector3 cellsize = Vector3.Scale(GetBoardBounds().size, Chess.Board.GetCellCount().Invert());
            cellsize[1] = 0;
            return cellsize;
        }

        public Bounds GetBoardBounds() {
            Vector3 extent = gameObject.GetActualSize() / 2f; 
            Vector3 nextent = Vector3.Scale(extent, new Vector3(BoardWidth[0], 1, BoardHeight[0]));
            Vector3 pextent = Vector3.Scale(extent, new Vector3(BoardWidth[1], 1, BoardHeight[1]));
            Vector3 center = (transform.position + nextent + pextent) / 2f;
            return new Bounds(center, pextent - nextent);
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Bounds bounds = GetBoardBounds();
            var p1 = bounds.center + bounds.extents;
            var p3 = bounds.center - bounds.extents;
            var p2 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1,1,1));
            var p4 = bounds.center + Vector3.Scale(bounds.extents, new Vector3(1,1,-1));
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
            Gizmos.DrawSphere(p1, 0.1f);
            Gizmos.DrawSphere(p2, 0.1f);
            Gizmos.DrawSphere(p3, 0.1f);
            Gizmos.DrawSphere(p4, 0.1f);
        }

        void OnValidate() {
            // change the size of this object to match the const BOARD_WIDTH BOARD_WIDTH if the values are altered.
            Vector3 size = gameObject.GetActualSize();
            float width = size[0];
            float height = size[2];
            // scale to match
            float wscale = BOARD_WIDTH / width;
            float hscale = BOARD_HEIGHT / height;
            float zscale = wscale; // scale depth...
            gameObject.transform.localScale =  Vector3.Scale(gameObject.transform.localScale , new Vector3(wscale, zscale, hscale));
        }
    }
}