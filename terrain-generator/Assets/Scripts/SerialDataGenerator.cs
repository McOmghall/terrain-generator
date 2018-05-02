using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

[ExecuteInEditMode]
public class SerialDataGenerator : MonoBehaviour {
  public Vector2Int Size = new Vector2Int(800, 600);
  public DataFilter ApplyFilter;

  public float[,] LastResult;

  public void ApplyNext() {
    if (LastResult == null) {
      Restart();
    }

    if (ApplyFilter != null) {
      LastResult = ApplyFilter.Filter(LastResult);
      ApplyFilter = null;
    }
  }

  internal void Restart() {
    LastResult = new float[Size.x, Size.y];
  }
}

[CustomEditor(typeof(SerialDataGenerator))]
public class SerialDataGeneratorEditor : Editor {
  public override void OnInspectorGUI() {
    SerialDataGenerator castTarget = (SerialDataGenerator)target;
    if (GUILayout.Button("Restart")) {
      castTarget.Restart();
    }

    DrawDefaultInspector();

    GUI.enabled = castTarget.ApplyFilter != null;
    if (GUILayout.Button("Apply Next Filter")) {
      castTarget.ApplyNext();
    }
    GUI.enabled = true;
  }
}