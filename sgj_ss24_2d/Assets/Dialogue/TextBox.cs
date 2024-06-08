using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue
{
    public class TextBox : MonoBehaviour
    {
        [Header("Points")]
        [SerializeField] private Transform belowScreenPoint;
        [SerializeField] private Transform inScreenPoint;

        [Header("Animation Properties")] [SerializeField]
        private float duration;
        
        // private properties
        private DOTweenAnimation _doTweenAnimation;

        private void Awake()
        {
            transform.position = belowScreenPoint.position;
            InitAnimation();
        }

        private void InitAnimation()
        {
            _doTweenAnimation = GetComponent<DOTweenAnimation>();
            _doTweenAnimation.duration = duration;
        }
        
        
        [ContextMenu("move up")]
        public void MoveUp()
        {
            _doTweenAnimation.DOPlayForward();
        }
        
        [ContextMenu("move down")]
        public void MoveDown()
        {
            _doTweenAnimation.DOPlayBackwards();
        }
    }
}