using System;
using UnityEngine;

namespace IntroCutScene
{
    public class Flash : MonoBehaviour
    {
        [SerializeField] private float timeFlashing;

        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.gameObject.SetActive(false);
        }

        public void StartFlash()
        {
            _spriteRenderer.gameObject.SetActive(true);
            Invoke(nameof(StopFlash), timeFlashing);
        }
        
        public void StopFlash()
        {
            _spriteRenderer.gameObject.SetActive(false);
        }
    }
}