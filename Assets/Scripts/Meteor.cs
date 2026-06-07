using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour
{
    public float moveSpeed;
    float startY;

    public GameObject meteorPrefab;
    public Transform meteorSpawnPoint;
    float spawnRate = 2f;
    float nextSpawnTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = Random.Range(1.5f, 2.5f);
        startY = transform.position.y;
        nextSpawnTime = Time.time + spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.InstantiateMeteorDeath();
            }
            Destroy(gameObject);
        }
    }
}
