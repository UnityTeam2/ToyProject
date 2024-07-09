using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGenerator : MonoBehaviour
{
    public GameObject dronePrefab;
    public Transform playerTransform;
    public int maxDrones = 7;
    public float spawnInterval = 0.5f;
    public float spawnDistance = 10.0f;
    public float minDroneDistance = 2.0f; // 최소 드론 간격

    private List<GameObject> activeDrones = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnDrones());
    }

    IEnumerator SpawnDrones()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (activeDrones.Count < maxDrones)
            {
                SpawnDrone();
            }
        }
    }

    void SpawnDrone()
    {

        GameObject drone = Instantiate(dronePrefab, playerTransform.position + new Vector3 (Random.Range(-5f,5f), Random.Range(0,4f), Random.Range(0f,2f)), Quaternion.identity);

        drone.GetComponent<Drone>().OnDestroyed += () => activeDrones.Remove(drone);
    }



}
