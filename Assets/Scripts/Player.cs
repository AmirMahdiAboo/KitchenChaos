using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 10f;

    private bool _isWalking;
    private void Update()
    {
        Vector2 inputvector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputvector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputvector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputvector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputvector.x = +1;
        }

        inputvector = inputvector.normalized;
        Vector3 moveDir = new Vector3(inputvector.x, 0, inputvector.y);
        transform.position += moveDir * Time.deltaTime * _moveSpeed;

        _isWalking = (moveDir != Vector3.zero);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotationSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
