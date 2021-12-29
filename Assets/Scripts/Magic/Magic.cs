using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Rigidbody2D rb;
    //魔法のスピード  消えるまでの時間
    [SerializeField] private float moveSpeed = 2.5f, deactiveTimer = 3f;
    //攻撃力
    [SerializeField] private int damageAmount = 25;
    //攻撃を与えたか判定
    private bool damage;
    [SerializeField] private bool destroyObj;
    //軌跡の有無
    [SerializeField] private bool getTrailRenderer;
    private TrailRenderer trail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //軌跡を表示可能か判定
        if (getTrailRenderer)
        {
            trail = GetComponent<TrailRenderer>();
        }
    }

    //弾の発生3秒後に消滅
    private void OnEnable()
    {
        damage = false;

        //時間差
        Invoke("DeactiveMagic", deactiveTimer);
    }

    
    private void OnDisable()
    {
        //弾の速度 = 0
        rb.velocity = Vector2.zero;

        //軌跡リセット
        if (getTrailRenderer)
        {
            trail.Clear();
        }
    }

    //動く方向を設定
    public void MoveDirection(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    //非表示
    void DeactiveMagic()
    {
        //削除するものか判定
        if (destroyObj)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss")) 
        {
            rb.velocity = Vector2.zero;

            //自動消滅をキャンセル
            CancelInvoke("DeactiveMagic");
            //ダメージを与えた獲ていない場合
            if (!damage)
            {
                damage = true;
                //攻撃実装
                collision.GetComponent<Health>().TakeDamage(damageAmount);
            }

            DeactiveMagic();
        }

        if (collision.CompareTag("Blocking"))
        {
            rb.velocity = Vector2.zero;

            //自動消滅をキャンセル
            CancelInvoke("DeactiveMagic");

            DeactiveMagic();
        }
    }
}
