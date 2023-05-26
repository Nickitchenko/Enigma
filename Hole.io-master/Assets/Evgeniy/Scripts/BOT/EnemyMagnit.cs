using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagnit : MonoBehaviour
{
    public bool startMov = false;
    public int scoreAmount;
    public GameObject player;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (startMov)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, -0.1f, 0), speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.transform.position) <= 0.85f)
            {
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}
