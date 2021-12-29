using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Player　＆　Enemy(Boss意外)のキャラの移動を司る    */
public class Movement : MonoBehaviour
{
    //proteccted = 派生したところから呼ぶため
    [SerializeField] protected float xSpeed = 1.5f, ySpeed = 1.5f;

    private Vector2 moveDelta;

    //ターゲットを発見しているかの判定
    private bool _hasPlayerTarget;
    //プロパティ（関数に近い）
    public bool HasPlayerTarget
    {
        // get とは  x = _hasPlayerTarget
        get { return _hasPlayerTarget; }
        // set とは  _hasPlayerTarget = value(x)
        set { _hasPlayerTarget = value; }
    }

    //キャラクターの移動
    protected void CharacterMovement(float x, float y)
    {
        moveDelta = new Vector2(x * xSpeed, y * ySpeed);

        // * Time.deltaTime = 瞬間的に移動することを防ぐ
        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
    }

    //キャラの移動する値を返す
    public Vector2 GetMoveDelta()
    {
        return moveDelta;
    }
}
