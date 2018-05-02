using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PerlinNoiseAdderFilter : DataFilter {
  public float verticalScale = 10;
  public float horizontalScale = 10f;
  public float verticalOffset = -0.5f;

  public override float[,] Filter(float[,] t) {
    var rval = (float[,])t.Clone();
    var perlinOffset = new Vector2(Random.value, Random.value);

    for (int i = 0; i < rval.GetLength(0); i++) {
      for (int j = 0; j < rval.GetLength(1); j++) {
        rval[i, j] += (Mathf.PerlinNoise((float)(i * horizontalScale) / rval.GetLength(0) + perlinOffset.x, (float)(j * horizontalScale) / rval.GetLength(1) + perlinOffset.y) + verticalOffset) * verticalScale;
      }
    }

    return rval;
  }

  [MenuItem("Assets/Create/DataFilter/PerlinNoiseAdderFilter")]
  public static void CreateAsset() {
    ScriptableObjectUtility.CreateAsset<PerlinNoiseAdderFilter>();
  }
}
