using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameover : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = "Final Score: " + scoreKeeper.GetScore();
    }

}
