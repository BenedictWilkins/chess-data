using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ChessmenCollection {
    
    // prefabs
    public GameObject Pawn; 
    public GameObject Knight;
    public GameObject Bishop;
    public GameObject Rook;
    public GameObject King;
    public GameObject Queen;

    public GameObject InstantiateChessman(ChessmanType type) {
        switch (type) {
            case ChessmanType.Pawn:
                return GameObject.Instantiate(Pawn, new Vector3(0, 0, 0), Quaternion.identity);
            case ChessmanType.Knight:
                return GameObject.Instantiate(Knight, new Vector3(0, 0, 0), Quaternion.identity);
            case ChessmanType.Bishop:
                return GameObject.Instantiate(Bishop, new Vector3(0, 0, 0), Quaternion.identity);
            case ChessmanType.Rook:
                return GameObject.Instantiate(Rook, new Vector3(0, 0, 0), Quaternion.identity);
            case ChessmanType.King:
                return GameObject.Instantiate(King, new Vector3(0, 0, 0), Quaternion.identity);
            case ChessmanType.Queen:
                return GameObject.Instantiate(Queen, new Vector3(0, 0, 0), Quaternion.identity);
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}