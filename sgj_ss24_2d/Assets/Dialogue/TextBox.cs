using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue
{
    public class TextBox : MonoBehaviour
    {
        [Header("Points")] [SerializeField] private Transform belowScreenPoint;

        [Header("Animation Properties")] [SerializeField]
        private float duration = 1;

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

        public void DisplayTextBox(string text)
        {
            SetText(text);
            MoveUp();
        }
        
        public void HideTextBox()
        {
            MoveDown();
            
            // clear text when animation has finished
            Invoke(nameof(ClearText), duration);
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