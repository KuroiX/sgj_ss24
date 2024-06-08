using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
}

public class GameLoop : MonoBehaviour
{
    //TODO change class
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private List<PlayerClass> players;
    [SerializeField] private List<Stage> stages;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PlayerInputManager playerInputManager;
    private int _currentStage;
    private float _currentTime;
    private bool _gameLoopStarted;

    public UnityEvent OnGameLoopStarted;
    public UnityEvent OnStageSwitch;

    private void Start()
    {
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        _gameLoopStarted = false;
    }

    public void StartGameLoop()
    {
        _currentStage = 0;
        _currentTime = stages[_currentStage].time;
        UpdateTimer(_currentTime);
        SetPlayerCards(_currentStage);
        OnStageSwitch.AddListener(SwitchStage);
        _gameLoopStarted = true;
        OnGameLoopStarted.Invoke();
    }

    private void OnPlayerJoined(PlayerInput obj)
    {
        players.Add(obj.gameObject.GetComponent<PlayerClass>());
        if(players.Count >= 2) 
            ShowStartButton();
    }

    private void ShowStartButton()
    {
        Debug.Log("TODO: nice asset");
    }

    private void Update()
    {
        if(!_gameLoopStarted) return;
        
        _currentTime -= Time.deltaTime;
        UpdateTimer(_currentTime);
        if (_currentTime <= 0)
        {
            OnStageSwitch.Invoke();
        }
    }

    private void GameOver()
    {
        
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

    [ContextMenu("Switch Stage")]
    public void SwitchStage()
    {
        if (_currentStage >= stages.Count - 1)
        {
            Debug.Log("YOU WIN");
            FindObjectOfType<SceneLoader>().LoadNextScene();
            return;
        }
        
        _currentStage++;
        _currentTime = stages[_currentStage].time;
        SetPlayerCards(_currentStage);
    }

    [ContextMenu("TriggerCutscene")]
    public void TriggerCutscene()
    {
        StartCoroutine(CutsceneEnumerator());
    }

    private IEnumerator CutsceneEnumerator()
    {
        vCam.Priority = 10;
        yield return new WaitForSeconds(10);
        //trigger cutscene
        //disable character input;
        vCam.Priority = 0;
    }
}
