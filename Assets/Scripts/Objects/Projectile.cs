using System;
using UnityEngine;

public class Projectile : BasePoolElement
{
    public event Action<Projectile> onExplotion;

    [SerializeField] private float _sizeMin = 0.75f;
    [SerializeField] private float _sizeMax = 1.25f;
    [SerializeField] private float _duration = 2f;
    [SerializeField, Min(1)] private int _minTouchesToExplotiob = 2;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Material _material;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private AlternativeRigidBody _alternativeRigidBody;
    private static readonly IMeshGenerator _boxGenerator = new BoxGenerator();
    private float _currentTimeDuration = 0f;
    private int _currentTouchs = 0;


    private void Awake()
    {
        _meshRenderer.material = _material;

        var size = RandomSize(_sizeMin, _sizeMax);
        _meshFilter.mesh = _boxGenerator.Generate(size);

        _collider.size = size;

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
        _currentTimeDuration = 0f;
        _currentTouchs = 0;
        _alternativeRigidBody.ResetVelocity();
    }

    public override void SetUnUse()
    {
        base.SetUnUse();
        gameObject.SetActive(false);
    }

    private Vector3 RandomSize(float min, float max)
    {
        return Vector3.one * UnityEngine.Random.Range(min, max);
    }

    public void AddForce(float power)
    {
        _alternativeRigidBody.AddForce(transform.forward * power);
    }

    private void OnAlternativeRigidBodyCollisionEnter(AlternativeRigidBody arg1, AlternativeCollision arg2)
    {
        _currentTouchs++;
        if(_currentTouchs >= _minTouchesToExplotiob)
        {
            onExplotion?.Invoke(this);
            SetUnUse();
        }
    }
}