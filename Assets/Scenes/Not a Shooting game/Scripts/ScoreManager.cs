using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI textScore;
    public Canvas canvas;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        textScore.text = score.ToString();
        if (score == 10)
        {
            canvas.gameObject.SetActive(true);
            Debug.Log("YOU WIN !!");
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName: sceneName);
    }

    public void AddScore()
    {
        score++;
    }
}
