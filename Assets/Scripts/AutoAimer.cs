using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimer : MonoBehaviour
{
    Ball ball;

    LineRenderer autoLR;
    public Transform lineRendererPivot;
    public float minAngle = 0f;
    public float maxAngle = 360f;
    float lerpSpeed = 0.75f;
    float lineLength = 1f;

    private float currentAngle;

    void Awake()
    {
        autoLR = GetComponent<LineRenderer>();
        ball = FindObjectOfType<Ball>();
    }

    void Start()
    {
        currentAngle = minAngle;
        lineRendererPivot.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    void Update()
    {
        MoveLine();
    }

    void MoveLine()
    {
        currentAngle = Mathf.LerpAngle(minAngle, maxAngle, Mathf.PingPong(Time.time * lerpSpeed, 1f));

        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;

        direction.Normalize();
        direction *= lineLength;

        autoLR.SetPosition(0, lineRendererPivot.position);
        autoLR.SetPosition(1, lineRendererPivot.position + direction);
    }

    public Vector3 GetAimDirection()
    {
        float currentAngle = Mathf.LerpAngle(minAngle, maxAngle, Mathf.PingPong(Time.time * lerpSpeed, 1f));
        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;
        direction.Normalize();
        autoLR.enabled = false;
        return direction;
    }

    public void EnableLR()
    {
        autoLR.enabled = true;
    }

    public void DisableLR()
    {
        autoLR.enabled = false;
    }
}
