using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private List<Card> players;
    [SerializeField] private List<Stage> stages;
    [SerializeField] private TextMeshProUGUI timerText;
    private int _currentStage;
    private float _currentTime;

    public UnityEvent OnStageSwitch;

    private void Start()
    {
        _currentStage = 0;
        _currentTime = stages[_currentStage].time;
        UpdateTimer(_currentTime);
        SetPlayerCards(_currentStage);
        OnStageSwitch.AddListener(SwitchStage);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        UpdateTimer(_currentTime);
        if (_currentTime <= 0)
        {
            OnStageSwitch.Invoke();
        }
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
            players[i] = stageCards[i % stageCards.Count];
        }
    }

    [ContextMenu("Switch Stage")]
    public void SwitchStage()
    {
        if (_currentStage >= stages.Count - 1)
        {
            Debug.Log("YOU WIN");
            return;
        }
        
        _currentStage++;
        _currentTime = stages[_currentStage].time;
        SetPlayerCards(_currentStage);
    }
}
