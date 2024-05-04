
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fireball : MonoBehaviour
{
    public float moveSpeed;
    public float moveRange;
    private bool movingRight = true;
    private Vector2 originalPosition;
    public int health = 3;
    public TextMeshProUGUI fireballHealthText;

    void Start()
    {
        if (fireballHealthText == null)
        {
            Debug.LogError("Health text is not assigned to the Fireball Object.");
            return;
        }
        originalPosition = transform.position;
    }


    void Update()
    {
        MoveFireball();
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        // check if it hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            movingRight = !movingRight;
        }
        
    }
    
    private void MoveFireball()
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
        fireballHealthText.text = "FireballHealth: " + health.ToString();
        Debug.Log("Health: "+ health);

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die ()
    {
        Debug.Log("Fireball Died");
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
