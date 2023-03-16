using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class FBXMeshFix : EditorWindow {

    //private static string _targetExtension = ".asset";
    //public Vector3 Rotation = Vector3.up;

    void OnGUI() {
        EditorGUILayout.LabelField("Change Mesh Rotation", EditorStyles.wordWrappedLabel);
        GUILayout.Space(10);
        if (GUILayout.Button("Correct Z-Up")) {
            for (int i = 0; i < Selection.objects.Length; i++) {
                Mesh mesh = Selection.objects[i] as Mesh;
                Vector3[] verts = RotateVertices(mesh.vertices, new Vector3(-90,0,0), mesh.bounds.center);
                mesh.SetVertices(verts);
                mesh.RecalculateBounds();
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            //Close();
        }
        if (GUILayout.Button("Center")) {
            for (int i = 0; i < Selection.objects.Length; i++) {
                Mesh mesh = Selection.objects[i] as Mesh;
                // center the mesh
                Vector3[] verts = TranslateVerticies(mesh.vertices, -mesh.bounds.center);
                mesh.SetVertices(verts);
                mesh.RecalculateBounds();
                // mesh bottom should be the y extent
                verts = TranslateVerticies(mesh.vertices,  Vector3.Scale(new Vector3(0,1,0), mesh.bounds.extents));
                mesh.SetVertices(verts);
                mesh.RecalculateBounds();

            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            //Close();
        }
       
        
        if (GUILayout.Button("Cancel")) Close();
    }

    [MenuItem("Assets/FBX/Fix", validate = true)]
    private static bool ExtractMeshesMenuItemValidate() {
        for (int i = 0; i < Selection.objects.Length; i++) {
            //Debug.Log($"{Selection.objects[i].GetType()} {Selection.objects[i] is Mesh}");
            if (!(Selection.objects[i] is Mesh)) {
                return false;
            }
        }
        return true;
    }

    [MenuItem("Assets/FBX/Fix")]
    private static void ExtractMeshesMenuItem() {
        FBXMeshFix window = CreateInstance<FBXMeshFix>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
        window.ShowUtility();
    }

    Vector3[] TranslateVerticies(Vector3[] vertices, Vector3 translate) {
        for (int i = 0; i < vertices.Length; i++) { 
            vertices[i] += translate; 
        } 
        return vertices;
    }

    Vector3[] RotateVertices(Vector3[] vertices, Vector3 rotation, Vector3 center) { 
        Quaternion rotationQuaternion = Quaternion.Euler(rotation); 
        for (int i = 0; i < vertices.Length; i++) { 
            vertices[i] = rotationQuaternion * (vertices[i] - center) + center; 
        } 
        return vertices; 
    }
}