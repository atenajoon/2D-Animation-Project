using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 moveOffset;
    private Vector2 aPos;
    private Vector2 bPos;
    public int health = 10;
    // public GameObject deathEffect;

    void Start()
    {
        aPos = transform.position;
        bPos = aPos;
    }

    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {

        // moving the gameObject's position towards the bPos at the specified speed
        Vector2 newPosition = Vector2.MoveTowards(transform.position, bPos, moveSpeed * Time.deltaTime);

        // Smoothly moving the Rigidbody of the gameObject to the newPosition
        GetComponent<Rigidbody2D>().MovePosition(newPosition);

        // converting Vector3 Transform's position into a Vector2 by using the (Vector2) type casting operation,
        // the result is stored in a a Vector2 variable;
        Vector2 position2D = (Vector2)transform.position;

        // in Vector2, for comparing floating-point numbers we don't use direct equality (==) due to the potential for very small differences.
        // so, I compare postion2D and bPos to see if they are very close to each other within a small threshold
        if (Vector2.Distance(position2D, bPos) < 0.1f)
        {       
            if (bPos == aPos)
            {
                bPos = aPos + moveOffset;
            }
            else
            {
                bPos = aPos;
            }
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
