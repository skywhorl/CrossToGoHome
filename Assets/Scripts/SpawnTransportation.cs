using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTransportation : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject[] Car;
    public GameObject Train;
    private int randomSpeed;
    private float spawnTime;
    public static bool goTrain = false;


    void Start()
    {
        spawnTime = Random.Range(2, 5);
        randomSpeed = Random.Range(0, 3);
        if(gameObject.transform.parent.tag != "TrainRoad")
        StartCoroutine("Caller");
    }

    // 계속해서 차가 나타나도록 무한 반복 설정
    IEnumerator Caller()
    {
        StartCoroutine(CallTransportation());
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine("Caller");
    }
    IEnumerator CallTransportation()
    {
            Spawn = Instantiate(Car[randomSpeed], transform.position, Car[randomSpeed].transform.rotation);
            Spawn.transform.parent = transform;
        yield return null;
    }
    IEnumerator SpawnTrain()
    {
        Spawn = Instantiate(Train, transform.position, Train.transform.rotation);
        Spawn.transform.parent = transform;
        yield return new WaitForSeconds(2.0f);
    }
}
