using UnityEngine;
using UnityEngine.EventSystems;

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

        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            float speed = 500;
            float x = Input.GetAxis("Mouse X") / Screen.width * 1920f * Time.deltaTime * speed;
            float y = Input.GetAxis("Mouse Y") / Screen.height * 1080f * Time.deltaTime * speed;

            _gun.Rotate(new Vector2(y, x));
        }
    }
}