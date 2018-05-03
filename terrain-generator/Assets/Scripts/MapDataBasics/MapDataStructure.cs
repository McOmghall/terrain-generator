using System;
using UnityEngine;

public class MapDataStructure {
  public float height = 0;
  public float averageTemperature = 0;
  public float habitability = 0;
}

public static class ExtensionMethodsMapDataStructure {
  public static T[,] Map<A, T>(this A[,] array, Func<A, Vector2Int, T> f) {
    var rval = new T[array.GetLength(0), array.GetLength(1)];

    for (int i = 0; i < rval.GetLength(0); i++) {
      for (int j = 0; j < rval.GetLength(1); j++) {
        rval[i, j] = f(array[i, j], new Vector2Int(i, j));
      }
    }

    return rval;
  }

  public static MapDataStructure[,] InitDefault (this MapDataStructure[,] array) {
    return array.Map((e, i) => new MapDataStructure());
  }
}