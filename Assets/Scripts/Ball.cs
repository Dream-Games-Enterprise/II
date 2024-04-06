using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LevelManager levelManager;
    AutoAimer autoAimer;
    PlayerMovement playerMovement;
    Rigidbody2D rb;
    LineRenderer lr;
    CircleCollider2D collider2D;
    [SerializeField] Transform ballStartPos;

    float power = 0.001f;
    float maxLength = 1f;

    Vector3 dragStartPos;
    bool isDragging = false;
    public bool canAim = true;
    float aimDelay = 0.3f;
    public bool ballIsStationary;
    bool isInitialLaunch;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        collider2D = GetComponent<CircleCollider2D>();
        levelManager = FindObjectOfType<LevelManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        autoAimer = FindObjectOfType<AutoAimer>();
    }

    void Start()
    {
        transform.position = ballStartPos.transform.position;
        ballIsStationary = true;
        isInitialLaunch = true;
        lr.positionCount = 0;
        StartCoroutine(EnableAimingDelay());
    }

    /*void FixedUpdate()
    {
        if (ballIsStationary && canAim)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.MovePosition(rb.position + (Vector2)transform.right * power * Time.deltaTime);
        }
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            ContactPoint2D contact = collision.contacts[0];
            transform.position = contact.point;
            //set lr to match middle of ball here
            ballIsStationary = true;
            autoAimer.EnableLR();
            StartCoroutine(EnableAimingDelay());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            levelManager.RemoveLife();
            ResetBall();
            //Stop auto aimer
        }    
    }

    IEnumerator EnableAimingDelay()
    {
        yield return new WaitForSeconds(aimDelay);
        canAim = true;
    }

    void Update()
    {
        if (ballIsStationary && canAim && isInitialLaunch)
        {
            autoAimer.DisableLR();
            ProcessLaunchInput();
        }
        else if (ballIsStationary && canAim && !isInitialLaunch)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                autoAimer.EnableLR();
                LaunchBall();
            }
        }
    }

    void ResetBall()
    {
        transform.position = ballStartPos.transform.position;
        ballIsStationary = true;
        isInitialLaunch = true;
        StartCoroutine(EnableAimingDelay());
    }

    void LaunchBall()
    {
        if (ballIsStationary && !isInitialLaunch)
        {
            Vector3 direction = autoAimer.GetAimDirection();
            rb.AddForce(direction * power, ForceMode2D.Impulse);
            ballIsStationary = false;
        }
    }


    void ProcessLaunchInput()
    {
        if (Input.GetMouseButtonDown(0) && canAim)
        {
            DragStart(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && canAim)
        {
            Dragging(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragRelease(Input.mousePosition);
        }
    }

    void DragStart(Vector3 screenPos)
    {
        if (isInitialLaunch)
        {
            isDragging = true;
            dragStartPos = transform.position;
            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);
            lr.enabled = true;
        }
    }

    void Dragging(Vector3 screenPos)
    {
        if (isInitialLaunch)
        {
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(screenPos);
            draggingPos.z = 0f;

            Vector3 direction = (draggingPos - dragStartPos).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, 15f, 165f);
            direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

            draggingPos = dragStartPos + direction * maxLength;

            lr.positionCount = 2;
            lr.SetPosition(0, dragStartPos);
            lr.SetPosition(1, draggingPos);
            lr.enabled = true;
        }
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
        lr.enabled = false;
        isInitialLaunch = false;
    }
}
