using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Chess { 
        

    [RequireComponent(typeof(MeshRenderer))]
    public class Chessman : MonoBehaviour {
        
        public Mesh mesh;
        public Material material;
        public ChessmanType chessmanType = ChessmanType.Pawn;
                
        public static Mesh[] GetMeshesAtPath(string path, ChessmanType chessmanType) {
            string [] files = System.IO.Directory.GetFiles(path,"*", System.IO.SearchOption.AllDirectories);
            // filter files by piece type (by name)
            files = files.Where(f => f.ToLower().Contains(Enum.GetName(typeof(ChessmanType), chessmanType).ToLower())).ToArray();
            Mesh[] result = files.Select(f => AssetDatabase.LoadAssetAtPath(f, typeof(Mesh)))
                            .Where(z => z!=null)
                            .Cast<Mesh>()
                            .ToArray();
            return result;
        }

        public static Material[] GetMaterialsAtPath(string path) {
            return GetAssetsAtPath(path, typeof(Material)).Cast<Material>().ToArray();
        }

        public static UnityEngine.Object[] GetAssetsAtPath(string path, System.Type type) {
            string [] files = System.IO.Directory.GetFiles(path,"*", System.IO.SearchOption.AllDirectories);
            UnityEngine.Object[] result = files.Select(f => AssetDatabase.LoadAssetAtPath(f, type))
                            .Where(z => z!=null)
                            .ToArray();
            return result;
        }

    }
}