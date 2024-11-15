using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ProjectilePathDrawer : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _maxSimulationTime = 5;
    [SerializeField] private float _projectileMass = 10;

    private void Update()
    {
        var path = GetPath().ToArray();
        _lineRenderer.positionCount = path.Length;
        _lineRenderer.SetPositions(path);
    }

    private IEnumerable<Vector3> GetPath()
    {
        yield return _gun.SpawnPoint.position - _gun.SpawnPoint.forward * 2;
        var path = Projectile.GeneratePath(_gun.SpawnPoint.position, _gun.SpawnPoint.forward * _gun.Power, _projectileMass, _maxSimulationTime);

        foreach (var item in path)
        {
            yield return item;
        }
    }
}