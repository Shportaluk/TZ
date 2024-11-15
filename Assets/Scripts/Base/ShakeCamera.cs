using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public bool IsShaking { get; private set; } = false;

    [SerializeField, Range(0, 1)] private float _ratio = 0.2f;
    [SerializeField] private Vector3 _shakeOffset;
    [SerializeField] private float _duration;
    private Vector3 _startPosition;


    public void Shake(float multiplier = 1f)
    {
        if (!IsShaking)
        {
            StartCoroutine(ShakeCor(_duration, multiplier));
        }
    }

    private IEnumerator ShakeCor(float duration, float multiplier = 1f)
    {
        IsShaking = true;
        _startPosition = transform.localPosition;
        PointsLerp pointsLerp = new PointsLerp();
        pointsLerp.Add(0, transform.localPosition);
        pointsLerp.Add(_ratio, transform.localPosition + (_shakeOffset * multiplier));
        pointsLerp.Add(1, transform.localPosition);

        float time = 0;
        while(time < duration)
        {
            transform.localPosition = pointsLerp.Lerp(time / duration);
            yield return null;
            time += Time.deltaTime;
        }

        transform.localPosition = _startPosition;
        IsShaking = false;
    }
}