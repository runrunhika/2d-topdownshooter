using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float shootTimer, shootTimeDelay = 0.2f;
    [SerializeField] private Transform magicSpawnPos;
    private PlayerMagicSquareManager playerMagicSquareManager;

    private void Awake()
    {
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }
    
    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //˜A‘±ŽËŒ‚‚ð–h‚®
            if (Time.time > shootTimer)
            {
                shootTimer = Time.time + shootTimeDelay;

                playerMagicSquareManager.Shoot(magicSpawnPos.position);
            }
        }
    }

    void Update()
    {
        Shooting();
    }
}
