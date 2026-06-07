using UnityEngine;
using UnityEngine.SceneManagement;
public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    float spawnRate = 2f;
    
    float minY = -6f;
    float nextSpawnTime = 0f;

    public void towardsardsPlayer(Meteor meteor)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 direction = (player.transform.position - meteor.transform.position).normalized;
            meteor.moveSpeed = Random.Range(1.5f, 2.5f);
            meteor.GetComponent<Rigidbody2D>().linearVelocity = direction * meteor.moveSpeed;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnTime = Time.time + spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMeteor();
            nextSpawnTime = Time.time + spawnRate;
        }

        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    void SpawnMeteor()
    {
        float spawnX = Random.Range(-3f, 3f);
        float spawnY = Random.Range(0f, 2f);
        GameObject meteorObj = Instantiate(meteorPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
        Meteor meteorScript = meteorObj.GetComponent<Meteor>();
        towardsardsPlayer(meteorScript);
    }

}
