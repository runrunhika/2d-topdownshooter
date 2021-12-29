using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  �e���� */
public class MagicPool : MonoBehaviour
{
    public static MagicPool instnce;
    //���@��Prefab
    [SerializeField] private GameObject[] magics;
    //�����������@���Ǘ�����
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
        //�������Ă��Ȃ�
        magicSpawned = false;

        TakeMagicPool(magicIndex, spawnPos, magicRotaion, magicDirection);
    }

    void TakeMagicPool(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotaion, Vector2 magicDirection)
    {
        if (magicIndex == 0)//fire
        {
            //�������ꂽFireMagic�������AFor������
            for (int i = 0; i < fireMagic.Count; i++)
            {
                //�\������Ă��Ȃ��ꍇ
                if (!fireMagic[i].gameObject.activeInHierarchy)
                {
                    fireMagic[i].gameObject.SetActive(true);
                    //�����ʒu
                    fireMagic[i].gameObject.transform.position = spawnPos;
                    //��]
                    fireMagic[i].gameObject.transform.rotation = magicRotaion;
                    //���˂������
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

        //�������AList�ɂȂ��ꍇ
        if (!magicSpawned)
        {
            //�V�������@�𐶐�
            CreateNewMagic(magicIndex, spawnPos, magicRotaion, magicDirection);
        }
    }

    //���@�𐶐�
    void CreateNewMagic(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotaion, Vector2 magicDirection)
    {
        GameObject newMagic = Instantiate(magics[magicIndex], spawnPos, magicRotaion);

        newMagic.transform.SetParent(transform);
        //�e�̈ړ��������
        newMagic.GetComponent<Magic>().MoveDirection(magicDirection);

        //�������� newMagic �� fireMagic (List)�ɒǉ�
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
