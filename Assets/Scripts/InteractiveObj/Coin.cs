using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;
    public float rotateSpeed;
    public string respawnPoistionName;
    private LevelState _levelState;

    void Start()
    {
        _levelState = GameObject.FindAnyObjectByType<LevelState>();
    }

    void Update()
    {
        transform.Rotate(0,rotateSpeed,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        _levelState.presentCoins += coinValue;
        GameObject.FindGameObjectWithTag(respawnPoistionName).transform.position = transform.position;
        Destroy(gameObject);
    }
}
