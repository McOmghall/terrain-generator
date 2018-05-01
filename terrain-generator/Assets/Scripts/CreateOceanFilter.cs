using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class CreateOceanFilter : DataFilter {
  public float displacement = 1;

  [MenuItem("Assets/Create/DataFilter/CreateOceanFilter")]
  public static void CreateAsset() {
    ScriptableObjectUtility.CreateAsset<CreateOceanFilter>();
  }

  public override float[,] Filter(float[,] t) {
    var rval = (float[,])t.Clone();
    var min = Mathf.Max(rval.Cast<float>().Min(), 0);
    var movement = displacement + min;

    for (int i = 0; i < rval.GetLength(0); i++) {
      for (int j = 0; j < rval.GetLength(1); j++) {
        rval[i, j] = rval[i, j] - movement;
      }
    }

    return rval;
  }
}
