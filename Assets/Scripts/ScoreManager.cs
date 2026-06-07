using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMPro.TextMeshProUGUI scoreText;
    private int currentScore = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
