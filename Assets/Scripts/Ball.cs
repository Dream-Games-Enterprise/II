using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D rb;
    LineRenderer lr;

    float power = 0.001f;
    float maxLength = 1f;

    Vector3 dragStartPos;
    bool isDragging = false;
    bool canAim = true;
    float aimDelay = 0.2f;
    public bool ballIsStationary;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        ballIsStationary = true;
        isDragging = false;
        lr.positionCount = 0;
        StartCoroutine(EnableAimingDelay());
    }

    void FixedUpdate()
    {
        if (ballIsStationary && canAim)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.MovePosition(rb.position + (Vector2)transform.right * power * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            ContactPoint2D contact = collision.contacts[0];
            transform.position = contact.point;
            ballIsStationary = true;
            StartCoroutine(EnableAimingDelay());
        }
    }

    IEnumerator EnableAimingDelay()
    {
        yield return new WaitForSeconds(aimDelay);
        canAim = true;
    }

    void Update()
    {
        if (ballIsStationary && canAim && !isDragging)
        {
            ProcessInput();
        }
    }

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStart(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Dragging(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DragRelease(Input.mousePosition);
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
        ballIsStationary = false;
        canAim = false;
        playerMovement.EnableMovement();
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
