using UnityEngine;

public class RandomizeVerticlesMeshDecorator : IMeshGenerator
{
    private readonly IMeshGenerator _meshGenerator;
    private readonly float _randomSize;

    public RandomizeVerticlesMeshDecorator(IMeshGenerator meshGenerator, float randomSize)
    {
        _meshGenerator = meshGenerator;
        _randomSize = randomSize;
    }

    public Mesh Generate(Vector3 scale = default)
    {
        return _meshGenerator.Generate(GetVertices());
    }

    public Mesh Generate(Vector3[] vertices)
    {
        return _meshGenerator.Generate(vertices);
    }

    public Vector3[] GetVertices()
    {
        var verticles = _meshGenerator.GetVertices();
        float halfSize = _randomSize / 2f;

        for (int i = 0; i < verticles.Length; i++)
        {
            verticles[i] += RandomVector(-halfSize, halfSize);
        }

        return verticles;
    }

    private Vector3 RandomVector(float min, float max)
    {
        return new Vector3(
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max));
    }
}