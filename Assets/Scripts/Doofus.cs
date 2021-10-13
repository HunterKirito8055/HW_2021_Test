using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doofus : MonoBehaviour
{
    public Rigidbody myBody;
    public float speed = 8f;
    public LayerMask layerMask;
    #region Private Variables
    Vector3 newPos;
    bool isOnGround;
    float fallTime = 0;
    #endregion
    private void FixedUpdate()
    {
        if (GameManager.instance.IsGameOver)
        {
            return;
        }
        newPos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        myBody.MovePosition(myBody.position + newPos * Time.fixedDeltaTime * speed);

        isOnGround = Physics.Raycast(transform.position, Vector3.down * 1f, 1f, layerMask);
        if (!isOnGround)
        {
            fallTime += Time.fixedDeltaTime;
            if (fallTime > 1f)
            {
                fallTime = 0;
                GameManager.instance.GameOver();
            }
        }
        else
        {
            fallTime = 0;
        }

    }
}
