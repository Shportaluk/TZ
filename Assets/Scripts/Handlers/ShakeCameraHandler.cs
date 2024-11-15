using System;

public class ShakeCameraHandler
{
    public bool IsRunning { get; private set; }
    private readonly ShakeCamera _shakeCamera;


    public ShakeCameraHandler(ShakeCamera shakeCamera)
    {
        _shakeCamera = shakeCamera;
    }


    public void Run()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Add(ICameraShakeGenerator shakeGenerator)
    {
        shakeGenerator.onGeneratedShake += OnGeneratedShake;
    }

    private void OnGeneratedShake(float shakePower)
    {
        if (IsRunning == false)
            return;

        _shakeCamera.Shake(shakePower);
    }
}

public interface ICameraShakeGenerator
{
    event Action<float> onGeneratedShake;
}