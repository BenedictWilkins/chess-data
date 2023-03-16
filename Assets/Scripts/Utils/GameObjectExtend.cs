using UnityEngine;

public static class GameObjectExtend {

    public static Vector3 GetActualSize(this GameObject go) {
        Vector3 size = Vector3.zero;
        foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>()) {
            Vector3 bsize = renderer.bounds.size; 
            size = Vector3.Max(size, bsize);
        }
        return size;
    }

}