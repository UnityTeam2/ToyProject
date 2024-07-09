using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float HP = 100;
    public GameObject bulletPrefab;
    public Transform firePoint; 
    private GameObject target;
    

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        StartCoroutine(FireCoroutine());
    }
    private void Update()
    {
        transform.LookAt(target.transform);
    }


    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(direction * 70, ForceMode.VelocityChange);
            yield return new WaitForSeconds(3);
        }
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP < 0)
            Destroy(gameObject);
    }

}
