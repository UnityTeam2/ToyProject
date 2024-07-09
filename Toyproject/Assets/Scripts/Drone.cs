using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Drone : MonoBehaviour
{
    public float HP = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Transform target;
    public GameObject droneDestroyPrefab;

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
        if (HP < 0)
        {
            StartCoroutine(PlayDestructionParticlesCoroutine());
            if (OnDestroyed != null)
                OnDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    IEnumerator PlayDestructionParticlesCoroutine()
    {
        if (droneDestroyPrefab != null)
        {
            GameObject destructionParticles = Instantiate(droneDestroyPrefab, transform.position, Quaternion.identity);

            ParticleSystem ps = destructionParticles.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
                ps.Play();

            yield return new WaitForSeconds(ps.main.duration);

            Destroy(destructionParticles);
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
}