using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class DataFilter : ScriptableObject {
  public abstract float[,] Filter(float[,] t);
}

