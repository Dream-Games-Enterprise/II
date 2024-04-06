using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimer : MonoBehaviour
{
    LineRenderer autoLR;

    void Awake()
    {
        autoLR = GetComponent<LineRenderer>();    
    }

    public Transform lineRendererPivot; // Empty GameObject where the line renderer should rotate around
    public float minAngle = 15f;
    public float maxAngle = 165f;
    public float lerpSpeed = 1f;

    private float currentAngle;

    void Start()
    {
        currentAngle = minAngle; // Start at the minimum angle
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Stop rotating when player gives input
            enabled = false;
        }

        // Interpolate between min and max angles
        currentAngle = Mathf.LerpAngle(minAngle, maxAngle, Mathf.PingPong(Time.time * lerpSpeed, 1f));

        // Convert angle to a direction vector
        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;

        // Set the positions of the line renderer
        autoLR.SetPosition(0, lineRendererPivot.position);
        autoLR.SetPosition(1, lineRendererPivot.position + direction * 10f);
    }
}
