using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WalkerGeneratorFilter : DataFilter {
  public int initialPositions = 1;
  public int spawnWalkers = 10;
  public int numberOfStepsPerWalker = 1000000;

  private static readonly System.Random random = new System.Random();

  [MenuItem("Assets/Create/DataFilter/WalkerGeneratorFilter")]
  public static void CreateAsset() {
    ScriptableObjectUtility.CreateAsset<WalkerGeneratorFilter>();
  }

  public override float[,] Filter(float[,] t) {
    var initialPositionArray = new Vector2Int[initialPositions];
    for (int i = 0; i < initialPositions; i++) {
      initialPositionArray[i] = new Vector2Int(random.Next(t.GetLength(0) + 1), random.Next(t.GetLength(1) + 1));
    }

    float[,] rval = (float[,])t.Clone();
    for (int i = 0, initialPositionPointer = 0; i < spawnWalkers; i++, initialPositionPointer++) {
      Vector2Int currentPosition = initialPositionArray[initialPositionPointer % initialPositionArray.Length];
      for (int j = 0; j < numberOfStepsPerWalker; j++) {
        if (rval.In2DArrayBounds(currentPosition.x, currentPosition.y)) {
          rval[currentPosition.x, currentPosition.y] = rval[currentPosition.x, currentPosition.y] + 1;
        }
        currentPosition = currentPosition + new Vector2Int(random.Next(-1, 2), random.Next(-1, 2));
      }
    }

    return rval;
  }
}

public static class ExtensionMethodsWalkerGeneratorFilter {
  public static bool In2DArrayBounds(this float[,] array, int x, int y) {
    if (x < array.GetLowerBound(0) ||
        x > array.GetUpperBound(0) ||
        y < array.GetLowerBound(1) ||
        y > array.GetUpperBound(1)) return false;
    return true;
  }
}
