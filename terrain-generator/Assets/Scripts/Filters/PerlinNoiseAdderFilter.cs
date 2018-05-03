using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class PerlinNoiseAdderFilter : DataFilter {
  public float verticalScale = 10;
  public float horizontalScale = 10f;
  public float verticalOffset = -0.5f;

  public override MapData Filter(MapData t) {
    var perlinOffset = new Vector2(Random.value, Random.value);

    t.MapDataStructure.Map((e, i) => {
      MapDataStructure rval = e;
      var noiseValue = Mathf.PerlinNoise((float)(i.x * horizontalScale) / t.MapDataStructure.GetLength(0) + perlinOffset.x, (float)(i.y * horizontalScale) / t.MapDataStructure.GetLength(1) + perlinOffset.y);
      rval.height = e.height + (noiseValue + verticalOffset) * verticalScale;
      return rval;
    });

    return t;
  }
}
