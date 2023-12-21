using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 20f;
    public Rigidbody2D _rigidbody;
    private int _damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        // tell the bullet to fly forward on spawn
        // I am using "right" instead of "forwrd" bcs forward is for Z axis and in 2D game we don't have Z axis. so "right" does the work for the X axis
        _rigidbody.velocity = transform.right * _speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(_damage);
            Debug.Log("hitInfo: " + hitInfo.name);
        }

        Destroy(gameObject); // destroy the bullet
    }
}
