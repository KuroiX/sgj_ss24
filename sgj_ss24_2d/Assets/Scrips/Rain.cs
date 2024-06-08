using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rain : MonoBehaviour
{
    [SerializeField] private GameObject rainPrefab;
    public float maxDropCount;
    public int spawnChance;
    public float height;
    public float width;

    private List<GameObject> _rainDrops;
    private float _halfHeight;
    private float _halfWidth;

    private void Start()
    {
        _rainDrops = new List<GameObject>();
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(-width, height, 0), new Vector3(width, height, 0));
        Debug.DrawLine(new Vector3(-width, -height, 0), new Vector3(width, -height, 0));
        Debug.DrawLine(new Vector3(-width, height, 0), new Vector3(-width, -height, 0));
        Debug.DrawLine(new Vector3(width, height, 0), new Vector3(width, -height, 0));
        
        if (Random.Range(0, spawnChance) == 0)
        {
            float randomX = Random.Range(-width, width);
            float randomY = Random.Range(-height, height);
            var instance = Instantiate(rainPrefab);
            instance.transform.position = new Vector3(randomX, randomY, 0);
            _rainDrops.Add(instance);
        }
    }
}