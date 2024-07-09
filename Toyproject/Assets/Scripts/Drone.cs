using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float HP = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Transform target;

    public delegate void DroneDestroyed();
    public event DroneDestroyed OnDestroyed;

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if (target != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(firePoint.forward * 70, ForceMode.VelocityChange);
                Destroy(gameObject, 5);
            }
            yield return new WaitForSeconds(3);
        }
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            if (OnDestroyed != null)
                OnDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
}
