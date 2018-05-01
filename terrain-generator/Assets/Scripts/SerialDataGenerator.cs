using UnityEditor;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class SerialDataGenerator : MonoBehaviour {
  public Vector2Int Size = new Vector2Int(800, 600);
  public DataFilter[] DataFiltersToApply;

  public float[,] LastResult;

  public void ApplyNext() {
    if (LastResult == null) {
      LastResult = new float[Size.x, Size.y];
    }

    if (DataFiltersToApply.Length > 0) {
      LastResult = DataFiltersToApply[0].Filter(LastResult);
      DataFiltersToApply = DataFiltersToApply.Skip(1).ToArray();
    }
  }
}

[CustomEditor(typeof(SerialDataGenerator))]
public class SerialDataGeneratorEditor : Editor {
  public override void OnInspectorGUI() {
    DrawDefaultInspector();
    SerialDataGenerator castTarget = (SerialDataGenerator)target;

    GUI.enabled = castTarget.DataFiltersToApply.Length > 0 && castTarget.DataFiltersToApply[0] != null;
    if (GUILayout.Button("Apply Next Filter")) {
      castTarget.ApplyNext();
    }
    GUI.enabled = true;
  }
}