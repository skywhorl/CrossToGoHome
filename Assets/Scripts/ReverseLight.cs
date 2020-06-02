using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseLight : MonoBehaviour
{
    public GameObject Light_L;
    public GameObject Light_R;
    public GameObject StartPoint;
    public static bool flick = false;

    void Start()
    {
        Light_L.SetActive(false);
        Light_R.SetActive(false);
        StartCoroutine("Flicker");
    }


    void Update()
    {

    }

    IEnumerator Flicker()
    {
        yield return new WaitForSeconds(4.0f);
        for (int i = 0; i < 5; i++)
        {
            Light_L.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Light_L.SetActive(false);
            Light_R.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Light_R.SetActive(false);
        }

        StartPoint.GetComponent<ReverTrans>().StartCoroutine("SpawnTrain");
        StartCoroutine("Flicker");
    }
}
