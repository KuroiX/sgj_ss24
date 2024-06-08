using UnityEngine;
using UnityEngine.Events;

namespace IntroCutScene
{
    [RequireComponent(typeof(Collider2D))]
    public class DetectDropMerge : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDropsCollided;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Drop")) return;
            
            FindObjectOfType<SceneLoader>().LoadNextScene();
        }
    }
}