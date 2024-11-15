using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private float _minGunPower;
    [SerializeField] private float _maxGunPower;
    [SerializeField] private ProjectilePool _projectilesPool;
    [SerializeField] private SettingGunUIView _settingGunUIView;

    private IGunController _gunController = new PCGunController();


    private void Awake()
    {
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