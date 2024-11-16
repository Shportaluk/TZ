using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BasePoolElement
{
    public event Action<Projectile, AlternativeCollision> onCollision;
    public event Action<Projectile> onExplotion;

    public Vector3 Velocity => _alternativeRigidBody.Velocity;
    public Vector3 PreviewVelocity => _alternativeRigidBody.PreviewVelocity;
    [SerializeField] private float _sizeMin = 0.75f;
    [SerializeField] private float _sizeMax = 1.25f;
    [SerializeField] private float _duration = 2f;
    [SerializeField, Min(1)] private int _minCollisionToExplotion = 2;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Material _material;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private AlternativeRigidBody _alternativeRigidBody;

    private const float RANDOM_MESH_VERTICES_SIZE = 0.2f;

    private static readonly IMeshGenerator _meshGenerator =
        new RandomizeVerticlesMeshDecorator(
            new BoxMeshGenerator(),
            RANDOM_MESH_VERTICES_SIZE);

    private float _currentTimeDuration = 0f;
    private int _currentCollision = 0;

    private void Awake()
    {
        _meshRenderer.material = _material;

        _collider.size = Vector3.one;

        _alternativeRigidBody.onCollisionEnter += OnAlternativeRigidBodyCollisionEnter;
    }

    private void Update()
    {
        _currentTimeDuration += Time.deltaTime;

        if(_currentTimeDuration > _duration)
        {
            SetUnUse();
        }
    }

    public override void SetUse()
    {
        base.SetUse();
        gameObject.SetActive(true);
        _meshFilter.mesh = _meshGenerator.Generate(Vector3.one);
        _currentTimeDuration = 0f;
        _currentCollision = 0;
        _alternativeRigidBody.ResetVelocity();
    }

    public override void SetUnUse()
    {
        base.SetUnUse();
        gameObject.SetActive(false);
    }

    public void AddForce(float power)
    {
        _alternativeRigidBody.AddForce(transform.forward * power);
    }

    private void OnAlternativeRigidBodyCollisionEnter(AlternativeRigidBody arg1, AlternativeCollision collision)
    {
        _currentCollision++;
        onCollision?.Invoke(this, collision);
        Debug.Log("OnAlternativeRigidBodyCollisionEnter: " + arg1.gameObject.name + " with: " + collision.collider.gameObject.name);
        if (_currentCollision >= _minCollisionToExplotion)
        {
            onExplotion?.Invoke(this);
            SetUnUse();
        }
    }

    public static IEnumerable<Vector3> GeneratePath(Vector3 startPosition, Vector3 force, float mass, float simulationTime)
    {
        float elapsedTime = 0f;
        var data = new AlternativeRigidBody.Data();
        data.position = startPosition;
        data.useGravity = true;
        data.accumulatedForce += force / mass;

        yield return startPosition;
        while (elapsedTime < simulationTime)
        {
            data = AlternativeRigidBody.Simulate(data);
            yield return data.position;
            elapsedTime += Time.fixedDeltaTime;
        }
    }
}