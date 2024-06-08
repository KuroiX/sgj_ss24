using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dialogue
{
    public class TextBox : MonoBehaviour
    {
        [SerializeField] private Transform belowScreenPoint;
        [SerializeField] private Image imageComponent;
        [SerializeField] private float duration = 1;

        // private properties
        private DOTweenAnimation _doTweenAnimation;
        private TMP_Text _textComponent;


        #region Init

        private void Awake()
        {
            transform.position = belowScreenPoint.position;
            _textComponent = GetComponentInChildren<TMP_Text>();
            InitAnimation();
        }

        private void InitAnimation()
        {
            _doTweenAnimation = GetComponent<DOTweenAnimation>();
            _doTweenAnimation.duration = duration;
        }

        #endregion

        public float GetDuration()
        {
            return duration;
        }

        public void DisplayTextBox(string text, Sprite icon)
        {
            SetText(text);
            SetIcon(icon);
            MoveUp();
        }

        public void HideTextBox()
        {
            MoveDown();

            // clear text when animation has finished
            Invoke(nameof(ClearText), duration);
        }

        // ICON
        private void SetIcon(Sprite icon)
        {
            imageComponent.overrideSprite = icon;
        }


        // TEXT
        private void SetText(string text)
        {
            _textComponent.text = text;
        }

        private void ClearText()
        {
            _textComponent.text = "";
        }


        // MOVEMENT ANIMATION
        private void MoveUp()
        {
            _doTweenAnimation.DOPlayForward();
        }

        private void MoveDown()
        {
            _doTweenAnimation.DOPlayBackwards();
        }
    }
}