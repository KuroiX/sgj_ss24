using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoalSpawner : MonoBehaviour
{
    public float height;
    public float width;

    public float minTime;
    public float maxTime;

    public GameObject coalPrefab;

    [ContextMenu("Spawn coal")]
    public void SpawnCoal()
    {
        StartCoroutine(SpawnCoalRoutine());
    }
    
    private IEnumerator SpawnCoalRoutine()
    {
        float time = Random.Range(minTime, maxTime);

        yield return new WaitForSeconds(time);

        float randomX = Random.Range(-width, width);
        float randomX2 = Random.Range(-width, width);
        int up = Random.Range(0, 2);


        var instance = Instantiate(coalPrefab);

        if (up == 0)
        {
            instance.transform.position = new Vector3(randomX, -height, 0);
            instance.GetComponent<CoalMovement>().moveDirection =
                (new Vector3(randomX2, height, 0) - new Vector3(randomX, -height, 0));
        }
        else
        {
            instance.transform.position = new Vector3(randomX, height, 0);
            instance.GetComponent<CoalMovement>().moveDirection =
                (new Vector3(randomX2, -height, 0) - new Vector3(randomX, height, 0));
        }
        
        SpawnCoal();
    }
    
}
