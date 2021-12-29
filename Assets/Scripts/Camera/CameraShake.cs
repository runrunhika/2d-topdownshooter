using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeAmount = 0.007f;

    private Vector3 camPos;
    private float cameraShakingOffsetX, cameraShakingOffsetY;

    //ƒJƒƒ‰‚ğ—h‚ç‚µ‘±‚¯‚é
    public void ShakeCamera(float shaketime)
    {
        //ˆø”1 : ŠÖ”@ˆø”2 : ‰½•bŒã‚ÉŒÄ‚Ô‚©@ˆø”3 : ‰½•b‚¨‚«‚ÉŒÄ‚Ô‚©@
        InvokeRepeating("StartCameraShaking", 0f, 0.05f);

        Invoke("StopCameraShaking", shaketime);
    }

    //ƒJƒƒ‰‚ğ—h‚ç‚·
    void StartCameraShaking()
    {
        if (shakeAmount > 0)
        {
            camPos = transform.position;

            //‚Ç‚Ì’ö“xA—h‚ç‚·‚©
            cameraShakingOffsetX = Random.value * shakeAmount * 2 - shakeAmount;
            cameraShakingOffsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += cameraShakingOffsetX;
            camPos.x += cameraShakingOffsetY;

            transform.position = camPos;
        }
    }

    //—h‚ê‚Ä‚¢‚éƒJƒƒ‰‚ğ~‚ß‚é
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");

        transform.localPosition = Vector3.zero;
    }
}
