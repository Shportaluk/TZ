using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private Transform _tranfromArt;
    [SerializeField] private Vector3 _reloadedOffset = new Vector3(0, -0.5f, 0);
    [SerializeField] private Gun _gun;

    private Vector3 _startLocalPos;
    private Vector3 _reloadPos;
    private readonly PointsLerp _pointsLerp = new PointsLerp();

    private void Awake()
    {
        _gun.onReloadingProgress += OnReloadingProgress;

        _startLocalPos = _tranfromArt.localPosition;
        _reloadPos = _startLocalPos + _reloadedOffset;
        _pointsLerp.Add(0, _startLocalPos);
        _pointsLerp.Add(0.25f, _reloadPos);
        _pointsLerp.Add(1f, _startLocalPos);
    }

    private void OnReloadingProgress(float t)
    {
        _tranfromArt.localPosition = _pointsLerp.Lerp(t);
    }
}