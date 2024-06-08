using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerClass : MonoBehaviour
{
    [SerializeField] private BlobController blobController;
    [SerializeField] private Card _card;

    private static bool started;
    
    private void Awake()
    {
        blobController = FindObjectOfType<BlobController>();
    }

    public void StartGame(InputAction.CallbackContext obj)
    {
        if (started) return;
        
        started = true;    
        FindObjectOfType<GameLoop>().StartGameLoop();
    }

    public void ActionOnperformedMove(InputAction.CallbackContext obj)
    {
       Vector2 input = obj.ReadValue<Vector2>();

       foreach (var cardAction in _card.cardActions)
       {
           switch (cardAction)
           {
               case CardAction.Up:
                   if(input.y >= 0)
                       blobController.SetUp(input.y);
                   break;
               case CardAction.Down:
                   if(input.y < 0)
                       blobController.SetDown(input.y);
                   break;
               case CardAction.Right:
                   if(input.x >= 0)
                       blobController.SetRight(input.x);
                   break;
               case CardAction.Left:
                   if(input.x < 0)
                       blobController.SetLeft(input.x);
                   break;
           }
       }
    }
    
    public void ActionOnperformedDashE(InputAction.CallbackContext obj)
    {
        foreach (var cardAction in _card.cardActions)
        {
            if(cardAction == CardAction.RightD)
                blobController.Dash(Vector2.right);
        }
    }
    
    public void ActionOnperformedDashN(InputAction.CallbackContext obj)
    {
        foreach (var cardAction in _card.cardActions)
        {
            if(cardAction == CardAction.UpD)
                blobController.Dash(Vector2.up);
        }
    }
    
    public void ActionOnperformedDashS(InputAction.CallbackContext obj)
    {
        foreach (var cardAction in _card.cardActions)
        {
            if(cardAction == CardAction.DownD)
                blobController.Dash(Vector2.down);
        }
    }
    public void ActionOnperformedDashW(InputAction.CallbackContext obj)
    {
        foreach (var cardAction in _card.cardActions)
        {
            if(cardAction == CardAction.LeftD)
                blobController.Dash(Vector2.left);
        }
    }
    
    public void SetCard(Card newCard)
    {
        _card = newCard;
    }
}
