using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotMagnit : MonoBehaviour
{
    [SerializeField] private GameObject MagnitPoint;

    public float radius;
    public int score;
    public int scoreToUpgrade;

    public int LVL=1;
    public bool sizeUp;

    public GameObject objectToSizeUp;
    public BoxCollider colliderToSizeUp;
    public BoxCollider detectionColliderToSizeUp;

    public Transform playerCanvas;
    public GameObject scoreIndicator;

    public Vector3 targetPosition;
    public GameObject targetBot;
    public bool MoveToBot=false;

    public float maxDistanceFromOrigin;
    public float speed;

    private void Start()
    {
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if (MoveToBot)
        {
            if(targetBot!=null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetBot.transform.position, speed * Time.deltaTime);
            }
            else
            {
                MoveToBot = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // Если достигли целевой позиции, генерируем новую случайную позицию
            if (Vector3.Distance(transform.position, targetPosition) < 5f)
            {
                targetPosition = GetRandomPosition();
            }
            Vector3 currentPosition = transform.position;

            if (currentPosition.magnitude > maxDistanceFromOrigin)
            {
                Vector3 fromOriginToCurrent = currentPosition.normalized;
                transform.position = fromOriginToCurrent * maxDistanceFromOrigin;
            }
        }
    }

    private Vector3 GetRandomPosition()
	{
		// Генерируем случайные координаты в заданном радиусе
		float randomX = Random.Range(-maxDistanceFromOrigin+5, maxDistanceFromOrigin-5);
		float randomZ = Random.Range(-maxDistanceFromOrigin+5, maxDistanceFromOrigin-5);

		// Создаем новый вектор с использованием случайных координат
		Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

		return randomPosition;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyMagnit>().enabled = true;
            other.gameObject.GetComponent<EnemyMagnit>().startMov = true;
            other.gameObject.GetComponent<EnemyMagnit>().player = MagnitPoint;
            AddScore(other.gameObject.GetComponent<EnemyMagnit>().scoreAmount);
            Destroy(other.gameObject, 0.2f);
        }
    }

    public void AddScore(int amount)    
    {
        score += amount;
        sizeUp = false;
        CheckSize();

        GameObject indicator = Instantiate(scoreIndicator, playerCanvas);
        TextMeshProUGUI scoreText = indicator.GetComponent<TextMeshProUGUI>();
        scoreText.text = "+" + amount.ToString();
        StartCoroutine(DisableAfter(indicator, 2f));

    }

    private IEnumerator DisableAfter(GameObject obj, float delay)
    {
        if (!obj)
        {
            yield break;
        }

        yield return new WaitForSeconds(delay);

        if (!obj)
        {
            yield break;
        }
        obj.SetActive(false);
    }

    public void CheckSize()
    {
        if(score>=scoreToUpgrade)
        {
            LVL++;
            score = 0;
            scoreToUpgrade += 10;
            UpdateSize();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void UpdateSize()
    {
        maxDistanceFromOrigin=maxDistanceFromOrigin-0.5f;
        objectToSizeUp.transform.localScale=new Vector3(objectToSizeUp.transform.localScale.x + 0.25f, objectToSizeUp.transform.localScale.y, objectToSizeUp.transform.localScale.z + 0.25f);
        radius = objectToSizeUp.transform.localScale.x + 0.25f;
        float x = colliderToSizeUp.size.x;
        x =x+0.2f;
        colliderToSizeUp.size = new Vector3(x, colliderToSizeUp.size.y, x);
        float d = detectionColliderToSizeUp.size.x;
        d++;
        detectionColliderToSizeUp.size = new Vector3(d, detectionColliderToSizeUp.size.y, d);
    }
}
