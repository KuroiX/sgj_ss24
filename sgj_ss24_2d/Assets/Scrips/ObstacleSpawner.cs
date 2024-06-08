using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> obstacles;
    public float height;
    public float width;
    public float minDistance;
    public float maxNumberOfObstacles;
    public float minObstacleSpawnTime;
    public float maxObstacleSpawnTime;

    public List<GameObject> spawnedObstacles;

    public GameObject player;

    private void Start()
    {
        spawnedObstacles = new List<GameObject>();
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(-width + transform.position.x, height + transform.position.y, 0), new Vector3(width + transform.position.x, height + transform.position.y, 0));
        Debug.DrawLine(new Vector3(-width + transform.position.x, -height + transform.position.y, 0), new Vector3(width + transform.position.x, -height + transform.position.y, 0));
        Debug.DrawLine(new Vector3(-width + transform.position.x, height + transform.position.y, 0), new Vector3(-width + transform.position.x, -height + transform.position.y, 0));
        Debug.DrawLine(new Vector3(width + transform.position.x, height + transform.position.y, 0), new Vector3(width + transform.position.x, -height + transform.position.y, 0));
    }

    [ContextMenu("Spawn Obstacle")]
    public void SpawnObstacle()
    {
        if (spawnedObstacles.Count >= maxNumberOfObstacles) return;

        float randomX = Random.Range(-width + transform.position.x, width + transform.position.x);
        float randomY = Random.Range(-height + transform.position.y, height + transform.position.y);

        var spawnPos = new Vector3(randomX, randomY, 0);

        while (!IsPositionValid(spawnPos))
        {
            randomX = Random.Range(-width + transform.position.x, width + transform.position.x);
            randomY = Random.Range(-height + transform.position.y, height + transform.position.y);

            spawnPos = new Vector3(randomX, randomY, 0);
        }

        var num = Random.Range(0, obstacles.Count);
        var obstacle = Instantiate(obstacles[num]);

        obstacle.transform.position = spawnPos;
        obstacle.GetComponent<Obscale>().SpawnObsacle(spawnPos, this, obstacle);
        spawnedObstacles.Add(obstacle);
    }

    private bool IsPositionValid(Vector3 position)
    {
        if ((player.transform.position - position).magnitude < minDistance)
        {
            Debug.Log("Player too close");
            return false;
        }

        foreach (var spawnedObstacle in spawnedObstacles)
        {
            if ((spawnedObstacle.transform.position - position).magnitude < minDistance)
            {
                Debug.Log("Obstacle too close");
                return false;
            }
        }

        return true;
    }
    
    [ContextMenu("Start Spawning")]
    public void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        float spawnTime = Random.Range(minObstacleSpawnTime, maxObstacleSpawnTime);
        yield return new WaitForSeconds(spawnTime);
        SpawnObstacle();
        StartSpawning();
    }
}
