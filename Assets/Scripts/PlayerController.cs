using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 5f;
    Vector2 moveInput;

    float minX = -2.5f;
    float maxX = 2.5f;
    float minY = -4.5f;
    float maxY = 4.5f;

    float maxHealth = 3f;
    float currentHealth;

    public Image[] healthIcons;
     void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GameObject bulletPrefab;
    public Transform firePoint;

    float fireRate = 0.25f;
    float nextFireTime = 0f;

    public AudioSource audioSource;
    public AudioClip shootClip;

    public AudioClip hitClip;

    public AudioClip explosionClip;

    public GameObject gameOverPanel;


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnAttack()
    {
        if (Time.time >= nextFireTime)
        {
            audioSource.PlayOneShot(shootClip);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        if (audioSource != null && hitClip != null && currentHealth > 0f)
        {
            audioSource.PlayOneShot(hitClip);
        }
        UpdateHealthUI();
        if (currentHealth <= 0f)
        {
            TriggerGameOver(false);
        }
    }

    void TriggerGameOver(bool isMeteorDeath)
    {
        Debug.Log("Game Over!");
        if (!isMeteorDeath && hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Invoke("RestartGame", 2f);
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].enabled = true;
            }
            else
            {
                healthIcons[i].enabled = false;
            }
        }
    }

    void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        if (audioSource != null && hitClip != null)
        {
            audioSource.PlayOneShot(hitClip);
        }

        Invoke("RestartGame", 1.5f);
    }

    public void InstantiateMeteorDeath()
    {
        currentHealth = 0f;
        UpdateHealthUI();
        if (explosionClip != null)
        {
            Vector3 soundPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            AudioSource.PlayClipAtPoint(explosionClip, soundPosition);
        }
        TriggerGameOver(true);
    }
    
}
