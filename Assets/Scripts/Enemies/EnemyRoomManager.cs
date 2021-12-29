using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  部屋のEnemyを管理 */
public class EnemyRoomManager : MonoBehaviour
{
    //Enemyを管理するリスト
    [SerializeField]
    private List<Movement> enemies;

    private void Start()
    {
        foreach (Transform tr in GetComponentInChildren<Transform>())
        {
            //EnemyRoomManager配下のEnemyをenemies(管理するリスト)に入れる
            enemies.Add(tr.GetComponent<Movement>());
        }
    }

    //Playerを追跡
    public void EnablePlayerTarget()
    {
        foreach (Movement move in enemies)
        {
            move.HasPlayerTarget = true;
        }
    }
    //Playerを見失う
    public void DisablePlayerTarget()
    {
        foreach (Movement move in enemies)
        {
            move.HasPlayerTarget = false;
        }
    }
}
