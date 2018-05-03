using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(HeightmapRenderer))]
public class MapData : MonoBehaviour {
  public Vector2Int Size = new Vector2Int(800, 600);
  private MapDataStructure[,] mapDataStructure;
 
  public MapDataStructure[,] MapDataStructure {
    get {
      if (mapDataStructure == null) {
        mapDataStructure = new MapDataStructure[Size.x, Size.y].InitDefault();
      }
      return mapDataStructure;
    }

    set {
      mapDataStructure = value;
    }
  }

  public void ApplyFilter(DataFilter ApplyFilter) {
    ApplyFilter.Filter(this);
    mapDataStructure.Map((e, i) => e.height);
  }

  public void Reset() {
    this.MapDataStructure = null;
  }
}