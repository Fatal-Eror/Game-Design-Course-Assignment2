using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPosition : MonoBehaviour
{
    private LevelState _levelState;
    private void Awake()
    {
        _levelState = GameObject.FindAnyObjectByType<LevelState>(); 
        _levelState.OnLevelStart += LevelStartSetup;
    }

    private void LevelStartSetup()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
    }
}
