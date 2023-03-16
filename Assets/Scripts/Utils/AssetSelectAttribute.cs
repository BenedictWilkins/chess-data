using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class AssetSelectAttribute : PropertyAttribute {
    public string AssetsPath { get; set; }
    
    public AssetSelectAttribute(string path) {
        AssetsPath = path;
    }
}
