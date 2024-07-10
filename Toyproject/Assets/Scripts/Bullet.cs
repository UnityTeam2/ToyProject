using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10;
    //public GameObject explosionPrefab; // 파티클 프리펩을 설정할 public 변수

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Drone>(out Drone drone))
        {
            drone.GetDamage(damage);

            //// 불릿이 생성된 위치에서 파티클 생성
            //GameObject bang = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Destroy(bang, 1.0f); // 예시로 2초 후에 파티클 삭제

            Destroy(gameObject); // 불릿 파괴
        }
    }
}