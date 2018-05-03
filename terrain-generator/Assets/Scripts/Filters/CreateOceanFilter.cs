using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class CreateOceanFilter : DataFilter {
  public float displacement = 1;

  public override MapData Filter(MapData t) {
    var min = Mathf.Max(t.MapDataStructure.Map((e, i) => e.height).Cast<float>().Min(), 0);
    var movement = displacement + min;

    t.MapDataStructure.Map((e, i) => {
      var rval = e;
      rval.height -= movement;
      return rval;
    });

    return t;
  }
}
