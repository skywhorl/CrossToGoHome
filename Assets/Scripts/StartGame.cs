using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private PlayerControll player;
    private GameObject Road;
    public GameObject[] map;
    private GameObject[] objects;
    public GameObject LodingImg;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
    }


    public void Start()
    {
        StartCoroutine("Loding");
        // 게임이 시작되면 플레이어 앞에 20칸까지 도로를 생성
        for (float i = 0; i < 20; i++)
        {
            Road = Instantiate(map[Random.Range(0, 11)], new Vector3(0, 0, i), transform.localRotation);
            if (Road.gameObject.tag == "TrainRoad")
            {
                Road.gameObject.transform.Translate(0, 0, 0.5f);
                i += 5;
            }
            // 선로 타일이 시야의 마지막에 있을 때 다음 도로의 좌표 조절
            if (i > 19.5f)
                PlayerControll.setNextRoadPoint = i - 19;
        }
    }


    public void ReGame()
    {
        StartCoroutine("Loding");
        // 도로에 생성되어 있는 오브젝트들 전부 삭제
        objects = GameObject.FindGameObjectsWithTag("Road");
        for (int i = 0; i < objects.Length; i++)
            Destroy(objects[i]);
        objects = GameObject.FindGameObjectsWithTag("TrainRoad");
        for (int i = 0; i < objects.Length; i++)
            Destroy(objects[i]);

        // 플레이어 원위치 후 다시 시작
        player.Start();
        // 다시 시작할 때 앞에 20칸은 생성하기(앞에 도로가 바로 보이도록)
        Start();
    }


    IEnumerator Loding()
    {
        LodingImg.SetActive(true);
        PlayerControll.play = false;
        yield return new WaitForSeconds(2.0f);
        LodingImg.SetActive(false);
        PlayerControll.play = true;
    }
}
