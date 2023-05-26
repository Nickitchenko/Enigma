using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHeart : MonoBehaviour
{
    private bool StartTime = false;

    private void Start()
    {
        Invoke("StartTimer", 0.5f);
    }

    private void StartTimer()
    {
        StartTime = true;
    }
    public GameObject main;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bot") && StartTime)
        {
            float myRadius = main.GetComponent<BotMagnit>().radius;
            float enemyRadius = other.gameObject.GetComponent<BotMagnit>().radius;
            if(myRadius>enemyRadius)
            {
                main.GetComponent<BotMagnit>().score = main.GetComponent<BotMagnit>().scoreToUpgrade;
                main.GetComponent<BotMagnit>().CheckSize();
                other.GetComponent<BotMagnit>().Death();

            }
            else
            {
                other.GetComponent<BotMagnit>().score = other.GetComponent<BotMagnit>().scoreToUpgrade;
                other.GetComponent<BotMagnit>().CheckSize();
                main.gameObject.GetComponent<BotMagnit>().Death();
            }
        }
    }
}
