using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WalkerGeneratorFilter : DataFilter {
  public int initialPositions = 1;
  public int spawnWalkers = 10;
  public int numberOfStepsPerWalker = 1000000;

  private static readonly System.Random random = new System.Random();

  public override MapData Filter(MapData t) {
    var initialPositionArray = new Vector2Int[initialPositions];
    for (int i = 0; i < initialPositions; i++) {
      initialPositionArray[i] = new Vector2Int(random.Next(t.MapDataStructure.GetLength(0) + 1), random.Next(t.MapDataStructure.GetLength(1) + 1));
    }

    for (int i = 0, initialPositionPointer = 0; i < spawnWalkers; i++, initialPositionPointer++) {
      Vector2Int currentPosition = initialPositionArray[initialPositionPointer % initialPositionArray.Length];
      for (int j = 0; j < numberOfStepsPerWalker; j++) {
        if (t.MapDataStructure.In2DArrayBounds(currentPosition.x, currentPosition.y)) {
          t.MapDataStructure[currentPosition.x, currentPosition.y].height += 1;
        }
        currentPosition = currentPosition + new Vector2Int(random.Next(-1, 2), random.Next(-1, 2));
      }
    }

    return t;
  }
}
