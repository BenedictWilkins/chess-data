using UnityEngine;
using System.Linq;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Chess;

[CustomEditor(typeof(Chessman))]
public class ChessmanEditor : Editor {

    public static string MESH_PATH = "Assets/3D";
    public static string MATERIAL_PATH = "Assets/Materials";

    SerializedProperty mesh;
    SerializedProperty material;
    SerializedProperty chessmanType;

    int _meshChoiceIndex;
    int _materialChoiceIndex;

    void OnEnable() {
        mesh = serializedObject.FindProperty("mesh");
        material = serializedObject.FindProperty("material");
        chessmanType = serializedObject.FindProperty("chessmanType");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        
        string[] _choices;
        
        EditorGUI.BeginChangeCheck();
        ChessmanType value = (ChessmanType) EditorGUILayout.EnumPopup(chessmanType.displayName, (ChessmanType) chessmanType.enumValueIndex);
        
        Mesh[] meshes = Chessman.GetMeshesAtPath(MESH_PATH, value);
        _choices = meshes.Select(a => AssetDatabase.GetAssetPath(a)).Select(s => s.Replace("/", "\\")).ToArray();

        if(EditorGUI.EndChangeCheck()) {
            chessmanType.enumValueIndex = (int) value;
            _choices = meshes.Select(a => AssetDatabase.GetAssetPath(a)).Select(s => s.Replace("/", "\\")).ToArray();
            Chessman chessman = serializedObject.targetObject as Chessman;
            chessman.gameObject.GetComponent<MeshFilter>().sharedMesh = meshes[_meshChoiceIndex];
        }
        
        
        EditorGUI.BeginChangeCheck();
        _meshChoiceIndex = EditorGUILayout.Popup("Mesh", _meshChoiceIndex, _choices);
        if(EditorGUI.EndChangeCheck()) {
            Chessman chessman = serializedObject.targetObject as Chessman;
            chessman.gameObject.GetComponent<MeshFilter>().sharedMesh = meshes[_meshChoiceIndex];
        }

        Material[] materials = Chessman.GetMaterialsAtPath(MATERIAL_PATH);
        _choices = materials.Select(a => AssetDatabase.GetAssetPath(a))
                                .Select(s => s.Replace("/", "\\"))
                                .ToArray();
        EditorGUI.BeginChangeCheck();
        _materialChoiceIndex = EditorGUILayout.Popup("Material", _materialChoiceIndex, _choices);
        if(EditorGUI.EndChangeCheck()) {
            Chessman chessman = serializedObject.targetObject as Chessman;
            chessman.gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[_materialChoiceIndex];
        }
        serializedObject.ApplyModifiedProperties();
    }
}