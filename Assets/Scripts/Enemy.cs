using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float moveRange;
    private bool movingRight = true;
    private Vector2 originalPosition;
    public int health = 10;
    public TextMeshProUGUI enemyHealthText;

    void Start()
    {
        originalPosition = transform.position;
    }


    void Update()
    {
        MoveEnemy();
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        // check if it hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            movingRight = !movingRight;
        }
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
        enemyHealthText.text = "EnemyHealth: " + health.ToString();
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
        SceneManager.LoadScene(0);
    }
}
