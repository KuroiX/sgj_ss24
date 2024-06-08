using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerClass : MonoBehaviour
{
    public GameObject blobPrefab;
    [SerializeField] private BlobController blobController;
    [SerializeField] private Card _card;
    [SerializeField] private bool isTutorial;
    private bool started;
    
    private void Awake()
    {
        blobController = FindObjectOfType<BlobController>();
        
        if (isTutorial)
        {
            //BlobController[] blobControllers = FindObjectsOfType<BlobController>();
            blobController = Instantiate(blobPrefab).GetComponent<BlobController>();
            if (FindObjectOfType<PlayerInputManager>().playerCount == 1)
            {
                _card = Instantiate(_card);
                _card.cardActions = new List<CardAction>() {CardAction.Up, CardAction.Down};
                //blobController = blobControllers[0];
                
                SceneManager.sceneLoaded += (arg0, mode) =>
                {
                    isTutorial = false;
                    blobController = FindObjectOfType<BlobController>();
                };
                
                DontDestroyOnLoad(gameObject);
            }
            
            if (FindObjectOfType<PlayerInputManager>().playerCount == 2)
            {
                _card = Instantiate(_card);
                _card.cardActions = new List<CardAction>() {CardAction.Left, CardAction.Right};
                //blobController = blobControllers[1];
                
                SceneManager.sceneLoaded += (arg0, mode) =>
                {
                    isTutorial = false;
                    blobController = FindObjectOfType<BlobController>();
                };
                
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public void StartGame(InputAction.CallbackContext obj)
    {
        if (started) return;
        
        started = true;    
        if(!isTutorial)
            FindObjectOfType<GameLoop>().StartGameLoop();
        else
        {
            blobController.StartBlob(); 
            blobController.StopShrinking();
        }
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
    
    public void ActionOnperformedDash(InputAction.CallbackContext obj)
    {
      blobController.Dash();
    }
    
    public void SetCard(Card newCard)
    {
        _card = newCard;
    }
}
