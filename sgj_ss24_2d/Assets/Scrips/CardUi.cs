using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class CardUi : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation upLeftInAnimation;
    [SerializeField] private DOTweenAnimation upRightInAnimation;

    [SerializeField] private DOTweenAnimation upLeftOutAnimation;
    [SerializeField] private DOTweenAnimation upRightOutAnimation;

    [SerializeField] private DOTweenAnimation swapL2RAnimation;
    [SerializeField] private DOTweenAnimation swapR2LAnimation;


    private void Update()
    {
        // TODO TEST:
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestAnis1();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TestAnis2();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            TestAnis3();
        }
    }

    public void CreateNewUICards()
    {
        
    }

    private void PlayInUpLeft(RectTransform transform)
    {
        transform.position = upLeftInAnimation.transform.position;
        transform.SetParent(upLeftInAnimation.transform);
        upLeftInAnimation.DORestart();
    }
    
    private void PlayInUpRight(RectTransform transform)
    {
        
        transform.position = upRightInAnimation.transform.position;
        transform.SetParent(upRightInAnimation.transform);
        upRightInAnimation.DORestart();
    }
    
    private void PlayOutUpLeft(RectTransform transform)
    {
        transform.position = upLeftOutAnimation.transform.position;
        transform.SetParent(upLeftOutAnimation.transform);
        upLeftOutAnimation.DORestart();
    }
    private void PlayOutUpRight(RectTransform transform)
    {
        transform.position = upRightOutAnimation.transform.position;
        transform.SetParent(upRightOutAnimation.transform);
        upRightOutAnimation.DORestart();
    }
    
    private void PlaySwapLToR(RectTransform transform)
    {
        transform.position = swapL2RAnimation.transform.position;
        transform.SetParent(swapL2RAnimation.transform);
        swapL2RAnimation.DORestart();
    }
    
    private void PlaySwapRToL(RectTransform transform)
    {
        transform.position = swapR2LAnimation.transform.position;
        transform.SetParent(swapR2LAnimation.transform);
        swapR2LAnimation.DORestart();
    }


    [SerializeField] private RectTransform cardPre;
    private RectTransform instance1;
    private RectTransform instance2;
    [ContextMenu(nameof(TestAnis1))]
    public void TestAnis1()
    {
        instance1 = Instantiate(cardPre, Vector3.zero, quaternion.identity);
        instance2 = Instantiate(cardPre, Vector3.zero, quaternion.identity);
        PlayInUpLeft(instance1);
        PlayInUpRight(instance2);
    }
    [ContextMenu(nameof(TestAnis2))]
    public void TestAnis2()
    {
        PlayOutUpLeft(instance1);
        PlayOutUpRight(instance2);
        
        instance1 = Instantiate(cardPre, Vector3.zero, quaternion.identity);
        instance2 = Instantiate(cardPre, Vector3.zero, quaternion.identity);
        
        PlayInUpLeft(instance1);
        PlayInUpRight(instance2);
        
    }
    [ContextMenu(nameof(TestAnis3))]
    public void TestAnis3()
    {
       
        PlaySwapLToR(instance1);
        PlaySwapRToL(instance2);
    }
}