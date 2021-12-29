using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  移動・アニメーション・キャラの向きを管理    */
//Movement継承
public class PlayerMovement : Movement
{
    //入力受け取り
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

    //キャラが向くべき方向を決める
    void PlayerTurning()
    {
        //ScreenToWorldPoint = MousePositionをゲーム内のPositionに変換
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //  normalized = 方向のみを知るため
        direction = new Vector2(
            mousePos.x - transform.position.x, mousePos.y - transform.position.y
            ).normalized;

        PlayerAnimation(direction.x, direction.y);
    }

    void PlayerAnimation(float x, float y)
    {
        // RoundToInt : 整数に丸める(10.5 -> 10,  11.5 -> 12)
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;

        //右を向く
        if (x > 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        //左を向く
        else if (x < 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }
        //向き更新
        transform.localScale = tempScale;

        //BlendTree内で 01で判断しているから
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
