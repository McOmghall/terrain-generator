using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MapData))]
public abstract class DataFilter : MonoBehaviour {
  public abstract MapData Filter(MapData map);

  public void UpdateMapData() {
    GetComponent<MapData>().ApplyFilter(this);
    DestroyImmediate(this);
  }
}

[CustomEditor(typeof(DataFilter), true)]
public class SerialDataGeneratorEditor : Editor {
  public override void OnInspectorGUI() {
    DataFilter castTarget = (DataFilter)target;
    DrawDefaultInspector();
    
    if (GUILayout.Button("Apply Next Filter")) {
      castTarget.UpdateMapData();
    }
  }
}

public static class ExtensionMethodsDataFilter {
  public static bool In2DArrayBounds<T>(this T[,] array, int x, int y) {
    if (x < array.GetLowerBound(0) ||
        x > array.GetUpperBound(0) ||
        y < array.GetLowerBound(1) ||
        y > array.GetUpperBound(1)) return false;
    return true;
  }
}

