using UnityEngine;

public interface IMeshGenerator
{
    Mesh Generate(Vector3 scale = default);
}