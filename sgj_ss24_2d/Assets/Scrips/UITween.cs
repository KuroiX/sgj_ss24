using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class UITween : MonoBehaviour
{
    public float cardSpeed;
    public RectTransform p1Rec;
    public RectTransform p2Rec;
    public RectTransform resetRec;
    public RectTransform upDownCard;
    public RectTransform leftRightCard;
    public RectTransform upRightCard;
    public RectTransform downLeftCard;

    private RectTransform _currentP1Card;
    private RectTransform _currentP2Card;

    private void Start()
    {
        upDownCard.position = resetRec.position;
        leftRightCard.position = resetRec.position;
        upRightCard.position = resetRec.position;
        downLeftCard.position = resetRec.position;
    }

    [ContextMenu("A")]
    public void ASFD()
    {
        StartCoroutine(SetP1Card(upDownCard));
        StartCoroutine(SetP2Card(leftRightCard));
    }
    
    [ContextMenu("B")]
    public void ASFDC()
    {
        StartCoroutine(SetP1Card(downLeftCard));
        StartCoroutine(SetP2Card(upRightCard));
    }
    
    private IEnumerator SetP1Card(RectTransform card)
    {
        ResetCards();
        
        yield return new WaitForSeconds(cardSpeed);
        
        card.DOMove(p1Rec.position, cardSpeed).SetLoops(1).SetEase(Ease.OutCubic).SetAutoKill(true);
        _currentP1Card = card;
    }

    public IEnumerator SetP2Card(RectTransform card)
    {
        ResetCards();

        yield return new WaitForSeconds(cardSpeed);
        
        card.DOMove(p2Rec.position, cardSpeed).SetLoops(1).SetEase(Ease.OutCubic).SetAutoKill(true);
        _currentP2Card = card;
    }

    public void ResetCard(RectTransform card)
    {
        card.DOMove(resetRec.position, cardSpeed).SetLoops(1).SetEase(Ease.OutCubic).SetAutoKill(true);
    }

    public void ResetCards()
    {
        ResetCard(upDownCard);
        ResetCard(leftRightCard);
        ResetCard(upRightCard);
        ResetCard(downLeftCard);
    }

    [ContextMenu("SwapP1P2")]
    public void SwapP1andP2()
    {
        _currentP1Card.DOMove(p2Rec.position, cardSpeed).SetLoops(1).SetEase(Ease.OutCubic).SetAutoKill(true);
        _currentP2Card.DOMove(p1Rec.position, cardSpeed).SetLoops(1).SetEase(Ease.OutCubic).SetAutoKill(true);

        (_currentP1Card, _currentP2Card) = (_currentP2Card, _currentP1Card);
    }
}
