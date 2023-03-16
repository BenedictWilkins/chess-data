using UnityEngine;
using System.Linq;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Chess; 

[CustomEditor(typeof(Controller))]
public class ControllerEditor : Editor {

   // SerializedProperty BoardWidth;
   // SerializedProperty BoardHeight; 
    
    SerializedProperty Board;
    SerializedProperty Chessmen;
    SerializedProperty ChessmanScale;
    SerializedProperty ChessmanHeight;

    SerializedProperty id;

    void OnEnable() {
        Chessmen = serializedObject.FindProperty("Chessmen");
        Board = serializedObject.FindProperty("Board");
        id = serializedObject.FindProperty("id");
        //_chessmen = serializedObject.FindProperty("_chessmen");
        ChessmanHeight = serializedObject.FindProperty("ChessmanHeight");
        ChessmanScale = serializedObject.FindProperty("ChessmanScale");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        //EditorGUILayout.PropertyField(BoardWidth);
        //EditorGUILayout.PropertyField(BoardHeight);
        EditorGUILayout.PropertyField(id);
        EditorGUILayout.PropertyField(Board);
        EditorGUILayout.PropertyField(Chessmen);
        EditorGUILayout.PropertyField(ChessmanScale);
        EditorGUILayout.PropertyField(ChessmanHeight);
        EditorGUILayout.Space(8f);
        if(GUILayout.Button("Populate Default")) {
            Controller contr = serializedObject.targetObject as Controller;
            contr.AddChessman(ChessmanType.Pawn, new Location(2,2));
        }
        if(GUILayout.Button("Populate Random")) {
            Debug.Log("POPULATE RANDOM STUB");
        }
        if(GUILayout.Button("Clear Board")) {
            Controller contr = serializedObject.targetObject as Controller;
            Debug.Log("CLEAR BOARD");
            contr.Clear();
        }
        serializedObject.ApplyModifiedProperties();
    }
}