using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    [SerializeField] private Material _sharedMaterial = null;
    private MeshCollider _meshCollider;
    private MeshRenderer _meshRenderer;
    private Mesh _hexMesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;
    private List<Color> _colors;

    public void Triangulate(ICell[] cells)
    {
        _hexMesh.Clear();
        _vertices.Clear();
        _triangles.Clear();
        _colors.Clear();
        foreach (var item in cells)
        {
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                Triangulate(d, item);
            }
        }
        _hexMesh.vertices = _vertices.ToArray();
        _hexMesh.triangles = _triangles.ToArray();
        _hexMesh.colors = _colors.ToArray();
        _hexMesh.RecalculateNormals();
        _meshRenderer.material = _sharedMaterial;
        _meshCollider.sharedMesh = _hexMesh;
    }

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = _hexMesh = new Mesh();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = gameObject.AddComponent<MeshCollider>();
        _hexMesh.name = "Hex Mesh";
        _vertices = new List<Vector3>();
        _colors = new List<Color>();
        _triangles = new List<int>();
    }

    private void Triangulate(HexDirection direction, ICell cell)
    {
        Vector3 center = cell.LocalPosition;
        float z = center.z;
        center.z = center.y;
        center.y = z;
        AddTriangle(
            center,
            center + HexMetrices.GetFirstCorner(direction),
            center + HexMetrices.GetSecondCorner(direction)
        );
        AddTriangleColor(cell.CellColor);
    }

    private void AddTriangleColor(Color solid)
    {
        AddTriangleColor(solid, solid, solid);
    }

    private void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        _colors.Add(c1);
        _colors.Add(c2);
        _colors.Add(c3);
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = _vertices.Count;
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);
        _triangles.Add(vertexIndex);
        _triangles.Add(vertexIndex + 1);
        _triangles.Add(vertexIndex + 2);
    }
}