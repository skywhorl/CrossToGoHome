using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Image station;
    public Image subway;
    private int goDefault = 0;
    private int pri = 0;  //플레이어 z값과 비교용
    public PlayerControll play;
    private Vector3 vect; // 전철 이동용
    public Text nowstation;
    public Text nextstation;
    public GameObject clear;
    private int one = 1; //1호선(?) 몇 번째 역
    private int two = 1; //2호선(?) 몇 번째역
    public Station stations;
    public bool next = true;  //Update에서 계속 i,j++을 막기위함


    // Start is called before the first frame update
    void Start()
    {
        station = GetComponent<Image>();
        clear.SetActive(false);
        nowstation = GameObject.Find("nowStation").GetComponent<Text>();
        nextstation = GameObject.Find("nextStation").GetComponent<Text>();
        station.fillAmount = 0;
        play = FindObjectOfType<PlayerControll>();
        vect = new Vector3(-825, 0, 0); // 전철 아이콘 초기 위치
        nowstation.text = stations.station1[one - 1];  //처음역(현재)
        nextstation.text = stations.station1[two];  //다음역
    }

    // Update is called once per frame
    void Update()
    {
        if (play.scoreint == 0) //모든걸 초기화(regame시작시 초기화용)
        {
            goDefault = 0;
            vect.x = 50;
            subway.transform.position = vect;
            one = 1;
            two = 1;
            next = true;
            nowstation.text = stations.station1[one - 1];
            nextstation.text = stations.station1[two];
        }
        if (play.scoreint % 10 == 0)
        {
            goDefault = 0;       // 전철 아이콘 초기로 돌아가기 위함
            station.fillAmount = 0;   // 게이지바 0
            if (play.scoreint != 0)   // PlayerCon에서 scoreint를 만들어서 비교 
            {
                if (stations.station1.Length > one)   //(x호선) 현재역과 다음역 출력
                {
                    nowstation.text = stations.station1[one - 1];
                    nextstation.text = stations.station1[one];
                    if (next == true)  // true여야 증가시킴
                    {
                        one++;
                        next = false; //연속증가 막기
                    }

                }
                else if (stations.station1.Length == one) // 환승
                {
                    nowstation.text = stations.station1[one - 1];
                    nextstation.text = stations.station2[two - 1];
                    if (next == true)  // true여야 증가시킴
                    {
                        one++;
                        next = false; //연속증가 막기
                    }

                }
                else if (stations.station2.Length > two) //(y호선) 현재역과 다음역 출력
                {
                    nowstation.text = stations.station2[two - 1];
                    nextstation.text = stations.station2[two];
                    if (next == true)
                    {
                        two++;
                        next = false;
                    }
                }
                else if (stations.station2.Length <= two)
                {
                    clear.SetActive(true); //클리어
                    PlayerControll.play = false;
                    Time.timeScale = 0;
                }
            }

        }
        else
        {
            if (pri != play.scoreint) // 현재위치와 전 위치 비교
            {
                goDefault++;  // 10을 만들기 위함
                pri = play.scoreint; // 현재위치 저장(전 위치가 될거임)
                station.fillAmount += 0.1f; // 10%씩 증가
                if (goDefault == 0) // 전철 위치 초기화
                {
                    vect.x = 50;
                    subway.transform.position = vect;
                }
                else
                {
                    //그게 아니면 전철 위치 이동
                    vect.x += 112;
                    vect.y = 980;
                    subway.transform.position = vect;
                }
            }
        }
        if (goDefault == 0) // 위 if문 못들어갈 경우를 위함,덕분에 next변수 초기화 가능
        {
            vect = new Vector3(50, 980, 0);
            subway.transform.position = vect;
        }
        else
        {
            next = true;
        }
    }
}
