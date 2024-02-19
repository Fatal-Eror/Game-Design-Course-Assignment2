using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class TimerScript : VisualElement
{
    public new class UxmlFactory : UxmlFactory<TimerScript, UxmlTraits> { }

    private readonly VisualTreeAsset _visualTree;
    private readonly VisualElement _InstanceTemplate;

    private float _startTime;
    private float _presentTime;
    private string _timerNumber;
    private bool isTiming;

    private LevelState _levelState;

    public TimerScript()
    {
        _levelState = GameObject.FindAnyObjectByType<LevelState>();
        // Bind start and end event
        _levelState.OnLevelStart += LevelStart;
        _levelState.OnLevelEnd += LevelEnd;
        _levelState.OnCoinChanged += UpdateCoinsInfo;

        // Forbid timing at first
        isTiming = false;

        // Load UI
        _visualTree = Resources.Load<VisualTreeAsset>("UI/Timer");
        _InstanceTemplate = _visualTree.CloneTree();
        Add(_InstanceTemplate);

        // Expand
        _InstanceTemplate.style.width = new StyleLength(Length.Percent(100));
        _InstanceTemplate.style.height = new StyleLength(Length.Percent(100));
    }

    private void UpdateCoinsInfo(int numOfCoins)
    {
        _InstanceTemplate.Q<Label>("NumOfCoins").text = numOfCoins.ToString() + " of " + _levelState.targetCoins.ToString(); 
    }

    public void LevelStart()
    {
        // Set Timer
        _startTime = Time.timeSinceLevelLoad;
        isTiming= true;
        _InstanceTemplate.Q<Label>("NumOfCoins").text = _levelState.presentCoins + " of " + _levelState.targetCoins;
    }

    public void LevelEnd()
    {
        // Stop Timer
        isTiming = false;

        // Set Best Score
        float score = _presentTime - _startTime;
        if(!PlayerPrefs.HasKey("Best"))
        {
            PlayerPrefs.SetFloat("Best", score);
        }

        if (score < PlayerPrefs.GetFloat("Best"))
        {
            PlayerPrefs.SetFloat("Best", score);
        }
    }

    public void UpdateTimer()
    {
        if (isTiming)
        {
            _presentTime = Time.timeSinceLevelLoad;
            _timerNumber = (_presentTime - _startTime).ToString("F2") + "Sec";
            _InstanceTemplate.Q<Label>("Timer").text = _timerNumber;
            /*int min = (int)(_presentTime - _startTime) / 60;
            int sec = (int)((_presentTime - _startTime) % 60);
            _timerNumber = $"{min:D2} : {sec:D2}";
            _InstanceTemplate.Q<Label>("Timer").text = _timerNumber;*/
        }
    }
}
