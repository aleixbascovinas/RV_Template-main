using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Text timerText;
    [SerializeField] float gameTimer = 30.0f;

    [SerializeField] GameObject[] moles; 
    [SerializeField] float moleActiveTime = 1.5f; 
    [SerializeField] float spawnInterval = 2.0f; 
    private bool gameActive = true;
    void Start()
    {
        StartCoroutine(SpawnMoles());
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
        
        gameTimer -= Time.deltaTime;
        
        if (gameTimer > 0.0f) {
            timerText.text = gameTimer.ToString("F2");
        }
        else
        {
            //Game Over
            timerText.text = "Game Over!";
        }
    }

    public void HitMole(GameObject mole)
    {
        mole.SetActive(false);
        Debug.Log("Mole hit!");
    }

}
