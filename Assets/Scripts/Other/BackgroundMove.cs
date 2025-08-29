using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private bool isMovingRight = true;
    private void Update()
    {
        if(isMovingRight == true)
        {
            this.transform.position = new Vector3(this.transform.position.x+0.0002f, this.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x - 0.0002f, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x >= 2.5f)
        {
            isMovingRight = false;
        }
        else if (this.transform.position.x <= -2.5f)
        {
            isMovingRight = true;
        }
    }
}
