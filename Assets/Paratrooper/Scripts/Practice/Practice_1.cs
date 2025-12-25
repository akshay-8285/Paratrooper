using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Practice_1 : MonoBehaviour
{
    [SerializeField] private int score = 50;
    [SerializeField] private TMP_InputField inputScore;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button getScoreButton;
    private List<string> itemName = new List<string>();
    void Start()
    {
        
        OnClickGetScoreButton();
        UpdateData();
        Debug.Log("itemName " + itemName[1]);

        
    }

    public void UpdateData()
    {
        itemName.Add("Sword");
        itemName.Add("Shield");
        itemName.Add("Potion");

    }

    public void OnClickGetScoreButton()
    {
        getScoreButton.onClick.AddListener(GetScoreOnUi);
    }

    public void ScoreManage()
    {
        string result = "";
        if(score < 30)
        {
            result =  "Too Low!";
        }
        else if(score >= 30 && score <=70)
        {
            result = "Good";
        }
        else if(score >70)
        {
            result = "Excellent";
        }

        scoreText.text = $"Result : {score} :→ {result}";
    }

    public void GetScoreOnUi()
    {
        if(int.TryParse(inputScore.text, out score))
        {
            scoreText.text = "Score : " + score;
            ScoreManage();
        }
        else
        {
            scoreText.text = " Please enter a valid score ";
            Debug.LogWarning("Invalid input for score.");
        }
        
    }



    
}
