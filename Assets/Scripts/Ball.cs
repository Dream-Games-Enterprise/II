using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    LineRenderer lr;

    float power = 0.005f;
    float maxLength = 1f;

    Vector3 dragStartPos;
    bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    void Update()
    {
        if (!isDragging && IsBallMoving())
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                ProcessInput(touch.position, touch.phase);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                ProcessInput(Input.mousePosition, TouchPhase.Began);
            }
            else if (Input.GetMouseButton(0))
            {
                ProcessInput(Input.mousePosition, TouchPhase.Moved);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ProcessInput(Input.mousePosition, TouchPhase.Ended);
            }
        }
    }

    void ProcessInput(Vector3 screenPos, TouchPhase phase)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                DragStart(screenPos);
                break;
            case TouchPhase.Moved:
                if (isDragging)
                {
                    Dragging(screenPos);
                }
                break;
            case TouchPhase.Ended:
                if (isDragging)
                {
                    DragRelease(screenPos);
                }
                break;
        }
    }

    void DragStart(Vector3 screenPos)
    {
        isDragging = true;
        dragStartPos = transform.position;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }

    void Dragging(Vector3 screenPos)
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(screenPos);
        draggingPos.z = 0f;

        Vector3 direction = (draggingPos - dragStartPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, 10f, 170f);

        direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

        draggingPos = dragStartPos + direction * maxLength;

        lr.positionCount = 2;
        lr.SetPosition(0, dragStartPos);
        lr.SetPosition(1, draggingPos);
    }

    void DragRelease(Vector3 screenPos)
    {
        isDragging = false;
        lr.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(screenPos);
        dragReleasePos.z = 0f;

        Vector3 direction = (dragReleasePos - dragStartPos).normalized;

        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}

    /*Rigidbody2D rb;
    LineRenderer lr;
    bool isBallStationary;
    bool isAiming;
    Vector2 aimDirection;
    float launchSpeed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lr.positionCount = 2;
        lr.enabled = false;
        isBallStationary = true;
        isAiming = false;
    }

    void Update()
    {
        if (isBallStationary)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAiming = true;
                aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            }
            else if (Input.GetMouseButtonUp(0) && isAiming)
            {
                isAiming = false;
                LaunchBall();
            }

            UpdateAimLine();
        }
    }

    void UpdateAimLine()
    {
        lr.enabled = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + (Vector3)aimDirection * 5f); // Set the length of the aim line
    }

    void LaunchBall()
    {
        rb.velocity = aimDirection * launchSpeed;
        isBallStationary = false;
        lr.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            isBallStationary = true;
        }    
    }*/












    /*OLD MOVEMENT
     * Rigidbody2D rb;

    float startSpeed = 7.5f;
    float extraSpeed = 1.5f;
    float maxSpeed = 4f;
    int hitCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(KickOff(1f));    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        if (collision.gameObject.name == "Player")
        {
            Bounce(collision);
        }
    }

    public IEnumerator KickOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        MoveBall(Vector2.down);
    }

    void MoveBall(Vector2 direction)
    {
        direction = direction.normalized;
        float ballSpeed = startSpeed + hitCounter * extraSpeed;
        rb.velocity = direction * ballSpeed;
    }

    void Bounce(Collision2D collision)
    {
        Vector3 ballPosition = transform.position;
        Vector3 paddlePosition = collision.transform.position;
        float paddleHeight = collision.collider.bounds.size.y;

        float positionX = (ballPosition.x - paddlePosition.x) / paddleHeight;

        float positionY = 0f;
        if (collision.gameObject.name == "Player")
        {
            positionY = 1;
        }

        IncreaseHitCounter();
        MoveBall(new Vector2(positionX, positionY));
    }

    public void IncreaseHitCounter()
    {
        if (hitCounter * extraSpeed < maxSpeed)
        {
            hitCounter++;
        }
    }*/
