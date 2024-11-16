using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingGunUIView : MonoBehaviour
{
    [SerializeField] private Slider _sliderPower;
    [SerializeField] private TextMeshProUGUI _txtPower;

    [SerializeField] private Slider _sliderSpeed;
    [SerializeField] private TextMeshProUGUI _txtSpeed;

    private SettingGunModel _model;


    private void Awake()
    {
        _sliderPower.onValueChanged.AddListener(OnChangedSliderValue);
        _sliderPower.minValue = 0.01f;

        _sliderSpeed.onValueChanged.AddListener(OnChangedSpeedValue);
        _sliderSpeed.minValue = 0.01f;
    }

    public void SetModel(SettingGunModel model)
    {
        if(_model != null)
        {
            _model.onChangedPower -= OnChangedPower;
            _model.onChangedSpeed -= OnChangedSpeed;
        }

        _model = model;
        model.onChangedPower += OnChangedPower;
        model.onChangedSpeed += OnChangedSpeed;
    }

    private void OnChangedSliderValue(float value)
    {
        float power = ConverterValue.Map(value, 0.01f, 1, _model.MinPower, _model.MaxPower);
        _model.SetPower(power);
    }

    private void OnChangedSpeedValue(float value)
    {
        float speed = ConverterValue.Map(value, 0.01f, 1, _model.MinSpeed, _model.MaxSpeed);
        _model.SetSpeed(speed);
    }

    private void OnChangedPower(float power)
    {
        UpdateViewPower();
    }

    private void OnChangedSpeed(float speed)
    {
        UpdateViewSpeed();
    }

    public void UpdateView()
    {
        UpdateViewPower();
        UpdateViewSpeed();
    }

    private void UpdateViewPower()
    {
        double percent = Math.Round(ConverterValue.Map(_model.Power, _model.MinPower, _model.MaxPower, 1f, 100f), 0);
        _sliderPower.value = (float)percent / 100f;
        _txtPower.text = percent.ToString();
    }

    private void UpdateViewSpeed()
    {
        double percent = Math.Round(ConverterValue.Map(_model.Speed, _model.MinSpeed, _model.MaxSpeed, 1f, 100f), 0);
        _sliderSpeed.value = (float)percent / 100f;
        _txtSpeed.text = percent.ToString();
    }
}