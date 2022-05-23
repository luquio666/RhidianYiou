using System;
using UnityEngine;

public class DirtyPlayerController : MonoBehaviour
{
    private float _movementSpeed = 4f; // Cuanto tarda en ir de un tile a otro en segundos

    // Delta time: Es el tiempo que transcurre entre frame y frame

    private float _progress = 0;

    private bool _isMoving = false;

    private Vector3 _targetMovement = Vector3.zero;

    private Vector3 _startPos = Vector3.zero;

    // Input
    public Vector3 GetMovementInput()
    {
        // (0,0) no movement
        // (1,0) right
        // (-1,0) left
        // (0,1) up
        // (0,-1) down

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }

    // Position
    public void UpdatePosition(Vector2 movementInput, float deltaTime)
    {
        if (!_isMoving && movementInput != Vector2.zero && CanMove(movementInput))
        {
            _isMoving = true;
            _startPos = transform.position;
            _progress = 0;

            if (movementInput.x != 0) // Horizontal Movement
            {
                _targetMovement = new Vector3(_startPos.x + movementInput.x, _startPos.y);
            }
            else if (movementInput.y != 0) // Vertical Movement
            {
                _targetMovement = new Vector3(_startPos.x, _startPos.y + movementInput.y);
            }
        }

        if (_isMoving)
        {
            _progress += _movementSpeed * deltaTime;
            if (_progress > 1)
            {
                _progress = 1;
            }

            transform.position = Vector3.Lerp(_startPos, _targetMovement, _progress);

            if (_progress >= 1)
            {
                _isMoving = false;
                _targetMovement = Vector3.zero;
            }
        }
    }

    // Graphics
    // TODO Manage and handle sprite animations

    private void Update()
    {
        float dt = Time.deltaTime;

        // Leo input
        Vector2 movementInput = GetMovementInput();

        // Actualizo posicion
        UpdatePosition(movementInput, dt);

        // Actualizo animacion
        // TODO update animations
    }

    private bool CanMove(Vector2 movementInput)
    {
        bool canMove = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementInput, 1);

        canMove = hit.collider == null;

        return canMove;
    }
}