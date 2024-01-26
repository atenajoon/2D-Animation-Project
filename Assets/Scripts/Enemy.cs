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


    void Start()
    {
        originalPosition = transform.position;
    }


    void Update()
    {
        MoveEnemy();
    }


    private void MoveEnemy()
    {
        float horizontalMovement = movingRight ? 1f : -1f;
        Vector2 movement = new Vector2(horizontalMovement, 0f) * moveSpeed * Time.deltaTime;


        transform.Translate(movement);


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
        Debug.Log("Died");
        Destroy(gameObject);
    }
}
