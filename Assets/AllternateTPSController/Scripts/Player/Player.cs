using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 damping;
        public Vector2 sensitivity;
    }

    [SerializeField]
    float speed;
    [SerializeField]
    MouseInput mouseControl;

    CrossHair m_CrossHair;
    CrossHair CrossHair
    {
        get
        {
            if(m_CrossHair == null)
            {
                m_CrossHair = GetComponentInChildren<CrossHair>();
            }
            return m_CrossHair;
        }
    }

    MoveController m_MoveController;
    public MoveController moveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;
    void Start()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
    }

    void Update()
    {
        Vector2 direction = new Vector2(playerInput.vertical * speed, playerInput.horizontal * speed);
        moveController.Move(direction);

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.mouseInput.x, 1f / mouseControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.mouseInput.y, 1f / mouseControl.damping.y);
        // mouseInput.y = playerInput.mouseInput.y;

        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.sensitivity.x);
        CrossHair.LookHeight(mouseInput.y * mouseControl.sensitivity.y);
    }
}
