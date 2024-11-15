using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingGunUIView : MonoBehaviour
{
    [SerializeField] private Slider _sliderPower;
    [SerializeField] private TextMeshProUGUI _txtPower;

    private SettingGunModel _model;

    private void Awake()
    {
        _sliderPower.onValueChanged.AddListener(OnChangedSliderValue);
        _sliderPower.minValue = 0.01f;
    }

    public void SetModel(SettingGunModel model)
    {
        if(_model != null)
        {
            _model.onChangedPower -= OnChangedPower;
        }

        _model = model;
        model.onChangedPower += OnChangedPower;
    }

    private void OnChangedSliderValue(float value)
    {
        float power = ConverterValue.Map(value, 0.01f, 1, _model.Min, _model.Max);
        _model.SetPower(power);
    }

    private void OnChangedPower(float power)
    {
        UpdateView();
    }

    public void UpdateView()
    {
        double percent = Math.Round(ConverterValue.Map(_model.Power, _model.Min, _model.Max, 1f, 100f), 0);
        _sliderPower.value = (float)percent / 100f;
        _txtPower.text = percent.ToString();
    }
}