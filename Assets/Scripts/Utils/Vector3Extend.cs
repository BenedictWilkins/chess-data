
using UnityEngine;
using System;

public static class Vector3Extend { 

    public static Vector3 GreaterThan(this Vector3 vec, float val) {
        return new Vector3(
            Convert.ToSingle(vec.x > val), 
            Convert.ToSingle(vec.y > val), 
            Convert.ToSingle(vec.z > val));
    }


    public static float Sum(this Vector3 vec) {
        return vec[0] + vec[1] + vec[2];
    }
    
    public static Vector3 Invert(this Vector3 vec) {
        return new Vector3(1f / vec.x, 1f / vec.y, 1f / vec.z);
    }

    public static Vector3 GreaterThan(this Vector3 vec, int val) {
        return new Vector3(
            Convert.ToSingle(vec.x > val), 
            Convert.ToSingle(vec.y > val), 
            Convert.ToSingle(vec.z > val));
    }

    public static float Maximum(this Vector3 vec) {
        if (vec.x > vec.y && vec.x > vec.z) {
            return vec.x;
        } else if(vec.y > vec.z) {
            return vec.y;
        } else {
            return vec.z;
        }
    }

    public static float Minimum(this Vector3 vec) {
        if (vec.x < vec.y && vec.x < vec.z) {
            return vec.x;
        } else if(vec.y < vec.z) {
            return vec.y;
        } else {
            return vec.z;
        }
    }

}