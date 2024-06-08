using UnityEngine;

namespace IntroCutScene
{
    public class Flash : MonoBehaviour
    {
        [SerializeField] private float timeFlashing;
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void StartFlash()
        {
            gameObject.SetActive(true);
            Invoke(nameof(StopFlash), timeFlashing);
        }

        public void StopFlash()
        {
            gameObject.SetActive(false);
        }
    }
}