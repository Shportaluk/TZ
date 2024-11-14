using UnityEngine;

public class Projectile : BasePoolElement
{
    [SerializeField] private float _sizeMin = 0.75f;
    [SerializeField] private float _sizeMax = 1.25f;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Material _material;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private AlternativeRigidBody _alternativeRigidBody;
    private static readonly IMeshGenerator _boxGenerator = new BoxGenerator();
    private float _currentTimeDuration = 0f;

    private void Awake()
    {
        _meshRenderer.material = _material;

        var size = RandomSize(_sizeMin, _sizeMax);
        _meshFilter.mesh = _boxGenerator.Generate(size);

        _collider.size = size;
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
        _alternativeRigidBody.ResetVelocity();
    }

    public override void SetUnUse()
    {
        base.SetUnUse();
        gameObject.SetActive(false);
    }

    private Vector3 RandomSize(float min, float max)
    {
        return Vector3.one * Random.Range(min, max);
    }

    public void AddForce(float power)
    {
        _alternativeRigidBody.AddForce(transform.forward * power);
    }
}