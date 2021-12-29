using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float boundX = 0.3f, boundY = 0.15f;
    //追跡範囲
    private Vector3 deltaPos;
    private float deltaX, deltaY;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //カメラがPlayerを追跡する関数
    private void LateUpdate()
    {
        if (!player)
        {
            return;
        }

        deltaPos = Vector3.zero;

        //どれだけ、Playerと離れているか取得
        deltaX = player.position.x - transform.position.x;
        deltaY = player.position.y - transform.position.y;

        /*  boundXの値よりも離れた場合 カメラが追跡を始める */
        if (deltaX > boundX || deltaX < -boundX) 
        {
            //Playerが右にいる場合
            if (transform.position.x < player.position.x)
            {
                //例) 0.2 = 0.5 - 0.3  -> 0.2近寄る
                deltaPos.x = deltaX - boundX;
            }
            //左
            else
            {
                deltaPos.x = deltaX + boundX;
            }
        }

        if (deltaY > boundY || deltaY < -boundY)
        {
            //Playerが上にいる場合
            if (transform.position.y < player.position.y)
            {
                deltaPos.y = deltaY - boundY;
            }
            //下
            else
            {
                deltaPos.y = deltaY + boundY;
            }
        }

        deltaPos.z = 0;

        //移動分加算
        transform.position += deltaPos;
    }
}
