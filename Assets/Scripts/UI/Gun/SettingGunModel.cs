using System;
using UnityEngine;

public class SettingGunModel
{
    public event Action<float> onChangedPower;
    public event Action<float> onChangedSpeed;

    public float MinPower { get; }
    public float MaxPower { get; }
    public float Power => _gun.Power;

    public float MinSpeed => 0.01f;
    public float MaxSpeed => 3f;
    public float Speed { get; private set; } = 1;

    private readonly Gun _gun;


    public SettingGunModel(Gun gun, float minPower, float maxPower)
    {
        _gun = gun;
        _gun.onChangedPower += OnChangedPower;
        MinPower = minPower;
        MaxPower = maxPower;
    }

    private void OnChangedPower(float power)
    {
        onChangedPower?.Invoke(power);
    }

    public void SetPower(float power)
    {
        _gun.SetPower(power);
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;

        Debug.Log(speed);
        Time.timeScale = speed;

        onChangedSpeed?.Invoke(speed);
    }
}