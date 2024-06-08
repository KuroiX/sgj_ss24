using UnityEngine;

namespace IntroCutScene
{
    public class ShuffleBetweenSprites : MonoBehaviour
    {
        [SerializeField] private GameObject[] spriteObjects;
        [SerializeField] private int indexOfFirstActiveSprite = 0;
        [SerializeField] private float timeBeforeShuffleStarts;
        [SerializeField] private float timeBetweenShuffles;

        private int _currentIndex;
        private int _numberOfSprites;

        private void Awake()
        {
            _numberOfSprites = spriteObjects.Length;
            _currentIndex = 0;
            if (_numberOfSprites < 2) return;

            DisableAllButOne();
        }

        public void StartShuffling()
        {
            InvokeRepeating(nameof(ShuffleSprites), timeBeforeShuffleStarts, timeBetweenShuffles);
        }

        public void StopShuffling()
        {
            CancelInvoke(nameof(ShuffleSprites));
        }

        private void DisableAllButOne()
        {
            for (int i = 0; i < _numberOfSprites; i++)
            {
                if (i == indexOfFirstActiveSprite) continue;
                spriteObjects[i].SetActive(false);
            }
        }

        private void ShuffleSprites()
        {
            // disable old
            spriteObjects[_currentIndex].SetActive(false);

            // enable new
            _currentIndex = (_currentIndex + 1) % _numberOfSprites;
            spriteObjects[_currentIndex].SetActive(true);
        }
    }
}