using UnityEngine;

namespace IntroCutScene
{
    public class SpriteFadeIn : MonoBehaviour
    {
        [SerializeField] private GameObject[] spriteObjects;
        [SerializeField] private float timeBeforeFirstFadeIn;
        [SerializeField] private float timeBetweenFadeIns = .4f;

        private int _currentIndex;
        private int _numberOfSprites;
        private SpriteRenderer _currentSprite;

        private void Start()
        {
            _numberOfSprites = spriteObjects.Length;
            if (_numberOfSprites == 2) return;

            _currentIndex = 0;

            DisableAll();
        }

        public void FadeInAll()
        {
            InvokeRepeating(nameof(FadeInNext), timeBeforeFirstFadeIn, timeBetweenFadeIns);
        }

        private void DisableAll()
        {
            foreach (var sprite in spriteObjects)
            {
                sprite.SetActive(false);
            }
        }

        private void FadeInNext()
        {
            if (_currentIndex == _numberOfSprites - 1)
            {
                CancelInvoke(nameof(FadeInNext));
                return;
            }

            _currentIndex++;

            // _currentSprite = spriteObjects[_currentIndex].GetComponent<SpriteRenderer>();
            // var currentSpriteColor = _currentSprite.color;
            // currentSpriteColor = new Color(currentSpriteColor.r, currentSpriteColor.g, currentSpriteColor.b, 0);
            // spriteObjects[_currentIndex].GetComponent<SpriteRenderer>().color = currentSpriteColor;
            spriteObjects[_currentIndex].SetActive(true);
        }
    }
}