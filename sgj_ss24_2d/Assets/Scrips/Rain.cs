using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rain : MonoBehaviour
{
    [SerializeField] private GameObject rainPrefab;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float height;
    public float width;

    private List<GameObject> _rainDrops;
    private float _halfHeight;
    private float _halfWidth;

    private void Start()
    {
        _rainDrops = new List<GameObject>();
        
    }

    [ContextMenu("Start Rain")]
    public void StartRain()
    {
        StartCoroutine(SpawnRain());
        GetComponent<ParticleSystem>().Play();
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(-width + transform.position.x, height + transform.position.y, 0), new Vector3(width + transform.position.x, height + transform.position.y, 0));
        Debug.DrawLine(new Vector3(-width + transform.position.x, -height + transform.position.y, 0), new Vector3(width + transform.position.x, -height + transform.position.y, 0));
        Debug.DrawLine(new Vector3(-width + transform.position.x, height + transform.position.y, 0), new Vector3(-width + transform.position.x, -height + transform.position.y, 0));
        Debug.DrawLine(new Vector3(width + transform.position.x, height + transform.position.y, 0), new Vector3(width + transform.position.x, -height + transform.position.y, 0));
    }

    private IEnumerator SpawnRain()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);
        float randomX = Random.Range(-width + transform.position.x, width + transform.position.x);
        float randomY = Random.Range(-height + transform.position.y, height + transform.position.y);
        var instance = Instantiate(rainPrefab);
        instance.transform.position = new Vector3(randomX, randomY, 0);
        _rainDrops.Add(instance);
        
        StartRain();
    }
}