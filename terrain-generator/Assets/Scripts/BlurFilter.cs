using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlurFilter : DataFilter {
  public int distance = 10;

  [MenuItem("Assets/Create/DataFilter/BlurFilter")]
  public static void CreateAsset() {
    ScriptableObjectUtility.CreateAsset<BlurFilter>();
  }

  public override float[,] Filter(float[,] t) {
    return t.WeightedSurroundings(distance, (currentDistance, fixedDistance) => (currentDistance <= fixedDistance ? 1.0f : 0.0f));
  }
}

public static class ExtensionMethodsBlurFilter {
  public static float[,] WeightedSurroundings(this float[,] a, int distance, Func<int, int, float> distanceWeight) {
    var rval = (float[,])a.Clone();
    var linearSurroundings = Enumerable.Range(-distance, distance).ToArray();
    var surroundings =
      from fst in linearSurroundings
      from snd in linearSurroundings
      select new[] { fst, snd }.ToArray();

    for (int i = 0; i < a.GetLength(0); i++) {
      for (int j = 0; j < a.GetLength(1); j++) {
        var m = surroundings.Select(element => {
          try {
            return new float[] { a[i + element[0], j + element[1]], distanceWeight(Mathf.Abs(element[0]) + Mathf.Abs(element[1]), distance) };
          } catch (IndexOutOfRangeException) {
            return new float[] { 0, 0};
          }
        }).ToArray();
        var e = m.Aggregate(0.0f, (acc, element) => acc + element[0] * element[1]);
        var d = m.Aggregate(0.0f, (acc, element) => acc + element[1]);
        rval[i, j] = e / (d == 0 ? 1 : d);
      }
    }

    return rval;
  }
}
