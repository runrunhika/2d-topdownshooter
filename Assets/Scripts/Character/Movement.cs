using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Player�@���@Enemy(Boss�ӊO)�̃L�����̈ړ����i��    */
public class Movement : MonoBehaviour
{
    //proteccted = �h�������Ƃ��납��ĂԂ���
    [SerializeField] protected float xSpeed = 1.5f, ySpeed = 1.5f;

    private Vector2 moveDelta;

    //�^�[�Q�b�g�𔭌����Ă��邩�̔���
    private bool _hasPlayerTarget;
    //�v���p�e�B�i�֐��ɋ߂��j
    public bool HasPlayerTarget
    {
        // get �Ƃ�  x = _hasPlayerTarget
        get { return _hasPlayerTarget; }
        // set �Ƃ�  _hasPlayerTarget = value(x)
        set { _hasPlayerTarget = value; }
    }

    //�L�����N�^�[�̈ړ�
    protected void CharacterMovement(float x, float y)
    {
        moveDelta = new Vector2(x * xSpeed, y * ySpeed);

        // * Time.deltaTime = �u�ԓI�Ɉړ����邱�Ƃ�h��
        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
    }

    //�L�����̈ړ�����l��Ԃ�
    public Vector2 GetMoveDelta()
    {
        return moveDelta;
    }
}
