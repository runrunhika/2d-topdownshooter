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

        //MainCamera�ɂ��Ă���CameraShake���擾
        cameraShake = cam.GetComponent<CameraShake>();
    }

    private void Update()
    {
        ChangeMagic();
    }

    //�����w�؂�ւ�
    private void ChangeMagic()
    {
        //�}�E�X�z�C�[���Ŗ��@�w�؂�ւ�
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //���݂̖��@�w���\��
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

        //NumberKey�Ŗ��@�w�؂�ւ�
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

    //�ˌ�
    public void Shoot(Vector2 spawnPos)
    {
        //�łꏊ�i�}�E�X�̌��ݒn�j
        targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        //�ł��o���ꏊ
        magicSpawnPos = spawnPos;

        //����
        direction = (targetPos - magicSpawnPos).normalized;
        //��]
        magicRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        //GameObject newMagic = Instantiate(magics[magicSquareIndex], spawnPos, magicRotation);
        //newMagic.GetComponent<Magic>().MoveDirection(direction);

        //���@����
        MagicPool.instnce.Fire(magicSquareIndex, spawnPos, magicRotation, direction);

        cameraShake.ShakeCamera(cameraShakeTimer);
    }
}
    