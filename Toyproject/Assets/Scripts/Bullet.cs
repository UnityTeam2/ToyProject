using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10;
    //public GameObject explosionPrefab; // ��ƼŬ �������� ������ public ����

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Drone>(out Drone drone))
        {
            drone.GetDamage(damage);

            //// �Ҹ��� ������ ��ġ���� ��ƼŬ ����
            //GameObject bang = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Destroy(bang, 1.0f); // ���÷� 2�� �Ŀ� ��ƼŬ ����

            Destroy(gameObject); // �Ҹ� �ı�
        }
    }
}