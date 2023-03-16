// https://frarees.github.io/default-gist-license

using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Chess; 

[CustomPropertyDrawer(typeof(AssetSelectAttribute))]
internal class AssetSelectDrawer : PropertyDrawer {
    
    int _choiceIndex;
 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        AssetSelectAttribute attr  = attribute as AssetSelectAttribute;
        Object[] assets = Chessman.GetAssetsAtPath(attr.AssetsPath, fieldInfo.FieldType);
        string[] _choices = assets.Select(a => AssetDatabase.GetAssetPath(a))
                                .Select(s => s.Replace("/", "\\"))
                                .ToArray();
        EditorGUI.BeginChangeCheck();
        _choiceIndex = EditorGUI.Popup(position, _choiceIndex, _choices);
        if(EditorGUI.EndChangeCheck()) {
            Debug.Log("CHANGED");
        }
    }
}
