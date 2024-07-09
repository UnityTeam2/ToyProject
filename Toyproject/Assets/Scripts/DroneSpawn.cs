using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneSpawn : MonoBehaviour
{
    public GameObject dronePrefab; // 드론 프리팹
    public int poolSize = 2; // 풀 크기
    public float spawnInterval = 10f; // 스폰 간격 (초)
    public Vector3 spawnPosition = Vector3.zero; // 스폰 위치

    public List<GameObject> dronePool = new List<GameObject>(); // 드론 풀
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
            } // 비활성화 상태로 생성
            dronePool.Add(drone);
        }
    }

    public void SpawnRandomDrone()
    {
        // 비활성화된 드론을 찾아 활성화 후 랜덤한 위치에 생성
        for (int i = 0; i < dronePool.Count; i++)
        {
            if (!dronePool[i].activeInHierarchy)
            {
                Vector3 cameraPosition = Camera.main.transform.position; // 메인 카메라의 위치 가져오기
                Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f)); // y값을 1로 변경하여 약간 떠오르도록 설정
                dronePool[i].transform.position = cameraPosition + randomOffset;
                dronePool[i].SetActive(true);
                return; // 드론 하나를 생성하면 바로 반환하여 더 이상 생성하지 않도록 함
            }
        }

        // 풀이 모두 활성화된 상태일 경우, 새로운 드론 생성하지 않음
    }
}