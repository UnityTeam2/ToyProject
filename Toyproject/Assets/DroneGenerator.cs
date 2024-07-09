using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGenerator : MonoBehaviour
{
    public GameObject dronePrefab;
    public Transform playerTransform; 
    public int maxDrones = 70;
    public float spawnInterval = 0.5f;
    public float spawnDistance = 10.0f;

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
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance ;
        GameObject drone = Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
        activeDrones.Add(drone);
        StartCoroutine(MoveDroneToPlayer(drone));
        drone.GetComponent<Drone>().OnDestroyed += () => activeDrones.Remove(drone);
    }

    IEnumerator MoveDroneToPlayer(GameObject drone)
    {
        Vector3 startPosition = drone.transform.position + new Vector3((float)Random.Range(2, 4), (float)Random.Range(2, 4), (float)Random.Range(2, 4));
        Vector3 endPosition = playerTransform.position + playerTransform.forward * 5.0f + new Vector3((float)Random.Range(2, 4), (float)Random.Range(2, 4), (float)Random.Range(2, 4));
        float journeyTime = 1.0f; 
        float startTime = Time.time;

        while (Time.time - startTime < journeyTime)
        {
            drone.transform.position = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime) / journeyTime);
            drone.transform.LookAt(playerTransform);
            yield return null;
        }

        drone.GetComponent<Drone>().SetTarget(playerTransform);
    }
}
