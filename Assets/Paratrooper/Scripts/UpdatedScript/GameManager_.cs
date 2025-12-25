using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_ : MonoBehaviour
{
    public static GameManager_ Instance;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;
    private int currentScore;
    private int highScore;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InitializeScore();
        UpdateTexts();
    }
    
    public void InitializeScore()
    {
        currentScore = 10;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void OnBulletFired()
    {
        if(currentScore > 0)
        {
            currentScore --;
            UpdateTexts();
        }
    }
    public void OnEnemyDestroyed(int reward)
    {
        currentScore += reward;
       // UpdateHighScore();
        UpdateTexts();
    }
    public void UpdateHighScore()
    {
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
    public void UpdateTexts()
    {
        UpdateHighScore();
        currentScoreText.text =  currentScore.ToString();
        highScoreText.text = highScore.ToString();
    }


    

    
}