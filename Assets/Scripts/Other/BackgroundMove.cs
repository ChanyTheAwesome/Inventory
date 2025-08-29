using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{//그냥 뒷 배경 조금씩 움직여주는 친구에요.
    private bool _isMovingRight = true;
    private void Update()
    {
        this.transform.position = _isMovingRight ? new Vector3(this.transform.position.x+0.0002f, this.transform.position.y, this.transform.position.z) : 
            new Vector3(this.transform.position.x - 0.0002f, this.transform.position.y, this.transform.position.z);
        //아주 느린 속도로 배경이 움직이고,
        switch (this.transform.position.x)
        {//값을 확인해 오른쪽으로 움직일지, 왼쪽으로 움직일지 결정합니다.
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
