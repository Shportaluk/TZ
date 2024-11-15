//#define DEBUG_MOD
using System;
using UnityEngine;


public class AlternativeRigidBody : MonoBehaviour, ICollisionHandler
{
    public event Action<AlternativeRigidBody, AlternativeCollision> onCollisionEnter;

    public Collider Collider => _collider;
    public BodyType Type => _bodyType;
    public float Mass => _mass;

    [SerializeField] private BodyType _bodyType = BodyType.Dynamic;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _bounciness = 0.8f;
    [SerializeField] private bool _useGravity = true;

    private Data _data = new Data();


    private void Awake()
    {
        _data.useGravity = _useGravity;
        AlternativeCollisionDetection.Add(this);
    }

    private void FixedUpdate()
    {
        if (_bodyType == BodyType.Dynamic)
        {
            ApplyPhysics();
        }
    }

    public void AddForce(Vector3 force)
    {
        _data.accumulatedForce += force / _mass;
    }

    private void ApplyPhysics()
    {
        _data.position = transform.position;
        _data = Simulate(_data);
        transform.position = _data.position;
    }

    public static Data Simulate(Data data)
    {
        if (data.useGravity)
        {
            data.accumulatedForce += Physics.gravity;
        }

        data.velocity += data.accumulatedForce * Time.fixedDeltaTime;
        data.position += data.velocity * Time.fixedDeltaTime;
        data.accumulatedForce = Vector3.zero;

        return data;
    }

    public void ResetVelocity()
    {
        _data.accumulatedForce = Vector3.zero;
        _data.velocity = Vector3.zero;
    }

    public void OnAlternativeCollisionEnter(AlternativeCollision collision)
    {
#if DEBUG_MOD
        Debug.Log(collision.collider.gameObject.name);
        Debug.DrawRay(Collider.bounds.center, collision.contactNormal, Color.red, 0.5f);
        Debug.DrawRay(Collider.bounds.center, _velocity, Color.cyan, 0.5f);
#endif

        _data.velocity = Vector3.Reflect(_data.velocity, collision.contactNormal) * _bounciness;
        onCollisionEnter?.Invoke(this, collision);
    }

    public enum BodyType
    {
        Static = 0,
        Dynamic = 1
    }

    public struct Data
    {
        public bool useGravity;
        public Vector3 accumulatedForce;
        public Vector3 velocity;
        public Vector3 position;
    }
}