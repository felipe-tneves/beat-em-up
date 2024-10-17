using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _playerRigidbody2D;
    public float _playerSpedd;
    public Vector2 _playerDirection;

    private Animator _playerAnimator;
    private bool _playerFaceRight = true;
    private bool _isWalk;


    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (_playerDirection.x != 0 || _playerDirection.y != 0)
        {
            _isWalk = true;
        }
        else
        {
            _isWalk = false;
        }

        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerDirection.normalized * _playerSpedd * Time.fixedDeltaTime);
    }

    void PlayerMove()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(_playerDirection.x < 0 && _playerFaceRight)
        {
            Flip();
        }
        else if(_playerDirection.x > 0 && !_playerFaceRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        _playerAnimator.SetBool("isWalk", _isWalk); //true ou false 
    }

    void Flip()
    {
        _playerFaceRight = !_playerFaceRight;
        transform.Rotate(0f, 180, 0f);
    }
}
