using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetScore(int score)
    {
        StartCoroutine(IncreaseScoreUntilMax(score));
    }

    private IEnumerator IncreaseScoreUntilMax(int maxScore)
    {
        int score = 0;
        while(score < maxScore)
        {
            score++;
            RenderScore(score);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void RenderScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
