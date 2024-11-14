using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
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

public interface IGunController
{
    void Init(Gun gun);
    void Update();
}

public class PCGunController : IGunController
{
    private Gun _gun;


    public void Init(Gun gun)
    {
        _gun = gun;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _gun.IsReloading == false)
        {
            _gun.Fire();
        }


        float speed = 500;
        float x = Input.GetAxis("Mouse X") / Screen.width * 1920f * Time.deltaTime * speed;
        float y = Input.GetAxis("Mouse Y") / Screen.height * 1080f * Time.deltaTime * speed;

        _gun.Rotate(new Vector2(y, x));
    }
}