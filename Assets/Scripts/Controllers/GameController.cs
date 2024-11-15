using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private float _minGunPower;
    [SerializeField] private float _maxGunPower;
    [SerializeField] private ProjectilePool _projectilesPool;

    private IGunController _gunController = new PCGunController();


    private void Awake()
    {
        _gun.Init(_projectilesPool.Get);
        _gunController.Init(_gun);
    }

    private void Update()
    {
        _gunController.Update();
    }
}