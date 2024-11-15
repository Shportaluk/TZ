using System;

public class ProjectileExplotionHandler
{
    public bool IsRunning { get; private set; }
    private readonly Func<ExplotionEffect> _funcGetExplotionEffect;

    public ProjectileExplotionHandler(ProjectilePool projectilePool, Func<ExplotionEffect> funcGetExplotionEffect)
    {
        _funcGetExplotionEffect = funcGetExplotionEffect;
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
        projectile.onExplotion += OnProjectileExplotion;
    }

    private void OnProjectileExplotion(Projectile projectile)
    {
        if (IsRunning == false)
            return;

        ExplotionEffect explotionEffect = _funcGetExplotionEffect();
        explotionEffect.transform.position = projectile.transform.position;
        explotionEffect.Play();
    }
}