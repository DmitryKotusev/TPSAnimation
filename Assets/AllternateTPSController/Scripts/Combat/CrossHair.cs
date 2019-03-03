using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    Texture2D image;
    [SerializeField]
    int size;
    [SerializeField]
    float maxAngle;
    [SerializeField]
    float minAngle;

    float lookHeight;

    public void LookHeight(float value)
    {
        lookHeight += value;

        lookHeight = Mathf.Clamp(lookHeight, minAngle, maxAngle);
    }

    private void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size), image);
    }
}
