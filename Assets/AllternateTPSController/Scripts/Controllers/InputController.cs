using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Vector2 mouseInput;

    public bool fire1;
    public bool reload;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        fire1 = Input.GetButton("Fire1");
        reload = Input.GetKey(KeyCode.R);
    }
}
