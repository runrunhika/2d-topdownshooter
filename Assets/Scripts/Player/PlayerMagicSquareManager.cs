using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicSquareManager : MonoBehaviour
{
    [SerializeField] private MagicSquareManager[] playerMagicSquares;
    private int magicSquareIndex;

    [SerializeField] private GameObject[] magics;
    private Vector2 targetPos, direction, magicSpawnPos;
    private Camera cam;
    private Quaternion magicRotation;

    private CameraShake cameraShake;
    [SerializeField] private float cameraShakeTimer = 0.2f;

    private void Awake()
    {
        magicSquareIndex = 0;
        playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

        cam = Camera.main;

        //MainCameraについているCameraShakeを取得
        cameraShake = cam.GetComponent<CameraShake>();
    }

    private void Update()
    {
        ChangeMagic();
    }

    //魔方陣切り替え
    private void ChangeMagic()
    {
        //マウスホイールで魔法陣切り替え
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //現在の魔法陣を非表示
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);

            magicSquareIndex++;

            if (magicSquareIndex >= playerMagicSquares.Length)
            {
                magicSquareIndex = 0;
            }

            playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);

            magicSquareIndex--;

            if (magicSquareIndex < 0)
            {
                magicSquareIndex = playerMagicSquares.Length - 1;
            }
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);
        }

        //NumberKeyで魔法陣切り替え
        for (int i = 0; i < playerMagicSquares.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);

                magicSquareIndex = i;

                playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

                break;
            }
        }
    }


    public void Activate(int dirIndex)
    {
        playerMagicSquares[magicSquareIndex].ActivateMagicSquare(dirIndex);
    }

    //射撃
    public void Shoot(Vector2 spawnPos)
    {
        //打つ場所（マウスの現在地）
        targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        //打ち出す場所
        magicSpawnPos = spawnPos;

        //方向
        direction = (targetPos - magicSpawnPos).normalized;
        //回転
        magicRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        //GameObject newMagic = Instantiate(magics[magicSquareIndex], spawnPos, magicRotation);
        //newMagic.GetComponent<Magic>().MoveDirection(direction);

        //魔法生成
        MagicPool.instnce.Fire(magicSquareIndex, spawnPos, magicRotation, direction);

        cameraShake.ShakeCamera(cameraShakeTimer);
    }
}
    