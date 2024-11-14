using System;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private float _power = 10000;
    [SerializeField] private Transform _tranfromRotationX;
    [SerializeField] private Transform _tranfromRotationY;
    [SerializeField] private float _rotationMinX = -45;
    [SerializeField] private float _rotationMaxX = 45;
    [SerializeField] private float _rotationMinY = -10;
    [SerializeField] private float _rotationMaxY = 60;
    private Func<Projectile> _funcGetProjectile;
    private Vector2 _currentRotation = Vector3.zero;


    public void Init(Func<Projectile> funcGetProjectile)
    {
        _funcGetProjectile = funcGetProjectile;
    }

    public void Fire()
    {
        var projectile = _funcGetProjectile();
        projectile.AddForce(_power);
    }

    public void SetPower(float power)
    {
        _power = power;
    }


    public void Rotate(Vector2 dir)
    {
        _currentRotation += dir;
        // x Up/DOwn
        // y Left Right
        float x = Mathf.Clamp(_currentRotation.x, _rotationMinX, _rotationMaxX);
        float y = Mathf.Clamp(_currentRotation.y, _rotationMinY, _rotationMaxY);
        _currentRotation = new Vector2(x, y);

        _tranfromRotationX.localEulerAngles = new Vector3(x, 0, 0);
        _tranfromRotationY.localEulerAngles = new Vector3(0, y, 0);
    }
}