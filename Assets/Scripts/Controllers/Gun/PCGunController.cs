using UnityEngine;
using UnityEngine.EventSystems;

public class PCGunController : IGunController
{
    private Gun _gun;
    private bool _isMouseDown = false;

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


        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            _isMouseDown = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
        }

        if(_isMouseDown)
        {
            float speed = 500;
            float x = Input.GetAxis("Mouse X") / Screen.width * 1920f * Time.deltaTime * speed;
            float y = Input.GetAxis("Mouse Y") / Screen.height * 1080f * Time.deltaTime * speed;

            _gun.Rotate(new Vector2(y, x));
        }
    }
}