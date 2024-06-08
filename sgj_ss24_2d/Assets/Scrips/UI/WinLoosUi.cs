using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinLoosUi : MonoBehaviour
{
    [Header("Winn")]
    [SerializeField] private GameObject winnUi;
    [SerializeField] private DOTweenAnimation winnFadeAnimation;
    
    [Header("Loos")]
    [SerializeField] private GameObject loosUi;
    [SerializeField] private DOTweenAnimation loosFadeAnimation;

    [ContextMenu(nameof(DisplayWinnUI))]
    public void DisplayWinnUI()
    {
        winnUi.SetActive(true);
        winnFadeAnimation.DORestart();
    }
    
    [ContextMenu(nameof(DisplayLoosUI))]
    public void DisplayLoosUI()
    {
        loosUi.SetActive(true);
        loosFadeAnimation.DORestart();
    }
}
