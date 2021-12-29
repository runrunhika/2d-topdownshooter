using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeAmount = 0.007f;

    private Vector3 camPos;
    private float cameraShakingOffsetX, cameraShakingOffsetY;

    //�J������h�炵������
    public void ShakeCamera(float shaketime)
    {
        //����1 : �֐��@����2 : ���b��ɌĂԂ��@����3 : ���b�����ɌĂԂ��@
        InvokeRepeating("StartCameraShaking", 0f, 0.05f);

        Invoke("StopCameraShaking", shaketime);
    }

    //�J������h�炷
    void StartCameraShaking()
    {
        if (shakeAmount > 0)
        {
            camPos = transform.position;

            //�ǂ̒��x�A�h�炷��
            cameraShakingOffsetX = Random.value * shakeAmount * 2 - shakeAmount;
            cameraShakingOffsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += cameraShakingOffsetX;
            camPos.x += cameraShakingOffsetY;

            transform.position = camPos;
        }
    }

    //�h��Ă���J�������~�߂�
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");

        transform.localPosition = Vector3.zero;
    }
}
