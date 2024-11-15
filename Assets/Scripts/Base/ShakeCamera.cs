using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public bool IsShaking { get; private set; } = false;

    [SerializeField] private float _shakeStrength;
    [SerializeField] private float _duration;
    private Vector3 _startPosition;


    public void Shake(float multiplier = 1f)
    {
        Shake(_shakeStrength * multiplier, _duration);
    }

    public void Shake(float shakeStrength, float duration = 0.1f)
    {
        if (!IsShaking)
        {
            StartCoroutine(ShakeCor(shakeStrength, duration));
        }
    }

    private IEnumerator ShakeCor(float shakeStrength, float duration)
    {
        IsShaking = true;
        _startPosition = transform.localPosition;
        float time = 0f;

        while (time < duration * 0.1f)
        {
            Vector3 offset = new Vector3(0f, 0f, -shakeStrength * (1 - time / (duration * 0.1f)));
            transform.localPosition = _startPosition + offset;
            time += Time.deltaTime;
            yield return null;
        }


        time = 0f;
        while (time < duration * 0.9f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPosition, time / (duration * 0.9f));
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _startPosition;
        IsShaking = false;
    }
}