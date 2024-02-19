using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUIScript : MonoBehaviour
{
    private VisualElement root;
    private LevelState _levelState;

    private void Awake()
    {
        _levelState = GameObject.FindAnyObjectByType<LevelState>();
        // Bind start and end event
        _levelState.OnLevelStart += LevelStart;
        _levelState.OnLevelEnd += LevelEnd;
    }

    private void LevelEnd()
    {
        
    }

    private void LevelStart()
    {
        
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    void Update()
    {
        root.Q<TimerScript>().UpdateTimer();
        
    }
}
