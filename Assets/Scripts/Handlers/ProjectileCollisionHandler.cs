using System;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    public bool IsRunning { get; private set; }

    public ProjectileCollisionHandler(ProjectilePool projectilePool)
    {
        projectilePool.onInstantiated += OnInstantiatedProjectile;
    }


    public void Run()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    private void OnInstantiatedProjectile(Projectile projectile)
    {
        projectile.onCollision += OnProjectileCollision;
    }

    private void OnProjectileCollision(Projectile projectile, AlternativeCollision collision)
    {
        if (IsRunning == false)
            return;


        var source = projectile.transform.position - (projectile.PreviewVelocity * 0.2f);
        var direction = projectile.PreviewVelocity;

        Ray ray = new Ray(source, direction);

        foreach (var hit in Physics.RaycastAll(ray))
        {
            if (hit.collider is MeshCollider && hit.collider.TryGetComponent(out MeshPainting painter))
            {
                painter.Paint(hit.textureCoord);
                break;
            }
        }

        Debug.DrawLine(source, direction, Color.red, 1f);
    }
}