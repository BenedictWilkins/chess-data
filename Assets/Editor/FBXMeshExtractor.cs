
using System.IO;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class FBXMeshExtractor
{
    private static string _progressTitle = "Extracting Meshes";
    private static string _sourceExtension = ".fbx";
    private static string _targetExtension = ".asset";

    private static int _unique = 0;

    [MenuItem("Assets/FBX/Extract", validate = true)]
    private static bool ExtractMeshesMenuItemValidate() {
        for (int i = 0; i < Selection.objects.Length; i++) {
            //if (!AssetDatabase.GetAssetPath(Selection.objects[i]).EndsWith(_sourceExtension))
            //    return false;
        }
        return true;
    }

    [MenuItem("Assets/FBX/Extract")]
    private static void ExtractMeshesMenuItem() {
        _unique = 0;
        EditorUtility.DisplayProgressBar(_progressTitle, "", 0);
        for (int i = 0; i < Selection.objects.Length; i++) {
            EditorUtility.DisplayProgressBar(_progressTitle, Selection.objects[i].name, (float)i / (Selection.objects.Length - 1));
            Extract(Selection.objects[i]);
        }
        EditorUtility.ClearProgressBar();
    }

    private static void Extract(Object selectedObject) {
        //Create Folder Hierarchy
        string selectedObjectPath = AssetDatabase.GetAssetPath(selectedObject);
        string parentfolderPath = selectedObjectPath.Substring(0, selectedObjectPath.Length - (selectedObject.name.Length + 5));
        string objectFolderName = selectedObject.name;
        string objectFolderPath = parentfolderPath + "/" + objectFolderName;
        string meshFolderName = "Meshes";
        string meshFolderPath = objectFolderPath + "/" + meshFolderName;
        string matFolderName = "Materials";
        string matFolderPath = objectFolderPath + "/" + matFolderName;

        if (!AssetDatabase.IsValidFolder(objectFolderPath)) {
            AssetDatabase.CreateFolder(parentfolderPath, objectFolderName);
            if (!AssetDatabase.IsValidFolder(meshFolderPath)) {
                AssetDatabase.CreateFolder(objectFolderPath, meshFolderName);
            }
            if (!AssetDatabase.IsValidFolder(matFolderPath)) {
                AssetDatabase.CreateFolder(objectFolderPath, matFolderName);
            }
        }

        //Create Meshes
        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(selectedObjectPath);
        for (int i = 0; i < objects.Length; i++) {
            EditorUtility.DisplayProgressBar(_progressTitle, selectedObject.name + " : " + objects[i].name, (float)i / (objects.Length - 1));
            if (objects[i] is Mesh) {
                Mesh mesh = Object.Instantiate(objects[i]) as Mesh;
                //AssetDatabase.CreateAsset(mesh, meshFolderPath + "/" + objects[i].name + _targetExtension);
                CreateAsset(mesh, meshFolderPath + "/" + objects[i].name + _targetExtension);
            }
            if (objects[i] is Material) {
                Material mat = Object.Instantiate(objects[i]) as Material;
                //AssetDatabase.CreateAsset(mat, matFolderPath + "/" + objects[i].name + _targetExtension);
                CreateAsset(mat, matFolderPath + "/" + objects[i].name + _targetExtension);
            }
        }

        //Cleanup
        //AssetDatabase.MoveAsset(selectedObjectPath, objectFolderPath + "/" + selectedObject.name + _sourceExtension);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void CreateAsset(Object asset, string path) {
        //if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path))) {
        //   string ext = System.IO.Path.GetExtention(path);
        //   Debug.Log(ext);
        //}
        AssetDatabase.CreateAsset(asset, path);
            
    }

}

