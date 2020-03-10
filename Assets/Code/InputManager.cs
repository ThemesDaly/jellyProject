using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Data

    public static float magnitude;
    public float sensitivity = 2f;

    private Vector2 latePositionMouse;
    private Vector2 updatePositionMouse;

    private Vector2 latePositionTouch;
    private Vector2 updatePositionTouch;

    #endregion

    #region Unity

    private void Start()
    {
        magnitude = 0;

        latePositionMouse = Vector2.zero;
        updatePositionMouse = Vector2.zero;

        latePositionTouch = Vector2.zero;
        updatePositionTouch = Vector2.zero;
    }

    private void Update()
    {
        magnitude = Mathf.Lerp(magnitude, 0, 20f * Time.deltaTime);

        MouseControl();
    }

    #endregion

    #region Core

    private void MouseControl()
    {
        if(!Input.GetMouseButton(0))
        {
            latePositionMouse = Vector2.zero;
            updatePositionMouse = Vector2.zero;
            return;
        }

        if (latePositionMouse == Vector2.zero)
        {
            latePositionMouse = Input.mousePosition;
            latePositionMouse.x = 0;
        }
        else
        {
            updatePositionMouse = Input.mousePosition;
            updatePositionMouse.x = 0;

            float result = (latePositionMouse - updatePositionMouse).magnitude / (Screen.height);

            if ((latePositionMouse - updatePositionMouse).y < 0)
            {
                magnitude += result * sensitivity;
            }
            else
            {
                magnitude -= result * sensitivity;
            }

            latePositionMouse = Vector2.zero;
        }
    }

    #endregion
}
