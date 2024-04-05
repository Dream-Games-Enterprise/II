using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIndicator : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Camera camera;
    LineRenderer lr;

    Vector3 camOffset = new Vector3(0, 0, 10);

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            lr.enabled = true;
            lr.positionCount = 2;
            startPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
        } 

        if (Input.GetMouseButton(0))
        {
            endPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(1, endPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }
    }
}
