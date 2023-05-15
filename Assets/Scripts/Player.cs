using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private GameInput _gameInput;
    private bool _isWalking;
    private void Update()
    {
       Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * Time.deltaTime * _moveSpeed;

        _isWalking = (moveDir != Vector3.zero);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotationSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
