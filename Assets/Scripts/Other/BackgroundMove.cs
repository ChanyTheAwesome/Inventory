using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private bool _isMovingRight = true;
    private void Update()
    {
        this.transform.position = _isMovingRight ? new Vector3(this.transform.position.x+0.0002f, this.transform.position.y, this.transform.position.z) : 
            new Vector3(this.transform.position.x - 0.0002f, this.transform.position.y, this.transform.position.z);
        switch (this.transform.position.x)
        {
            case >= 2.5f:
                _isMovingRight = false;
                break;
            case <= -2.5f:
                _isMovingRight = true;
                break;
            default:
                break;
        }
    }
}
