#define DEBUG
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AlternativeRigidBody : MonoBehaviour, ICollisionHandler
{
    public Collider Collider { get; private set; }

    [SerializeField] private BodyType _bodyType = BodyType.Dynamic;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _bounciness = 0.8f;
    [SerializeField] private bool _useGravity = true;

    private readonly static Vector3 _gravity = new Vector3(0, -9.81f, 0);

    private Vector3 _accumulatedForce = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;


    private void Awake()
    {
        Collider = this.GetComponent<Collider>();
        AlternativeCollisionDetection.Add(this);

        if (_bodyType == BodyType.Dynamic)
        {
            AddForce(transform.forward * 10000);
        }
    }

    private void FixedUpdate()
    {
        if (_bodyType == BodyType.Dynamic)
        {
            ApplyPhysics(Time.fixedDeltaTime);
        }
    }

    public void AddForce(Vector3 force)
    {
        _accumulatedForce += force / _mass;
    }

    private void ApplyPhysics(float deltaTime)
    {
        if (_useGravity)
        {
            _accumulatedForce += _gravity;
        }

        _velocity += _accumulatedForce * deltaTime;
        transform.position += _velocity * deltaTime;
        _accumulatedForce = Vector3.zero;
    }

    public void OnAlternativeCollisionEnter(AlternativeCollision collision)
    {
#if DEBUG
        Debug.Log(collision.collider.gameObject.name);
        Debug.DrawRay(Collider.bounds.center, collision.contactNormal, Color.red, 0.5f);
        Debug.DrawRay(Collider.bounds.center, _velocity, Color.cyan, 0.5f);
#endif

        _velocity = Vector3.Reflect(_velocity, collision.contactNormal) * _bounciness;
    }


    public enum BodyType
    {
        Static = 0,
        Dynamic = 1
    }
}