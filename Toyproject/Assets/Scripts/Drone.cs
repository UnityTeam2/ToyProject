using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Drone : MonoBehaviour
{
    public float HP = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Transform target;
    public GameObject droneDestroyPrefab; // ��� �ı� �� ��ƼŬ�� ���Ե� ������

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
            //StartCoroutine(PlayDestructionParticlesCoroutine());
            if (OnDestroyed != null)
                OnDestroyed.Invoke();
            Destroy(gameObject);

            // ��� �ı� �����տ��� ��ƼŬ �ý����� �ν��Ͻ�ȭ�Ͽ� ����
            GameObject destructionParticles = Instantiate(droneDestroyPrefab, transform.position, Quaternion.identity);

            // ��ƼŬ �ý��� ���
            ParticleSystem ps = destructionParticles.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
                ps.Play();

        }
    }

    //IEnumerator PlayDestructionParticlesCoroutine()
    //{
    //    if (droneDestroyPrefab != null)
    //    {
    //        // ��� �ı� �����տ��� ��ƼŬ �ý����� �ν��Ͻ�ȭ�Ͽ� ����
    //        GameObject destructionParticles = Instantiate(droneDestroyPrefab, transform.position, Quaternion.identity);

    //        // ��ƼŬ �ý��� ���
    //        ParticleSystem ps = destructionParticles.GetComponentInChildren<ParticleSystem>();
    //        if (ps != null)
    //            ps.Play();

    //        // ��� �ð�(��ƼŬ�� ���� �ð�)��ŭ ��ٸ� �� �ı�
    //        yield return new WaitForSeconds(ps.main.duration);

    //        // ��ƼŬ �ý��� �ı�
    //        Destroy(destructionParticles);
    //    }
    //}

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
}