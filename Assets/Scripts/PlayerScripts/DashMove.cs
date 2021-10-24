using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int isDashing;
    private Vector2 direction;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    void Update()
    {
        if (isDashing == 1) {
            if (dashTime <= 0) {
                isDashing = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
            } else {
                dashTime -= Time.deltaTime;
                Debug.Log(direction);
                rigidBody.velocity = direction * dashSpeed;
            }
        }
    }

    public void Dash() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // Need to dash in current direction if moveInput == (0.0, 0.0)
        isDashing = 1;
        // Debug.Log("direction: " + moveInput);
        direction = moveInput.normalized;
        // playerAnimator.SetDashAnimation(currentDirection);
    }

}
