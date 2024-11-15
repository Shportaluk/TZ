using UnityEngine;

public interface IMeshGenerator
{
    Mesh Generate(Vector3 scale = default);
    Mesh Generate(Vector3[] vertices);
    Vector3[] GetVertices();
}