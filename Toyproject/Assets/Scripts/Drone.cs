using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Drone : MonoBehaviour
{
    public float HP = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Transform target;
    public GameObject droneDestroyPrefab; // 드론 파괴 시 파티클이 포함된 프리팹

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

            // 드론 파괴 프리팹에서 파티클 시스템을 인스턴스화하여 생성
            GameObject destructionParticles = Instantiate(droneDestroyPrefab, transform.position, Quaternion.identity);

            // 파티클 시스템 재생
            ParticleSystem ps = destructionParticles.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
                ps.Play();

        }
    }

    //IEnumerator PlayDestructionParticlesCoroutine()
    //{
    //    if (droneDestroyPrefab != null)
    //    {
    //        // 드론 파괴 프리팹에서 파티클 시스템을 인스턴스화하여 생성
    //        GameObject destructionParticles = Instantiate(droneDestroyPrefab, transform.position, Quaternion.identity);

    //        // 파티클 시스템 재생
    //        ParticleSystem ps = destructionParticles.GetComponentInChildren<ParticleSystem>();
    //        if (ps != null)
    //            ps.Play();

    //        // 재생 시간(파티클의 지속 시간)만큼 기다린 후 파괴
    //        yield return new WaitForSeconds(ps.main.duration);

    //        // 파티클 시스템 파괴
    //        Destroy(destructionParticles);
    //    }
    //}

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
}