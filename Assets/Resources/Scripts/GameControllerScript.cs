using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private int score;

    [SerializeField] Text timerText;
    [SerializeField] float gameTimer = 30.0f;

    [SerializeField] GameObject[] moles;
    [SerializeField] float moleActiveTime = 1.5f;
    [SerializeField] float spawnInterval = 2.0f;

    private bool gameActive = false;

    void Start()
    {
        // La partida no empieza automáticamente; se debe llamar a StartGame()
        score = 0;
        UpdateScoreText();
        UpdateTimerText();
    }

    public void StartGame()
    {
        if (!gameActive)
        {
            gameActive = true;
            score = 0;
            gameTimer = 30.0f;
            UpdateScoreText();
            UpdateTimerText();
            StartCoroutine(SpawnMoles());
        }
    }

    IEnumerator SpawnMoles()
    {
        while (gameActive)
        {
            int randomIndex = Random.Range(0, moles.Length);
            ActivateMole(randomIndex);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void ActivateMole(int index)
    {
        moles[index].SetActive(true);
        StartCoroutine(DeactivateMole(index));
    }

    IEnumerator DeactivateMole(int index)
    {
        yield return new WaitForSeconds(moleActiveTime);
        moles[index].SetActive(false);
    }

    void Update()
    {
        if (gameActive)
        {
            gameTimer -= Time.deltaTime;

            if (gameTimer > 0.0f)
            {
                UpdateTimerText();
            }
            else
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        gameActive = false;
        timerText.text = "Game Over!";
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void UpdateTimerText()
    {
        timerText.text = gameTimer.ToString("F2");
    }

    public void HitMole(GameObject mole)
    {
        if (gameActive)
        {
            mole.SetActive(false);
            score++;
            UpdateScoreText();
        }
    }
}
