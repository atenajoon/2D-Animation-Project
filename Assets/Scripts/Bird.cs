using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float _speed = 5f;
    public Rigidbody2D _rigidbody;
    // private int _damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (_rigidbody == null)
        {
            Debug.LogError("_rigidbody is not assigned to Bird object.");
            return;
        }
        // tell the bird to fly forward on spawn
        // I am using "right" instead of "forwrd" bcs forward is for Z axis and in 2D game we don't have Z axis. so "right" does the work for the X axis
        _rigidbody.velocity = transform.right * _speed;
    }

    // void OnTriggerEnter2D (Collider2D hitInfo)
    // {
    //     Player player = hitInfo.GetComponent<Player>();
    //     if(player != null)
    //     {
    //         // player.TakeDamage(_damage);
    //         Debug.Log("hitInfo: " + hitInfo.name);
    //     }

    //     Destroy(gameObject); // destroy the bird on Collision
    // }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // destroy the bird out of the scene
    }

}
