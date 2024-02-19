using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstcle : MonoBehaviour
{
    public bool isMoveLeft = false;
    public float targetMoveDistance = 10f;
    public float moveSpeed = 1;
    private int _moveDirection;
    private Vector3 _originalLocation;

    private void Awake()
    {
        _originalLocation= transform.position;
        _moveDirection = isMoveLeft? -1: 1;
    }

    // Update is called once per frame
    void Update()
    {
            
        if (Mathf.Abs(transform.position.x - _originalLocation.x) > targetMoveDistance
            && Mathf.Sign(transform.position.x - _originalLocation.x) == Mathf.Sign(_moveDirection))
        {
            _moveDirection *= -1;
            // transform.position = new Vector3(_originalLocation.x + (targetMoveDistance - 0.1f) * _moveDirection, 0, 0);
        }
        transform.position += new Vector3(moveSpeed * Time.deltaTime * _moveDirection, 0, 0);
    }
}
