using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Rigidbody2D rb;
    //���@�̃X�s�[�h  ������܂ł̎���
    [SerializeField] private float moveSpeed = 2.5f, deactiveTimer = 3f;
    //�U����
    [SerializeField] private int damageAmount = 25;
    //�U����^����������
    private bool damage;
    [SerializeField] private bool destroyObj;
    //�O�Ղ̗L��
    [SerializeField] private bool getTrailRenderer;
    private TrailRenderer trail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //�O�Ղ�\���\������
        if (getTrailRenderer)
        {
            trail = GetComponent<TrailRenderer>();
        }
    }

    //�e�̔���3�b��ɏ���
    private void OnEnable()
    {
        damage = false;

        //���ԍ�
        Invoke("DeactiveMagic", deactiveTimer);
    }

    
    private void OnDisable()
    {
        //�e�̑��x = 0
        rb.velocity = Vector2.zero;

        //�O�Ճ��Z�b�g
        if (getTrailRenderer)
        {
            trail.Clear();
        }
    }

    //����������ݒ�
    public void MoveDirection(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    //��\��
    void DeactiveMagic()
    {
        //�폜������̂�����
        if (destroyObj)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //�����蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss")) 
        {
            rb.velocity = Vector2.zero;

            //�������ł��L�����Z��
            CancelInvoke("DeactiveMagic");
            //�_���[�W��^�����l�Ă��Ȃ��ꍇ
            if (!damage)
            {
                damage = true;
                //�U������
                collision.GetComponent<Health>().TakeDamage(damageAmount);
            }

            DeactiveMagic();
        }

        if (collision.CompareTag("Blocking"))
        {
            rb.velocity = Vector2.zero;

            //�������ł��L�����Z��
            CancelInvoke("DeactiveMagic");

            DeactiveMagic();
        }
    }
}
