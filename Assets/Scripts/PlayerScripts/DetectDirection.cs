using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDirection : MonoBehaviour
{
    public float viewRadius = 5f;
    [Range (0, 360)]
    public float viewAngle;

    [HideInInspector]
    private Vector3 rayDirection;

    private Vector2 screenBounds;
    private string[] tabDirection = {"up", "down", "left", "right"};

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        // if (direction == 0)
        //     rayDirection = transform.right;
        // else if (direction == 1)
        //     rayDirection = -transform.right;
        // else if (direction == 2)
        //     rayDirection = transform.up;
        // else if (direction == 3)
        //     rayDirection = -transform.up;
        // StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    // IEnumerator FindTargetsWithDelay(float delay)
    // {
    //     while (true) {
    //         yield return new WaitForSeconds(delay);
    //         FindVisibleTarget();
    //     }
    // }

    private void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (screenBounds.x - this.transform.position.x < 6f && (this.gameObject.CompareTag("Enemy") == false))
            viewRadius = screenBounds.x - this.transform.position.x;
        else
            viewRadius = 6f;
        GetMouseDirection();
    }

    public string GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 dirToMousePos = (mousePosition - transform.position);
        for (int i = 0; i < tabDirection.Length; i += 1) {
            if (tabDirection[i] == "up")
                rayDirection = transform.up;
            else if (tabDirection[i] == "down")
                rayDirection = -transform.up;
            else if (tabDirection[i] == "left")
                rayDirection = -transform.right;
            else if (tabDirection[i] == "right")
                rayDirection = transform.right;
            // Debug.Log("Mouse position: " + mousePosition);
            // Debug.Log("Player position" + transform.position);
            if (Vector3.Angle(rayDirection, dirToMousePos) < viewAngle / 2) {
                return tabDirection[i];
                // Debug.Log("Direction: " + tabDirection[i]);
            }
        }
        return null;
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    } 
}
