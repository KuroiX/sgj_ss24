using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Obscale : MonoBehaviour
{
    public float flyingSpeed;
    public float yOffset;
    public Ease easeVariable;
    public GameObject sprite;
    public float lifeTime;
    public float fadeoutTime;

    private float _currentFadeoutTime;
    private ObstacleSpawner _obstacleSpawner;
    private GameObject _obscale;
    
    public void SpawnObsacle(Vector3 spawnPos, ObstacleSpawner obstacleSpawner, GameObject obscale)
    {
        _obscale = obscale;
        _obstacleSpawner = obstacleSpawner;
        sprite.transform.position = spawnPos + new Vector3(0, yOffset, 0);
        sprite.transform.DOMove(spawnPos, flyingSpeed).SetEase(easeVariable);
        StartCoroutine(Despawn());
    }
    

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifeTime + flyingSpeed);
        var spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        _currentFadeoutTime = fadeoutTime;

        Color originalColor = spriteRenderer.color;

        while (_currentFadeoutTime > 0)
        {
            //Debug.Log("Fadeout Time: " + _currentFadeoutTime);
            float alpha = _currentFadeoutTime / fadeoutTime;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            _currentFadeoutTime -= Time.deltaTime;
            yield return null;
        }

        _obstacleSpawner.spawnedObstacles.Remove(gameObject);
        Destroy(gameObject);
    }
}
