using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(SerialDataGenerator))]
public class HeightmapRenderer : MonoBehaviour {

  private void Update() {
    RenderData(GetComponent<SerialDataGenerator>().LastResult);
  }

  Mesh RenderData (float[,] Data) {
    if (Data == null) {
      return null;
    }

    Vector2Int size = new Vector2Int(Data.GetLength(0) - 1, Data.GetLength(1) - 1);
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		mesh.Clear();
    mesh.name = "Procedural Map";
    mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

    Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];
		Vector3[] normals = new Vector3[vertices.Length];
		Vector2[] uvs = new Vector2[vertices.Length];
    for (int i = 0, y = 0; y <= size.y; y++) {
			for(int x = 0; x <= size.x; x++, i++) {
        Vector2 flatPosition = new Vector2(x - size.x / 2.0f, y - size.y / 2.0f);
				vertices[i] = new Vector3(flatPosition.x, Data[x, y], flatPosition.y);
				uvs[i] = new Vector2(flatPosition.x / size.x, flatPosition.y / size.y);
				normals[i] = Vector3.up;
			}
		}

    int[] triangles = new int[size.x * size.y * 6];
    for (int ti = 0, vi = 0, y = 0; y < size.y; y++, vi++) {
      for (int x = 0; x < size.x; x++, ti += 6, vi++) {
        triangles[ti] = vi;
        triangles[ti + 3] = triangles[ti + 2] = vi + 1;
        triangles[ti + 4] = triangles[ti + 1] = vi + size.x + 1;
        triangles[ti + 5] = vi + size.x + 2;
      }
    }

    mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uvs;
    mesh.triangles = triangles;

    mesh.RecalculateBounds();

    return mesh;
	}
}

public static class ExtensionMethodsTerrainGenerator {
  public static float[,] Lerp(this float[,] a, float[,] b, float alpha) {
    var rval = (float[,])a.Clone();

    if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1)) {
      throw new UnityException("Both arrays must have the same dimensions");
    }

    for (int i = 0; i < a.GetLength(0); i++) {
      for (int j = 0; j < a.GetLength(1); j++) {
        rval[i, j] = Mathf.Lerp(a[i, j], b[i, j], alpha);
      }
    }

    return rval;
  }
}
