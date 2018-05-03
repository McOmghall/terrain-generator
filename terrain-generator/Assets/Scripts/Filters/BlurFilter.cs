using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlurFilter : DataFilter {
  public int distance = 10;

  public override MapData Filter(MapData t) {
    var blurredMap = t.MapDataStructure
      .Map((e, i) => e.height)
      .WeightedSurroundings(
        distance,
        element => element,
        eDistance => eDistance <= distance ? 1.0f : 0.0f,
        arrayToReduce => {
          float e = arrayToReduce.Aggregate(0.0f, (acc, element) => acc + element.Key * element.Value);
          float d = arrayToReduce.Aggregate(0.0f, (acc, element) => acc + element.Value);
          return e / (d == 0 ? 1 : d);
        });
    t.MapDataStructure = t.MapDataStructure.Map((e, i) => {
      e.height = blurredMap[i.x, i.y];
      return e;
    });
    return t;
  }
}

public static class ExtensionMethodsBlurFilter {
  public static T[,] WeightedSurroundings<A, B, C, T>(this A[,] a, int distance, Func<A, B> fNumerator, Func<int, C> fDenominator, Func<IEnumerable<KeyValuePair<B, C>>, T> j) {
    var linearSurroundings = Enumerable.Range(-distance, distance).ToArray();
    var surroundings =
      from fst in linearSurroundings
      from snd in linearSurroundings
      select new[] { fst, snd }.ToArray();

    return a.Map((e, i) => {
      return j(surroundings.Select(element => {
        try {
          var keyNumerator = a[i.x + element[0], i.y + element[1]];
          var valueDenominator = Mathf.Abs(element[0]) + Mathf.Abs(element[1]);
          return new KeyValuePair<B, C>(fNumerator(keyNumerator), fDenominator(valueDenominator));
        } catch (IndexOutOfRangeException) {
          return new KeyValuePair<B, C>();
        }
      }));
    });
  }
}
