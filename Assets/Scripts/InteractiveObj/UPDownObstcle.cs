using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPDownObstcle : MonoBehaviour
{
    public bool isMoveDown = false;
    public float targetMoveDistance = 5f;
    public float moveSpeed = 1;
    private int _moveDirection;
    private Vector3 _originalLocation;

    private void Awake()
    {
        _originalLocation = transform.position;
        _moveDirection = isMoveDown ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(transform.position.y - _originalLocation.y) > targetMoveDistance
            && Mathf.Sign(transform.position.y - _originalLocation.y) == Mathf.Sign(_moveDirection))
        {
            _moveDirection *= -1;
        }
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime * _moveDirection, 0);
    }
}
