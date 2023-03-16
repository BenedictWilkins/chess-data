// https://frarees.github.io/default-gist-license

using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
internal class ReadOnlyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        GUI.enabled = false;
        EditorGUILayout.PropertyField(property, label);
        GUI.enabled = true;
    }
}
