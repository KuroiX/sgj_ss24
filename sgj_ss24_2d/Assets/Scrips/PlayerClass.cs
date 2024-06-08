using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerClass : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionMove;
    [SerializeField] private InputActionReference inputActionDash;
    [SerializeField] private BlobController blobController;
    private Card _card;
    
    private void Start()
    {
        inputActionMove.action.performed += ActionOnperformedMove;
        inputActionDash.action.performed += ActionOnperformedDash;
    }

    private void ActionOnperformedMove(InputAction.CallbackContext obj)
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
    
    private void ActionOnperformedDash(InputAction.CallbackContext obj)
    {
      blobController.Dash();
    }
    
    public void SetCard(Card newCard)
    {
        _card = newCard;
    }
}
