using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public event Action<float> onChangedPower;
    public event Action<float> onReloadingProgress;

    public bool IsReloading { get; private set; } = false;
    public float Power => _power;

    [SerializeField] private float _power = 10000;
    [SerializeField] private Transform _tranfromRotationX;
    [SerializeField] private Transform _tranfromRotationY;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rotationMinX = -45;
    [SerializeField] private float _rotationMaxX = 45;
    [SerializeField] private float _rotationMinY = -45;
    [SerializeField] private float _rotationMaxY = 45;
    [SerializeField] private float _reloadTime = 0.5f;

    private Func<Projectile> _funcGetProjectile;
    private Vector2 _currentRotation = Vector3.zero;
    private Coroutine _reloadingCor;


    public void Init(Func<Projectile> funcGetProjectile)
    {
        _funcGetProjectile = funcGetProjectile;
    }

    public void Fire()
    {
        if (IsReloading)
            return;

        var projectile = _funcGetProjectile();
        projectile.transform.position = _spawnPoint.position;
        projectile.transform.rotation = _spawnPoint.rotation;
        projectile.AddForce(_power);

        StartReloading();
    }

    private void StartReloading()
    {
        IsReloading = true;

        _reloadingCor = StartCoroutine(Cor());

        IEnumerator Cor()
        {
            float time = 0;

            while (time < _reloadTime)
            {
                float t = time / _reloadTime;
                onReloadingProgress?.Invoke(t);
                time += Time.deltaTime;
                yield return null;
            }
            onReloadingProgress?.Invoke(1f);

            IsReloading = false;
        }
    }

    public void SetPower(float power)
    {
        _power = power;
        onChangedPower?.Invoke(power);
    }


    public void Rotate(Vector2 dir)
    {
        _currentRotation += dir;

        float x = Mathf.Clamp(_currentRotation.x, _rotationMinX, _rotationMaxX);
        float y = Mathf.Clamp(_currentRotation.y, _rotationMinY, _rotationMaxY);
        _currentRotation = new Vector2(x, y);

        _tranfromRotationX.localEulerAngles = new Vector3(x, 0, 0);
        _tranfromRotationY.localEulerAngles = new Vector3(0, y, 0);
    }
}