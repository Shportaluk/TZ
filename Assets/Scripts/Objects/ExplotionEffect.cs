using UnityEngine;

public class ExplotionEffect : BasePoolElement
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _duration;
    private float _currentDuration;


    public void Play()
    {
        _particleSystem.Play();
    }

    public void Stop()
    {
        _particleSystem.Stop();
    }

    private void Update()
    {
        _currentDuration += Time.deltaTime;

        if(_currentDuration >= _duration)
        {
            SetUnUse();
        }
    }

    public override void SetUse()
    {
        base.SetUse();
        gameObject.SetActive(true);
        _currentDuration = 0f;
    }

    public override void SetUnUse()
    {
        base.SetUnUse();
        gameObject.SetActive(false);
        Stop();
    }
}