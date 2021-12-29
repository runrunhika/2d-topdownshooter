using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  弾生成 */
public class MagicPool : MonoBehaviour
{
    public static MagicPool instnce;
    //魔法のPrefab
    [SerializeField] private GameObject[] magics;
    //生成した魔法を管理する
    private List<Magic> fireMagic = new List<Magic>();
    private List<Magic> iceMagic = new List<Magic>();
    private List<Magic> thunderMagic = new List<Magic>();

    private bool magicSpawned;

    private void Awake()
    {
        if (instnce == null)
        {
            instnce = this;
        }
    }


    public void Fire(int magicIndex, Vector3 spawnPos, 
        Quaternion magicRotaion, Vector2 magicDirection)
    {
        //生成していない
        magicSpawned = false;

        TakeMagicPool(magicIndex, spawnPos, magicRotaion, magicDirection);
    }

    void TakeMagicPool(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotaion, Vector2 magicDirection)
    {
        if (magicIndex == 0)//fire
        {
            //生成されたFireMagic分だけ、For分を回す
            for (int i = 0; i < fireMagic.Count; i++)
            {
                //表示されていない場合
                if (!fireMagic[i].gameObject.activeInHierarchy)
                {
                    fireMagic[i].gameObject.SetActive(true);
                    //生成位置
                    fireMagic[i].gameObject.transform.position = spawnPos;
                    //回転
                    fireMagic[i].gameObject.transform.rotation = magicRotaion;
                    //発射する方向
                    fireMagic[i].MoveDirection(magicDirection);


                    magicSpawned = true;
                    break;
                }
            }
        }

        if (magicIndex == 1)//ice
        {
            for (int i = 0; i < iceMagic.Count; i++)
            {
                if (!iceMagic[i].gameObject.activeInHierarchy)
                {
                    iceMagic[i].gameObject.SetActive(true);
                    iceMagic[i].gameObject.transform.position = spawnPos;
                    iceMagic[i].gameObject.transform.rotation = magicRotaion;
                    iceMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;
                }
            }
        }

        if (magicIndex == 2)//thunder
        {
            for (int i = 0; i < thunderMagic.Count; i++)
            {
                if (!thunderMagic[i].gameObject.activeInHierarchy)
                {
                    thunderMagic[i].gameObject.SetActive(true);
                    thunderMagic[i].gameObject.transform.position = spawnPos;
                    thunderMagic[i].gameObject.transform.rotation = magicRotaion;
                    thunderMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;
                }
            }
        }

        //生成分、Listにない場合
        if (!magicSpawned)
        {
            //新しく魔法を生成
            CreateNewMagic(magicIndex, spawnPos, magicRotaion, magicDirection);
        }
    }

    //魔法を生成
    void CreateNewMagic(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotaion, Vector2 magicDirection)
    {
        GameObject newMagic = Instantiate(magics[magicIndex], spawnPos, magicRotaion);

        newMagic.transform.SetParent(transform);
        //弾の移動する方向
        newMagic.GetComponent<Magic>().MoveDirection(magicDirection);

        //生成した newMagic を fireMagic (List)に追加
        if (magicIndex == 0)
        {
            fireMagic.Add(newMagic.GetComponent<Magic>());
        }
        if (magicIndex == 1)
        {
            iceMagic.Add(newMagic.GetComponent<Magic>());
        }
        if (magicIndex == 2)
        {
            thunderMagic.Add(newMagic.GetComponent<Magic>());
        }
    }
}
