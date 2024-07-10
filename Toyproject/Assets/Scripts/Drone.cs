using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float HP = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform target;
    private Vector3 targetPosition;
    public delegate void DroneDestroyed();
    public event DroneDestroyed OnDestroyed;
    public ParticleSystem destructionParticles;

    public Vector3 areaMin; 
    public Vector3 areaMax; 
    public float smoothTime = 0.5f; 
    private Vector3 currentVelocity;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(FireCoroutine());
        InvokeRepeating("RandomMove", 0, 4f);
    }

    void RandomMove()
    {
        float randomX = Random.Range(areaMin.x, areaMax.x);
        float randomY = Random.Range(areaMin.y, areaMax.y);
        float randomZ = Random.Range(areaMin.z, areaMax.z);

        targetPosition = new Vector3(randomX, randomY, randomZ);
    }

    private void Update()
    {
        MoveTowardsTarget();
        transform.LookAt(target);
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if (target != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponentInChildren<Rigidbody>();
                Vector3 directionToTarget = (target.position - firePoint.position).normalized;
                rb.velocity = directionToTarget * 70;
                Destroy(bullet, 5);
            }
            yield return new WaitForSeconds(3);
        }
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Instantiate(destructionParticles, transform.position, Quaternion.identity);
            OnDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
