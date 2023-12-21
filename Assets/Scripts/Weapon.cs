using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform _firePoint;
    public GameObject _bulletPrefab;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        // I might not need to use it if I improve my movement code with Unity New Input System
        if(Input.GetButtonDown("Fire1"))
        {
            // Apparently there is an Animation Layer concept I might want to apply later on
            animator.SetBool("IsShooting", true);
            Shoot();
        } 
        else if(Input.GetButtonUp("Fire1"))
        {  
            //  this needs to be changed, or the bullet should keep spwning as long as the button is down!
            animator.SetBool("IsShooting", false);
        }
    }

    private void Shoot()
    {
        // Instantiate(whatToSpawn, whereToSpawn, rotation)
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
