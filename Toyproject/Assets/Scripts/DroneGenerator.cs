using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGenerator : MonoBehaviour
{
    public GameObject dronePrefab;
    public Transform playerTransform;
    private int maxDrones = 4;
    private float spawnInterval = 0.5f;

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
            if (activeDrones.Count == 0)
            {
                maxDrones++;
                for (int i = 0; i < maxDrones; i++)
                    SpawnDrone();
            }
        }
    }

    void SpawnDrone()
    {
        Vector3 spawnPosition = playerTransform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(0, 4f), Random.Range(0f, 2f));

        GameObject drone = Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
        activeDrones.Add(drone);

        drone.GetComponent<Drone>().OnDestroyed += () => {
            activeDrones.Remove(drone);
        };
    }
}
