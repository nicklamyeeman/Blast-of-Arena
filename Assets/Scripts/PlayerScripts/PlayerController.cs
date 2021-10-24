using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum Direction {UP, DOWN, LEFT, RIGHT};

public class PlayerController : MonoBehaviourPun
{
    public float speed;
    private Vector2 velocity;
    private Rigidbody2D rigidBody;
    private HealthManager healthmanager;
    private AnimatorControllerOui playerAnimatorController;
    private bool facingLeft = true;
    public Transform spriteTransform;
    private bool isAttacking = false;

    public Direction currentDirection = Direction.LEFT;

    private void Awake()
    {
    }

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        healthmanager = GetComponent<HealthManager>();
        playerAnimatorController = GetComponent<AnimatorControllerOui>();
    }

    public void SetIsAttacking(bool _isAttacking)
    {
        isAttacking = _isAttacking;
    }

    public void SetPlayerSpeed(float _speed)
    {
        speed = _speed;
    }

    private Vector2 GetMoveInput()
    {
        return (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    public void SetCurrentDirection(Direction direction)
    {
        // Debug.Log("Current Direction: " + currentDirection);
        // Debug.Log("Target Direction: " + direction);
        Vector2 moveInput = GetMoveInput();
        if (((currentDirection != Direction.LEFT) 
            && (direction == Direction.LEFT) && !facingLeft) ||
            ((currentDirection != Direction.RIGHT)
            && (direction == Direction.RIGHT) && facingLeft)) {
            // Debug.Log("Flip the sprite");
            Flip();
            }
        currentDirection = direction;
        // playerAnimatorController.SetPlayerAnimation(currentDirection, moveInput, isAttacking);
        // playerAnimatorController.SetSideAutoAttackAnimation(true);
        playerAnimatorController.SetPlayerMovementAnimation(currentDirection, moveInput);
    }

    public void Update()
    {
        if (base.photonView.IsMine)
        {
            Vector2 moveInput = GetMoveInput();

            if (moveInput.Equals(new Vector2(0, 0)) && facingLeft)
                currentDirection = Direction.LEFT;
            else if (moveInput.Equals(new Vector2(0, 0)) && !facingLeft)
                currentDirection = Direction.RIGHT;

            // Debug.Log("Current Direction: " + currentDirection);
            velocity = moveInput.normalized * speed;
            if (isAttacking == false) {
                DetectDirection();
                playerAnimatorController.SetPlayerMovementAnimation(currentDirection, moveInput);
            }
        }
    }

    private void DetectDirection()
    {
        if (healthmanager.IsPlayerDead())
            return;
        if (Input.GetKey(KeyCode.R))
        {
            CinemachineManager.Instance.ShakeCamera(10f, .2f);
            playerAnimatorController.SetHitAnimation();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            CinemachineManager.Instance.SpectateMode("Player");
        }


        if (Input.GetKey(KeyCode.Z)) {
            currentDirection = Direction.UP;
            if (Input.GetKey(KeyCode.Q))
                currentDirection = Direction.LEFT;
            if (Input.GetKey(KeyCode.D))
                currentDirection = Direction.RIGHT;
        }

        if (Input.GetKey(KeyCode.S)) {
            currentDirection = Direction.DOWN;
            if (Input.GetKey(KeyCode.Q))
                currentDirection = Direction.LEFT;
            if (Input.GetKey(KeyCode.D))
                currentDirection = Direction.RIGHT;
        }

        if (Input.GetKey(KeyCode.D)) {
            currentDirection = Direction.RIGHT;
            if (HasChangedDirection() == true)
                Flip();
        }

        if (Input.GetKey(KeyCode.Q)) {
            currentDirection = Direction.LEFT;
            if (HasChangedDirection() == true)
                Flip();
        }
    }

    private void FixedUpdate() {
        if (healthmanager.IsPlayerDead() == false)
            rigidBody.position += velocity * Time.deltaTime;
    }

    private bool HasChangedDirection()
    {
        if (((Input.GetKey(KeyCode.Q)) && !facingLeft) ||
            ((Input.GetKey(KeyCode.D)) && facingLeft))
            return true;
        return false;
    }

    private void Flip()
    {
        // Debug.Log("Flip");
        facingLeft = !facingLeft;
        Vector3 theScale = spriteTransform.localScale;
        theScale.x *= -1;
        spriteTransform.localScale = theScale;
    }
}
