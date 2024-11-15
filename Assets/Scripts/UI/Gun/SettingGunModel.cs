using System;

public class SettingGunModel
{
    public event Action<float> onChangedPower;

    public float Min { get; }
    public float Max { get; }
    public float Power => _gun.Power;

    private Gun _gun;


    public SettingGunModel(Gun gun, float min, float max)
    {
        _gun = gun;
        _gun.onChangedPower += OnChangedPower;
        Min = min;
        Max = max;
    }

    private void OnChangedPower(float power)
    {
        onChangedPower?.Invoke(power);
    }

    public void SetPower(float power)
    {
        _gun.SetPower(power);
    }
}