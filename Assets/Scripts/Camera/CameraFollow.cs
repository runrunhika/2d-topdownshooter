using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float boundX = 0.3f, boundY = 0.15f;
    //�ǐՔ͈�
    private Vector3 deltaPos;
    private float deltaX, deltaY;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //�J������Player��ǐՂ���֐�
    private void LateUpdate()
    {
        if (!player)
        {
            return;
        }

        deltaPos = Vector3.zero;

        //�ǂꂾ���APlayer�Ɨ���Ă��邩�擾
        deltaX = player.position.x - transform.position.x;
        deltaY = player.position.y - transform.position.y;

        /*  boundX�̒l�������ꂽ�ꍇ �J�������ǐՂ��n�߂� */
        if (deltaX > boundX || deltaX < -boundX) 
        {
            //Player���E�ɂ���ꍇ
            if (transform.position.x < player.position.x)
            {
                //��) 0.2 = 0.5 - 0.3  -> 0.2�ߊ��
                deltaPos.x = deltaX - boundX;
            }
            //��
            else
            {
                deltaPos.x = deltaX + boundX;
            }
        }

        if (deltaY > boundY || deltaY < -boundY)
        {
            //Player����ɂ���ꍇ
            if (transform.position.y < player.position.y)
            {
                deltaPos.y = deltaY - boundY;
            }
            //��
            else
            {
                deltaPos.y = deltaY + boundY;
            }
        }

        deltaPos.z = 0;

        //�ړ������Z
        transform.position += deltaPos;
    }
}
