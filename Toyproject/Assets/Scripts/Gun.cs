using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioClip gunshotSound;
    public AudioSource audioSource;
    public GameObject particlePrefab;
    private Coroutine firingCoroutine;
    private float fireRate = 0.1f;

    private bool canPlayAudio = true;

    public void StartFiring()
    {
        if (firingCoroutine == null)
            firingCoroutine = StartCoroutine(FireRoutine());
    }

    public void StopFiring()
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(firePoint.forward * 100, ForceMode.VelocityChange);

            if (audioSource != null && gunshotSound != null)
            {
                audioSource.PlayOneShot(gunshotSound);
            }

            if (particlePrefab != null)
            {
                GameObject particle = Instantiate(particlePrefab, bullet.transform.position, bullet.transform.rotation);
                Destroy(particle, 0.5f); // 파티클을 일정 시간 후에 삭제 (예: 2초 후)
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    public void SpawnParticle(Vector3 position, Quaternion rotation)
    {
        GameObject particle = Instantiate(particlePrefab, position, rotation);
        Destroy(particle, 2.0f);
    }
}