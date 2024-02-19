using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationScript : MonoBehaviour
{
    private LevelState levelState;
    [SerializeField] private string playerTag;
    private void Awake()
    {
        levelState = GameObject.FindAnyObjectByType<LevelState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag) && levelState.presentCoins >= levelState.targetCoins)
        {
            levelState.GameFinish();
        }
    }
}
