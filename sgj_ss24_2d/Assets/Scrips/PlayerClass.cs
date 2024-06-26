using System;
using System.Collections;
using System.Collections.Generic;
using IntroCutScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerClass : MonoBehaviour
{
    public GameObject blobPrefab;
    [SerializeField] private BlobController blobController;
    [SerializeField] private Card _card;
    [SerializeField] private bool isTutorial;
    private bool started;

    [SerializeField] private bool singlePlayerOne;
    [SerializeField] private bool singlePlayerTwo;
    

    private void Awake()
    {
        blobController = FindObjectOfType<BlobController>();

        if (isTutorial)
        {

            bool isSinglePlayerGame = singlePlayerOne || singlePlayerTwo;
            //BlobController[] blobControllers = FindObjectsOfType<BlobController>();
            StartCoroutine(StartLobbyMovement());

            if (singlePlayerOne || (!isSinglePlayerGame && FindObjectOfType<PlayerInputManager>().playerCount == 1))
            {
                blobController = Instantiate(blobPrefab, new Vector3(-4f, -4f, 0f), Quaternion.identity)
                    .GetComponent<BlobController>();
                blobController.AddComponent<DetectDropMerge>();
                blobController.GetComponents<CircleCollider2D>()[1].enabled = true;

                _card = Instantiate(_card);
                _card.cardActions = new List<CardAction>()
                    {CardAction.Up, CardAction.Down, CardAction.UpD, CardAction.DownD};
                //blobController = blobControllers[0];

                SceneManager.sceneLoaded += Subscriber;

                DontDestroyOnLoad(gameObject);
            }

            if (singlePlayerTwo || (!isSinglePlayerGame && FindObjectOfType<PlayerInputManager>().playerCount == 2))
            {
                blobController = Instantiate(blobPrefab, new Vector3(4f, 2.5f, 0f), Quaternion.identity)
                    .GetComponent<BlobController>();
                _card = Instantiate(_card);
                _card.cardActions = new List<CardAction>()
                    {CardAction.Left, CardAction.Right, CardAction.LeftD, CardAction.RightD};
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
        if (arg0.buildIndex == 2) return; 
        
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
        if (!isTutorial)
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
                    if (input.y >= 0)
                    {
                        blobController.SetUp(input.y > schranke ? input.y : 0);
                        if (input.y > schranke)
                            VibratorGoBrrrrr(input.y);
                        else
                            VibrationGOAwai();
                    }
                  

                    break;
                case CardAction.Down:
                    if (input.y < 0)
                    {
                        blobController.SetDown(input.y < -schranke ? input.y : 0);
                        if (input.y < -schranke)
                            VibratorGoBrrrrr(input.y);
                        else
                            VibrationGOAwai();
                    }
                   

                    break;
                case CardAction.Right:
                    if (input.x >= 0)
                    {
                        blobController.SetRight(input.x > schranke ? input.x : 0);
                        if (input.x > schranke)
                        {
                            VibratorGoBrrrrr(input.x);
                        }
                        else
                        {
                            VibrationGOAwai();
                        }
                    }
                    

                    break;
                case CardAction.Left:
                    if (input.x < 0)
                    {
                        blobController.SetLeft(input.x < -schranke ? input.x : 0);
                        if (input.x < -schranke)
                        {
                            VibratorGoBrrrrr(input.x);
                        }
                        else
                        {
                            VibrationGOAwai();
                        }
                    }
                    

                    break;
            }
        }
    }


    public void ActionOnperformedDashE(InputAction.CallbackContext obj)
    {
        foreach (var cardAction in _card.cardActions)
        {
            if (cardAction == CardAction.RightD)
            {
                blobController.Dash(Vector2.right);
                VibratorGoBrrrrr(10f);
                //Invoke(nameof(VibrationGOAwai), 0.1f);
            }
        }
    }

    public void ActionOnperformedDashN(InputAction.CallbackContext obj)
    {
        Debug.Log("dash up");
        foreach (var cardAction in _card.cardActions)
        {
            if (cardAction == CardAction.UpD)
            {
                blobController.Dash(Vector2.up);
                VibratorGoBrrrrr(10f);
            }
        }
    }

    public void ActionOnperformedDashS(InputAction.CallbackContext obj)
    {
        Debug.Log("dash south");
        foreach (var cardAction in _card.cardActions)
        {
            if (cardAction == CardAction.DownD)
            {
                blobController.Dash(Vector2.down);
                VibratorGoBrrrrr(10f);
            }
        }
    }

    public void ActionOnperformedDashW(InputAction.CallbackContext obj)
    {
        Debug.Log("dash left");
        foreach (var cardAction in _card.cardActions)
        {
            if (cardAction == CardAction.LeftD)
            {
                blobController.Dash(Vector2.left);
                VibratorGoBrrrrr(10f);
            }
        }
    }

    public void SetCard(Card newCard)
    {
        _card = newCard;
    }

    public float lowFdiv;
    public float hightFdiv;

    public void VibratorGoBrrrrr(float strangth)
    {
        strangth = Mathf.Clamp01(Mathf.Abs(strangth));
        GetComponent<PlayerInput>()?.GetDevice<Gamepad>()?.SetMotorSpeeds(0, 0.02f * strangth);
    }

    private void VibrationGOAwai()
    {
        GetComponent<PlayerInput>()?.GetDevice<Gamepad>()?.SetMotorSpeeds(0, 0);
    }
}