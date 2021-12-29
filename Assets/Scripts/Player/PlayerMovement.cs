using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  �ړ��E�A�j���[�V�����E�L�����̌������Ǘ�    */
//Movement�p��
public class PlayerMovement : Movement
{
    //���͎󂯎��
    private float moveX, moveY;

    private Camera mainCam;
    private Vector2 mousePos, direction;
    private Vector3 tempScale;
    private Animator animator;

    private PlayerMagicSquareManager playerMagicSquareManager;

    private void Awake()
    {
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        PlayerTurning();

        CharacterMovement(moveX, moveY);
    }

    //�L�����������ׂ����������߂�
    void PlayerTurning()
    {
        //ScreenToWorldPoint = MousePosition���Q�[������Position�ɕϊ�
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //  normalized = �����݂̂�m�邽��
        direction = new Vector2(
            mousePos.x - transform.position.x, mousePos.y - transform.position.y
            ).normalized;

        PlayerAnimation(direction.x, direction.y);
    }

    void PlayerAnimation(float x, float y)
    {
        // RoundToInt : �����Ɋۂ߂�(10.5 -> 10,  11.5 -> 12)
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;

        //�E������
        if (x > 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        //��������
        else if (x < 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }
        //�����X�V
        transform.localScale = tempScale;

        //BlendTree���� 01�Ŕ��f���Ă��邩��
        x = Mathf.Abs(x);

        animator.SetFloat("FaceX", x);
        animator.SetFloat("FaceY", y);

        DirectionMagicSquare(x, y);
    }

    void DirectionMagicSquare(float x, float y)
    {
        //Side
        if (x == 1f && y == 0) 
        {
            playerMagicSquareManager.Activate(0);
        }
        //Up
        if (x == 0 && y == 1f)
        {
            playerMagicSquareManager.Activate(1);
        }
        //Front
        if (x == 0f && y == -1f) ;
        {
            playerMagicSquareManager.Activate(2);
        }
        //SideUp
        if (x == 1f && y == 1f)
        {
            playerMagicSquareManager.Activate(3);
        }
        //SideDown
        if (x == 1f && y == -1f)
        {
            playerMagicSquareManager.Activate(4);
        }
    }
}
