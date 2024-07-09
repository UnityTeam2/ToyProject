using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneSpawn : MonoBehaviour
{
    public GameObject dronePrefab; // ��� ������
    public int poolSize = 2; // Ǯ ũ��
    public float spawnInterval = 10f; // ���� ���� (��)
    public Vector3 spawnPosition = Vector3.zero; // ���� ��ġ

    public List<GameObject> dronePool = new List<GameObject>(); // ��� Ǯ
    public float timer = 0f;

    public void Start()
    {
        InitializeDronePool();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnRandomDrone();
        }
    }

    public void InitializeDronePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject drone = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            if (drone != null)
            {
                drone.SetActive(false);
            } // ��Ȱ��ȭ ���·� ����
            dronePool.Add(drone);
        }
    }

    public void SpawnRandomDrone()
    {
        // ��Ȱ��ȭ�� ����� ã�� Ȱ��ȭ �� ������ ��ġ�� ����
        for (int i = 0; i < dronePool.Count; i++)
        {
            if (!dronePool[i].activeInHierarchy)
            {
                Vector3 cameraPosition = Camera.main.transform.position; // ���� ī�޶��� ��ġ ��������
                Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f)); // y���� 1�� �����Ͽ� �ణ ���������� ����
                dronePool[i].transform.position = cameraPosition + randomOffset;
                dronePool[i].SetActive(true);
                return; // ��� �ϳ��� �����ϸ� �ٷ� ��ȯ�Ͽ� �� �̻� �������� �ʵ��� ��
            }
        }

        // Ǯ�� ��� Ȱ��ȭ�� ������ ���, ���ο� ��� �������� ����
    }
}