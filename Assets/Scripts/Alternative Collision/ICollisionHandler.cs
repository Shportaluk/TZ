using UnityEngine;
using static AlternativeRigidBody;

public interface ICollisionHandler
{
    Collider Collider { get; }
    BodyType Type { get; }

    void OnAlternativeCollisionEnter(AlternativeCollision collision);
}