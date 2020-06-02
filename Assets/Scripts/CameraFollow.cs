using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");   
    }


    void Update()
    {
        Vector3 TargetPos = new Vector3 (player.transform.position.x+10, player.transform.position.y+12, player.transform.position.z-10);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 3);
    }
}
