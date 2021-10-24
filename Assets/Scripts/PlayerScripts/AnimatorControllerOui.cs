using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerOui : MonoBehaviour
{
    public Animator playerAnimator;

    public void SetPlayerMovementAnimation(Direction direction, Vector2 moveInput)
    {
        if (moveInput.Equals(new Vector2(0, 0)))
        {
            SetDirectionBool(false, false, false);
        }
        else if ((direction == Direction.LEFT ||
            direction == Direction.RIGHT) && IsIdle())
        {
            SetDirectionBool(true, false, false);
        }
        else if ((direction == Direction.UP) && IsIdle())
        {
            SetDirectionBool(false, false, true);
        }
        else if ((direction == Direction.DOWN) && IsIdle())
        {
            SetDirectionBool(false, true, false);
        }
    }

    private void SetDirectionBool(bool isSideRun, bool isFrontRun, bool isBackRun) {
        playerAnimator.SetBool("isRunning", isSideRun);
        playerAnimator.SetBool("isRunningFront", isFrontRun);
        playerAnimator.SetBool("isRunningBack", isBackRun);
    }

    private bool IsIdle()
    {
        return !(playerAnimator.GetBool("isRunning") && playerAnimator.GetBool("isRunningFront") && playerAnimator.GetBool("isRunningBack"));
    }

    public void SetAutoAttack(bool side, bool front, bool back)
    {
        if (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).Equals(new Vector2(0, 0))) {
            playerAnimator.SetBool("AttackSideMoving", false);
            playerAnimator.SetBool("AttackFrontMoving", false);
            playerAnimator.SetBool("AttackBackMoving", false);


            playerAnimator.SetBool("AttackSide", side);
            playerAnimator.SetBool("AttackFront", front);
            playerAnimator.SetBool("AttackBack", back);
        } else {
            playerAnimator.SetBool("AttackSide", false);
            playerAnimator.SetBool("AttackFront", false);
            playerAnimator.SetBool("AttackBack", false);

            playerAnimator.SetBool("AttackSideMoving", side);
            playerAnimator.SetBool("AttackFrontMoving", front);
            playerAnimator.SetBool("AttackBackMoving", back);
        }
        // if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ninja_Auto"))
        //     Debug.Log("SIDE Length: " + playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        // else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ninja_Auto_Front"))
        //     Debug.Log("FRONT Length: " + playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        // else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ninja_Auto_Back"))
        //     Debug.Log("BACK Length: " + playerAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    public void SetDashAnimation(Direction currentDirection)
    {
        if (currentDirection == Direction.RIGHT
            || currentDirection == Direction.LEFT)
            playerAnimator.SetTrigger("DashTrigger");
        else if (currentDirection == Direction.DOWN)
            playerAnimator.SetTrigger("DashFrontTrigger");
        else if (currentDirection == Direction.UP)
            playerAnimator.SetTrigger("DashBackTrigger");
    }

    public void SetHitAnimation()
    {
        playerAnimator.SetTrigger("Hit");
    }

    public void SetDeathAnimation()
    {
        playerAnimator.SetBool("IsDead", true);
    }

}
