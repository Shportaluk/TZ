using UnityEngine;

public class BoxMeshGenerator : IMeshGenerator
{
    private static readonly Vector3[] _vertices = {
            // Front face
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f), 

            // Back face
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
        };

    private static readonly int[] _triangles = {
        // Front face
        0, 1, 2,
        0, 2, 3,

        // Top face
        3, 2, 7,
        7, 2, 6,

        // Back face
        7, 6, 5,
        7, 5, 4,

        // Bottom face
        4, 5, 1,
        4, 1, 0,

        // Left face
        4, 0, 3,
        4, 3, 7,

        // Right face
        1, 5, 6,
        1, 6, 2
        };

    private static readonly Vector3[] _normals = {
        Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
        Vector3.back, Vector3.back, Vector3.back, Vector3.back
        };

    private static readonly Vector2[] uv = {
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
        };



    public Mesh Generate(Vector3 scale = default)
    {
        Vector3[] vertices;
        if (scale == default)
        {
            vertices = _vertices;
        }
        else
        {
            vertices = new Vector3[_vertices.Length];
            for (int i = 0; i < _vertices.Length; i++)
            {
                vertices[i] = Vector3.Scale(_vertices[i], scale);
            }
        }

        return Generate(vertices);
    }

    public Mesh Generate(Vector3[] vertices)
    {
        Mesh mesh = new Mesh();
        mesh.name = "BoxMesh";
        mesh.vertices = vertices;
        mesh.triangles = _triangles;
        mesh.normals = _normals;
        mesh.uv = uv;

        return mesh;
    }

    public Vector3[] GetVertices()
    {
        return _vertices;
    }
}