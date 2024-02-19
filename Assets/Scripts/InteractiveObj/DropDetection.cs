using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDetection : MonoBehaviour
{
    [SerializeField] private string respawnPosistionTag;
    [SerializeField] private string playerTag;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            other.transform.position = GameObject.FindGameObjectWithTag(respawnPosistionTag).transform.position;
        }
    }
}
