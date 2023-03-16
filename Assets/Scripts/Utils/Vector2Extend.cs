
using UnityEngine;
using System;

public static class Vector2Extend { 

    public static Vector2 GreaterThan(this Vector2 vec, float val) {
        return new Vector2(
            Convert.ToSingle(vec.x > val), 
            Convert.ToSingle(vec.y > val));
    }

    public static Vector2 Add(this Vector2 vec, float val) {
        return new Vector2(vec[0] + val, vec[1] + val);
    }

    public static float Sum(this Vector2 vec) {
        return vec[0] + vec[1];
    }
    
    public static Vector2 Invert(this Vector2 vec) {
        return new Vector2(1f / vec.x, 1f / vec.y);
    }

    public static Vector2 GreaterThan(this Vector2 vec, int val) {
        return new Vector3(
            Convert.ToSingle(vec.x > val), 
            Convert.ToSingle(vec.y > val));
    }

    public static float Maximum(this Vector2 vec) {
        if (vec.x > vec.y) {
            return vec.x;
        } else {
            return vec.y;
        }
    }

    public static float Minimum(this Vector2 vec) {
        if (vec.x < vec.y) {
            return vec.x;
        } else {
            return vec.y;
        }
    }

}