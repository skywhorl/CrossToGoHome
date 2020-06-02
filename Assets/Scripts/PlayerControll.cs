using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    public static float setNextRoadPoint = 0;
    public int scoreint = 0;  //UI를 위해 만듦
    public static bool play;
    private StartGame setGame;
    private GameObject road;
    private Vector3 forward;
    private Text showScore;
    private float z, x, spin, frontCount, trainRoadCount;
    


    public void Start()
    {
        z = -1;
        x = 0;
        spin = 0;
        frontCount = 0;
        trainRoadCount = 0;
        play = true;
        MoveV();
        showScore = GameObject.Find("Score").GetComponent<Text>();
        setGame = GameObject.Find("GameRuleScript").GetComponent<StartGame>();
        showScore.text = (z + 1).ToString();
        scoreint = (int)(frontCount);
    }


    void Update()
    {
        if (Input.anyKeyDown && play == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                z += 1;
                spin = 0;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (transform.position.x > -19)
                {
                    x -= 1;
                    spin = -90;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (transform.position.z > -10)
                {
                    z -= 1;
                    spin = 180;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (transform.position.x < 19)
                {
                    x += 1;
                    spin = 90;
                }
            }
            MoveV();
            UpdateScore();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // 교통수단과 충돌 시 ReGame 함수 실행(다시 시작)
        if (other.tag == "Car" || other.tag == "Train")
        {
            scoreint = 0;
            setNextRoadPoint = 0;
            setGame.ReGame();
        }
    }


    private void MoveV()
    {
        transform.localRotation = Quaternion.Euler(0, spin, 0);
        // 플레이어 회전 후 앞의 장애물 판별하기
        RaycastHit hit;
        forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out hit, 1f) && hit.collider.tag == "Decoration")
        {
            // z, x값의 변화 막기
            z = transform.localPosition.z;
            x = transform.localPosition.x;
        }
        else
            transform.localPosition = new Vector3(x, 0.5f, z);
    }


    private void UpdateScore()
    {
        // 앞으로 한 칸 움직이면 max값과 동일하게 되어 다음 도로를 만들고 점수판에 점수를 기록
        // trainCount의 역할 : 소환된 열차 수만큼 소환될 도로를 선로의 길이만큼 뒤로 당겨야한다(5f칸 짜리라서)
        if (frontCount == z)
        {
            road = Instantiate(setGame.map[Random.Range(0, 11)], new Vector3(0, 0, z + 20 + trainRoadCount + setNextRoadPoint), transform.localRotation);
            if (road.gameObject.tag == "TrainRoad")
            {
                road.gameObject.transform.Translate(0, 0, 0.5f);
                trainRoadCount += 5f;
            }
            scoreint = (int)(frontCount + 1);
            showScore.text = (frontCount + 1).ToString();
            frontCount++;
        }
    }
}
