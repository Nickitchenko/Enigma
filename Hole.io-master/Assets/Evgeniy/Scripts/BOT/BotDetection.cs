using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDetection : MonoBehaviour
{
    public BotMagnit bm;

    private bool StartTime = false;

    private void Start()
    {
        Invoke("StartTimer", 0.5f);
    }

    private void StartTimer()
    {
        StartTime = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bot")&&StartTime)
        {
            if (other.gameObject.GetComponent<BotMagnit>().LVL < bm.LVL)
            {
                bm.targetBot = other.gameObject;
                bm.MoveToBot = true;
            }
        }
    }
}
