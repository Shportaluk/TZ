using UnityEngine;

public interface ICollisionHandler
{
    Collider Collider { get; }

    void OnAlternativeCollisionEnter(AlternativeCollision collision);
}