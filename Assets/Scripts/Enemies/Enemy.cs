using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{
    private Transform player;
    private Vector3 playerLastPos, startPos, movementPos;

    [SerializeField] private float chaseSpeed = 0.8f, truningDelay = 1f;
    //Player�̈ʒu���Ō�ɔc���������� , �����]���\�Ȏ���
    private float lastFollowTime, turningTimeDelay = 1f;

    private Vector3 tempScales;

    //�U���ς݂��̔���
    private bool attacked;
    //�U���̃N�[���_�E������
    [SerializeField] private float damageCooldown = 1f;
    private float damageCooldownTimer;
    //�U����
    [SerializeField] private int damageAmount = 1;

    private Health enemyHealth;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLastPos = player.position;

        //Enemy�̏����ʒu
        startPos = transform.position;
        lastFollowTime = Time.time;
        /*  Player�̈ʒu���擾���A��莞�Ԃ܂Œ��i����AI�̂���   */
        //��莞�Ԃ̐ݒ�
        turningTimeDelay *= turningTimeDelay;

        enemyHealth = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Enemy���S�� || Player���擾�ł��Ă��Ȃ��ꍇ
        if (!enemyHealth.IsAllive() || !player)
        {
            return;
        }

        MoveAnimation();

        TurnAround();

        ChaseingPlayer();
    }

    /*  �ǐ�  */
    void ChaseingPlayer()
    {
        /*  Player�𔭌��ł����ꍇ  */
        // Movement<_hasPlayerTarget�̒��g���擾(get)����
        if (HasPlayerTarget)
        {
            if (!attacked)
            {
                Chase();
            }
            else
            {
                //�U�����ł��Ȃ��ꍇ
                if (Time.time < damageCooldownTimer)
                {
                    //�����ʒu�ɖ߂�
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
            //�X�^�[�g�����n�_���擾
            movementPos = startPos - transform.position;
            //���݂̈ʒu���X�^�[�g�n�_�t�߂̏ꍇ
            if (Vector3.Distance(transform.position, startPos) < 0.1f)
            {
                movementPos = Vector3.zero;
            }
        }

        CharacterMovement(movementPos.x, movementPos.y);
    }

    void Chase()
    {
        //�Ȃ����ꍇ
        if (Time.time - lastFollowTime > turningTimeDelay)
        {
            //Player�̈ʒu�擾
            playerLastPos = player.transform.position;
            //�擾���Ԃ�ύX
            lastFollowTime = Time.time;
        }

        //Player�Ƌ���������ꍇ
        if (Vector3.Distance(transform.position, playerLastPos) > 0.15f)
        {
            //Player�̕����Ɉړ�
            movementPos = (playerLastPos - transform.position).normalized * chaseSpeed;
        }
        else
        {
            movementPos = Vector3.zero;
        }
    }
    //������ς���
    void TurnAround()
    {
        tempScales = transform.localScale;

        if (HasPlayerTarget)
        {
            //Player�̉E�ɂ���ꍇ
            if (player.position.x > transform.position.x)
            {
                //�E����
                tempScales.x = Mathf.Abs(tempScales.x);
            }

            if (player.position.x < transform.position.x)
            {
                //������
                tempScales.x = -Mathf.Abs(tempScales.x);
            }
        }
        else
        {
            //�����ʒu���E�ɂ���ꍇ
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
            //�U���N�[���_�E���X�V
            damageCooldownTimer = Time.time + damageCooldown;
            //�U��������
            attacked = true;
            //�U������
            collision.GetComponent<Health>().TakeDamage(damageAmount);
        }
    }

    void MoveAnimation()
    {
        /*  �����Ă��邩����    */
        //�L�����N�^�[�̈ړ���(x, y) -> ���l�ɕϊ����Ĕ���
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
