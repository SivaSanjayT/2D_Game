using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manger : MonoBehaviour
{
    public static Game_Manger Instance;
    public GameObject Options;
    public GameObject GameOver;
    public GameObject BossDefeat;

    private void Start()
    {
        if (Instance == null)
        {
         Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        Options.SetActive(false);   
    }
    private void Update()
    {
        if ((SceneManager.GetActiveScene().buildIndex) == 1)
        {
            if (Player.isPlayerDead == true)
            {
                StartCoroutine(DeathDelay(2));
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            CloseOptionsMenu();
        }

        if (Boss.Instance.BossDefeat() == true)
        {
           StartCoroutine(DeathDelays(15f));
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Options.SetActive(false);
        Time.timeScale = 1.0f;
        Player.isPlayerDead = false;
    }
    public void OpenOptionsMenu()
    {
        Options.SetActive(true);
        
        if (Player.isPlayerDead == true)
        {
            GameOver.SetActive(true);
        }
        else
        {
            GameOver.SetActive(false);
        }
        Time.timeScale = 0f;
    }
    public void CloseOptionsMenu()
    {
        Options.SetActive(false);
        if (!GameOver.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // ingame

    public void RestGame()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1f;
        Health.Instance.ResetsHealth();
        Player.Instance.ResetPlayerPos();
        Player.isPlayerDead = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    IEnumerator DeathDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OpenOptionsMenu();
    }
    IEnumerator DeathDelays(float delay)
    {
        yield return new WaitForSeconds(delay);
            Options.SetActive(true);
            GameOver.SetActive(false);
            BossDefeat.SetActive(true);
            Time.timeScale = 0f;
    }
}
