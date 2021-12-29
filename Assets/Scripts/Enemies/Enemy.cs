using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{
    private Transform player;
    private Vector3 playerLastPos, startPos, movementPos;

    [SerializeField] private float chaseSpeed = 0.8f, truningDelay = 1f;
    //Playerの位置を最後に把握した時間 , 方向転換可能な時間
    private float lastFollowTime, turningTimeDelay = 1f;

    private Vector3 tempScales;

    //攻撃済みかの判定
    private bool attacked;
    //攻撃のクールダウン時間
    [SerializeField] private float damageCooldown = 1f;
    private float damageCooldownTimer;
    //攻撃力
    [SerializeField] private int damageAmount = 1;

    private Health enemyHealth;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLastPos = player.position;

        //Enemyの初期位置
        startPos = transform.position;
        lastFollowTime = Time.time;
        /*  Playerの位置を取得し、一定時間まで直進するAIのため   */
        //一定時間の設定
        turningTimeDelay *= turningTimeDelay;

        enemyHealth = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Enemy死亡時 || Playerを取得できていない場合
        if (!enemyHealth.IsAllive() || !player)
        {
            return;
        }

        MoveAnimation();

        TurnAround();

        ChaseingPlayer();
    }

    /*  追跡  */
    void ChaseingPlayer()
    {
        /*  Playerを発見できた場合  */
        // Movement<_hasPlayerTargetの中身を取得(get)する
        if (HasPlayerTarget)
        {
            if (!attacked)
            {
                Chase();
            }
            else
            {
                //攻撃ができない場合
                if (Time.time < damageCooldownTimer)
                {
                    //初期位置に戻る
                    movementPos = startPos - transform.position;
                }
                else
                {
                    attacked = false;
                }
            }
            
        }
        else
        {
            //スタートした地点を取得
            movementPos = startPos - transform.position;
            //現在の位置がスタート地点付近の場合
            if (Vector3.Distance(transform.position, startPos) < 0.1f)
            {
                movementPos = Vector3.zero;
            }
        }

        CharacterMovement(movementPos.x, movementPos.y);
    }

    void Chase()
    {
        //曲がれる場合
        if (Time.time - lastFollowTime > turningTimeDelay)
        {
            //Playerの位置取得
            playerLastPos = player.transform.position;
            //取得時間を変更
            lastFollowTime = Time.time;
        }

        //Playerと距離がある場合
        if (Vector3.Distance(transform.position, playerLastPos) > 0.15f)
        {
            //Playerの方向に移動
            movementPos = (playerLastPos - transform.position).normalized * chaseSpeed;
        }
        else
        {
            movementPos = Vector3.zero;
        }
    }
    //向きを変える
    void TurnAround()
    {
        tempScales = transform.localScale;

        if (HasPlayerTarget)
        {
            //Playerの右にいる場合
            if (player.position.x > transform.position.x)
            {
                //右向き
                tempScales.x = Mathf.Abs(tempScales.x);
            }

            if (player.position.x < transform.position.x)
            {
                //左向き
                tempScales.x = -Mathf.Abs(tempScales.x);
            }
        }
        else
        {
            //初期位置より右にいる場合
            if (startPos.x > transform.position.x)
            {
                tempScales.x = Mathf.Abs(tempScales.x);
            }
            if (startPos.x < transform.position.x)
            {
                tempScales.x = -Mathf.Abs(tempScales.x);
            }
        }

        transform.localScale = tempScales;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //攻撃クールダウン更新
            damageCooldownTimer = Time.time + damageCooldown;
            //攻撃中判定
            attacked = true;
            //攻撃実装
            collision.GetComponent<Health>().TakeDamage(damageAmount);
        }
    }

    void MoveAnimation()
    {
        /*  動いているか判定    */
        //キャラクターの移動量(x, y) -> 数値に変換して判定
        if (GetMoveDelta().sqrMagnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
}
