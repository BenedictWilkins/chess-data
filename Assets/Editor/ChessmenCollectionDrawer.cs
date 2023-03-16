using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(ChessmenCollection))]
public class ChessmenCollectionDrawer : PropertyDrawer {
    public bool foldout; 
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        
        position.height = 16f;
        foldout = EditorGUI.Foldout(position, foldout, label);
        EditorGUI.indentLevel = 1;
        if (foldout) {
            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Pawn"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Knight"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Bishop"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Rook"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Queen"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("King"));
        }
        

        /*
        // Calculate rects
        var pawnRect = new Rect(position.x, position.y, position.width, position.height);
        var knightRect = new Rect(position.x, position.y, position.width, position.height);
        var bishopRect = new Rect(position.x, position.y, position.width, position.height);
        var rookRect = new Rect(position.x, position.y, position.width, position.height);
        var queenRect = new Rect(position.x, position.y, position.width, position.height);
        var kingRect = new Rect(position.x, position.y, position.width, position.height);
        EditorGUI.PropertyField(pawnRect, property.FindPropertyRelative("Pawn"));
        EditorGUI.PropertyField(knightRect, property.FindPropertyRelative("Knight"), GUIContent.none);
        EditorGUI.PropertyField(bishopRect, property.FindPropertyRelative("Bishop"), GUIContent.none);
        EditorGUI.PropertyField(rookRect, property.FindPropertyRelative("Rook"), GUIContent.none);
        EditorGUI.PropertyField(queenRect, property.FindPropertyRelative("Queen"), GUIContent.none);
        EditorGUI.PropertyField(kingRect, property.FindPropertyRelative("King"), GUIContent.none);
        */
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}