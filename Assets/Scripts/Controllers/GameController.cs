using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private float _minGunPower;
    [SerializeField] private float _maxGunPower;

    [Header("Pools")]
    [SerializeField] private ProjectilePool _projectilesPool;
    [SerializeField] private ExplotionEffectPool _explotionEffectsPool;

    [Header("UI Views")]
    [SerializeField] private SettingGunUIView _settingGunUIView;

    private IGunController _gunController = new PCGunController();
    private ProjectileExplotionHandler _projectileExplotionHandler;


    private void Awake()
    {
        _projectileExplotionHandler = new ProjectileExplotionHandler(_projectilesPool, _explotionEffectsPool.Get);
        _projectileExplotionHandler.Run();

        _gun.Init(_projectilesPool.Get);
        _gunController.Init(_gun);

        SettingGunModel settingGunModel = new SettingGunModel(_gun, _minGunPower, _maxGunPower);
        _settingGunUIView.SetModel(settingGunModel);
        _settingGunUIView.UpdateView();
    }

    private void Update()
    {
        _gunController.Update();
    }
}
