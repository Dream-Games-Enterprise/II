using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    bool isAiming;

    void Start()
    {
        isAiming = true;

        LaunchBall();
    }

    void LaunchBall()
    {
        if (isAiming)
        {

        }
    }












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
}
