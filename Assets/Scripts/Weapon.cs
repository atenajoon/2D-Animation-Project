using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform _firePoint;
    public GameObject _bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        // I might not need to use it if I improve my movement code with Unity New Input System
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Instantiate(whatToSpawn, whereToSpawn, rotation)
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
