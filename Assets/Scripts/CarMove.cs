using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private float carSpeed;
    private float trainSpeed = 50f;


    void Start()
    {
        carSpeed = Random.Range(10, 20);
        // 전철과 차 회전 방향 설정(전철 에셋이 처음부터 이상한 각도로 되어 있음)
        if (gameObject.tag == "Train")
            transform.localRotation = Quaternion.Euler(-90, 90, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 90, 0);
    }
    void Update()
    {
        if (gameObject.tag == "Train")
            transform.Translate(0, -trainSpeed * Time.deltaTime, 0);
            
        else
            transform.Translate(0, 0, carSpeed * Time.deltaTime);
        //교통수단의 X 위치가 20을 넘기면 삭제
        if (transform.position.x > 20)
            Destroy(gameObject);
    }
}
