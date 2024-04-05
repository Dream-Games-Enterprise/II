using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float minX = -2.26f;
    float maxX = 2.26f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            MovePaddle(touchPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 inputPosition = Input.mousePosition;
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(inputPosition);
            MovePaddle(touchPosition);
        }

        void MovePaddle(Vector3 touchPosition)
        {
            float clampedX = Mathf.Clamp(touchPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(touchPosition.y, transform.position.y, transform.position.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
