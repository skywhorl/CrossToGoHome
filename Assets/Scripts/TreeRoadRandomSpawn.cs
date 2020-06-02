using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoadRandomSpawn : MonoBehaviour
{
    public GameObject[] Deco;
    public static GameObject Spawn;
    private int random;

    void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            random = Random.Range(-20, 20);
            Spawn = Instantiate(Deco[i%3], new Vector3(random, transform.position.y+0.5f, transform.position.z), Deco[0].transform.rotation );
            Spawn.transform.parent = transform;
        }
    }
}
