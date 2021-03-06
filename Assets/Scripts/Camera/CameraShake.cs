using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeAmount = 0.007f;

    private Vector3 camPos;
    private float cameraShakingOffsetX, cameraShakingOffsetY;

    //カメラを揺らし続ける
    public void ShakeCamera(float shaketime)
    {
        //引数1 : 関数　引数2 : 何秒後に呼ぶか　引数3 : 何秒おきに呼ぶか　
        InvokeRepeating("StartCameraShaking", 0f, 0.05f);

        Invoke("StopCameraShaking", shaketime);
    }

    //カメラを揺らす
    void StartCameraShaking()
    {
        if (shakeAmount > 0)
        {
            camPos = transform.position;

            //どの程度、揺らすか
            cameraShakingOffsetX = Random.value * shakeAmount * 2 - shakeAmount;
            cameraShakingOffsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += cameraShakingOffsetX;
            camPos.x += cameraShakingOffsetY;

            transform.position = camPos;
        }
    }

    //揺れているカメラを止める
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");

        transform.localPosition = Vector3.zero;
    }
}
