using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Playerの入退場を検知する */

public enum EnemyTarget
{
    EnableTarget,
    DisableTarget
}

public class EnemyRoom : MonoBehaviour
{
    [SerializeField]
    private EnemyTarget enemyTarget;

    [SerializeField]
    private EnemyRoomManager enemyRoomManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enemyTarget == EnemyTarget.EnableTarget)
            {
                enemyRoomManager.EnablePlayerTarget();
            }
            else
            {
                enemyRoomManager.DisablePlayerTarget();
            }
        }
    }
}
