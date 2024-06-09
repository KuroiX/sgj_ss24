using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Dialogue;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[Serializable]
public class Stage
{
    public List<Card> cards;
    public int time;
    public UnityEvent OnStageEnter;

    public VoiceLine voiceLine;

    public void TriggerStageEnter()
    {
        OnStageEnter.Invoke();
    }
}

public class GameLoop : MonoBehaviour
{
    public UnityEvent StopEverything;
    public UnityEvent StartEverything;

    public DialogueManager dialogueManager;

    public CardUi cardUi;
    
    //TODO change class
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private List<PlayerClass> players;
    [SerializeField] private List<Stage> stages;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PlayerInputManager playerInputManager;
    public int cutsceneTime;
    private int _currentStage;
    private float _currentTime;
    private bool _gameLoopStarted;

    public UnityEvent OnGameLoopStarted;
    public UnityEvent OnStageSwitch;

    private void Start()
    {
        players = FindObjectsOfType<PlayerClass>().Reverse().ToList();
        //playerInputManager.onPlayerJoined += OnPlayerJoined;
        _gameLoopStarted = false;
    }

    public void StartGameLoop()
    {
        Debug.Log(" start before ");
        if (players.Count < 2) return;
        
        Debug.Log("start");
        _currentStage = 0;
        EnterStage();
        UpdateTimer(_currentTime);
        SetPlayerCards(_currentStage);
        OnStageSwitch.AddListener(SwitchStage);
        _gameLoopStarted = true;
        OnGameLoopStarted.Invoke();
    }

    private void EnterStage()
    {
        // TODO
        cardUi.CreateNewUICards(stages[_currentStage].cards[0].prefabTransform, stages[_currentStage].cards[1].prefabTransform);
        _currentTime = stages[_currentStage].time;
        stages[_currentStage].TriggerStageEnter();
        if (stages[_currentStage].voiceLine is not null)
        {
            dialogueManager.ShowVoiceLine(stages[_currentStage].voiceLine);
        }
        SetPlayerCards(_currentStage);
    }

    private void OnPlayerJoined(PlayerInput obj)
    {
        players.Add(obj.gameObject.GetComponent<PlayerClass>());
        if (players.Count >= 2)
        {
            //StartGameLoop();
            ShowStartButton();
        }
    }

    private void ShowStartButton()
    {
        Debug.Log("TODO: nice asset");
    }

    private bool paused;
    
    private void Update()
    {
        if(!_gameLoopStarted || gameOver || paused) return;
        
        _currentTime -= Time.deltaTime;
        UpdateTimer(_currentTime);
        if (_currentTime <= 0)
        {
            OnStageSwitch.Invoke();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        FindObjectOfType<WinLoosUi>().DisplayLoosUI();
        StartCoroutine(GoToMainMenu());
    }

    private void UpdateTimer(float time)
    {
        timerText.text = "Time: " + (int)time;
    }

    private void SetPlayerCards(int stage)
    {
        List<Card> stageCards = stages[stage].cards;
        
        for(int i = 0; i < players.Count; i++)
        {
            players[i].SetCard(stageCards[i % stageCards.Count]);
        }
    }

    private bool gameOver;

    [ContextMenu("Switch Stage")]
    public void SwitchStage()
    {
        if (_currentStage >= stages.Count - 1 && !gameOver)
        {
            gameOver = true;
            Debug.Log("YOU WIN");
            FindObjectOfType<WinLoosUi>().DisplayWinnUI();
            StartCoroutine(GoToMainMenu());
            return;
        }
        
        _currentStage++;
        //EnterStage();
        TriggerCutscene();
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(5);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

    public void TriggerCutscene()
    {
        StartCoroutine(CutsceneEnumerator());
    }

    private IEnumerator CutsceneEnumerator()
    {
        vCam.Priority = 10;
        
        paused = true;
        StopEverything.Invoke();
        
        yield return new WaitForSeconds(cutsceneTime);
        
        StartEverything.Invoke();
        paused = false;
        
        //trigger cutscene
        //disable character input;
        vCam.Priority = 0;
        EnterStage();
    }
}
