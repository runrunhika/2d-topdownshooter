using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSquareManager : MonoBehaviour
{
    [SerializeField] private GameObject[] magicSquares;

    private int currentMagicSquare;

    private void Start()
    {
        DeactivateAllMagicSquares();
    }

    //‚·‚×‚Ä‚Ì–‚–@w‚ğ”ñ•\¦‚É‚·‚é
    void DeactivateAllMagicSquares()
    {
        for (int i = 0; i < magicSquares.Length; i++)
        {
            magicSquares[i].SetActive(false);
        }
    }

    public void ActivateMagicSquare(int newMagicSquare)
    {
        magicSquares[currentMagicSquare].SetActive(false);

        currentMagicSquare = newMagicSquare;

        magicSquares[currentMagicSquare].SetActive(true);
    }
}
