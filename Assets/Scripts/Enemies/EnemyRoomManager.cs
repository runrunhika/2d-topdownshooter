using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ������Enemy���Ǘ� */
public class EnemyRoomManager : MonoBehaviour
{
    //Enemy���Ǘ����郊�X�g
    [SerializeField]
    private List<Movement> enemies;

    private void Start()
    {
        foreach (Transform tr in GetComponentInChildren<Transform>())
        {
            //EnemyRoomManager�z����Enemy��enemies(�Ǘ����郊�X�g)�ɓ����
            enemies.Add(tr.GetComponent<Movement>());
        }
    }

    //Player��ǐ�
    public void EnablePlayerTarget()
    {
        foreach (Movement move in enemies)
        {
            move.HasPlayerTarget = true;
        }
    }
    //Player��������
    public void DisablePlayerTarget()
    {
        foreach (Movement move in enemies)
        {
            move.HasPlayerTarget = false;
        }
    }
}
