using System;
using System.Collections;
using System.Collections.Generic;
using IntroCutScene;
using Unity.VisualScripting;
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
            StartCoroutine(StartLobbyMovement());
            
            if (FindObjectOfType<PlayerInputManager>().playerCount == 1)
            {
                blobController = Instantiate(blobPrefab, new Vector3(-4f, -4f, 0f), Quaternion.identity).GetComponent<BlobController>();
                blobController.AddComponent<DetectDropMerge>();
                blobController.GetComponents<CircleCollider2D>()[1].enabled = true;
                
                _card = Instantiate(_card);
                _card.cardActions = new List<CardAction>() {CardAction.Up, CardAction.Down};
                //blobController = blobControllers[0];

                SceneManager.sceneLoaded += Subscriber;
                
                DontDestroyOnLoad(gameObject);
            }
            
            if (FindObjectOfType<PlayerInputManager>().playerCount == 2)
            {
                blobController = Instantiate(blobPrefab, new Vector3(4f, 2.5f, 0f), Quaternion.identity).GetComponent<BlobController>();
                _card = Instantiate(_card);
                _card.cardActions = new List<CardAction>() {CardAction.Left, CardAction.Right};
                //blobController = blobControllers[1];

                SceneManager.sceneLoaded += Subscriber;

                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private IEnumerator StartLobbyMovement()
    {
        yield return null;
        
        blobController.StartBlob(); 
        blobController.StopShrinking();
    }

    private void Subscriber(Scene arg0, LoadSceneMode arg1)
    {
        isTutorial = false;
        blobController = FindObjectOfType<BlobController>();

        if (arg0.buildIndex == 0)
        {
            Unsubscriber();
        }
    }

    private void Unsubscriber()
    {
        SceneManager.sceneLoaded -= Subscriber;
        Destroy(gameObject);
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
       Debug.Log("move");
       Vector2 input = obj.ReadValue<Vector2>();

       float schranke = 0.1f;

       foreach (var cardAction in _card.cardActions)
       {
           switch (cardAction)
           {
               case CardAction.Up:
                   if(input.y >= 0)
                       blobController.SetUp(input.y > schranke ? input.y : 0);
                   break;
               case CardAction.Down:
                   if(input.y < 0)
                       blobController.SetDown(input.y < -schranke ? input.y : 0);
                   break;
               case CardAction.Right:
                   if(input.x >= 0)
                       blobController.SetRight(input.x > schranke ? input.x : 0);
                   break;
               case CardAction.Left:
                   if(input.x < 0)
                       blobController.SetLeft(input.x < -schranke ? input.x : 0);
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
