using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float moveRange;
    private bool movingRight = true;
    private Vector2 originalPosition;
    public int health = 10;
    private Collider2D blinkGameObject;
    private bool isImmune;
    private bool isTimerRunning;
    private float timeDuration = 1.5f;
    private float timer;


    void Start()
    {
        originalPosition = transform.position;

        isImmune = false;
        isTimerRunning = false;
        timer = timeDuration;
    }


    // void Update()
    // {
    //     MoveEnemy();
    // }

    void Update() {
        MoveEnemy();
        
        if( isTimerRunning == true)
        {
            if(timer > 0) 
            {
                timer -= Time.deltaTime;
            } 
            else
            {
                blinkGameObject.GetComponent<BlinkGameObject>().StopBlinkGameObject();
                isImmune = false;
                isTimerRunning = false;
                ResetTimer();
            }
        }
    }

    private void ResetTimer() 
    {
        timer = timeDuration;
        Debug.Log("timeDur" + timer);
    }
    private void MoveEnemy()
    {
        float horizontalMovement = movingRight ? 1f : -1f;
        Vector2 movement = new Vector2(horizontalMovement, 0f) * moveSpeed * Time.deltaTime;


        transform.Translate(movement);

        // Mathf.Abs(...) takes the absolute value of that difference. 
        // This ensures that the comparison is based on the magnitude of the difference rather than its direction
        if (Mathf.Abs(transform.position.x - originalPosition.x) >= moveRange)
        {
            // Change direction when reaching the move range
            movingRight = !movingRight;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health: "+ health);

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die ()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

     void OnTriggerEnter2D(Collider2D other)
    {
           // check if the player is hit by Enemy
        if (other.CompareTag("Player"))
        {
            Debug.Log("CPlayer!!");
            // ** add a safeTime after each hit **
            blinkGameObject = other;
            if (isTimerRunning == false)
            {
                Debug.Log("FalseTimeRunning");
                if (timer > 0 && isImmune == false)
                {
                    isImmune = true;
                    isTimerRunning = true;
                     Debug.Log("isImmune: " + isImmune);
                    // ResetTimer();

                    blinkGameObject.GetComponent<PlayerController>().SubtractHealth(1);
                    blinkGameObject.GetComponent<BlinkGameObject>().CallStartBlinkGameObject();
                }
            }    

            // ** add Auch audio **
            // blinkGameObject.GetComponent<AudioPlayer>().PlayAudio();
        }
    }
}
