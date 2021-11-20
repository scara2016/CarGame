using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    CheckpointManager checkpointManager;
    Text timerText;
    GameObject gameEndPanel;
    Text gameStatus;
    Text timerList;
    string timerListText = "";
    private float timer;
    private int gameEnd = 1;
    private bool gameWon;
    public Image healthBarImage;
    private PlayerHealth player;
    private PlayerMovement playerMovement;

    static private UIManager instance;
    static public UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("There is no UI manager");
            }
            return instance;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        gameStatus = GameObject.FindGameObjectWithTag("GameStatus").GetComponent<Text>();
        gameEndPanel = GameObject.FindGameObjectWithTag("GameEnd");
        timerList = GameObject.FindGameObjectWithTag("TimerList").GetComponent<Text>();
        player = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
        gameEndPanel.SetActive(false);

    }

    

    void Update()
    {
        UpdateHealthBar();
        SetTimer();
        if (checkpointManager.GameWon || player.Health<=0)
        {
            if (gameEnd > 0)
            {
                playerMovement.GameEnded = true;
                gameEnd--;
                if(checkpointManager.GameWon)
                SetGameEnd(true);
                else if (player.Health <= 0)
                    SetGameEnd(false);
                setTimerList();
            }
        }
        
    }

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(player.Health / player.totalHealth, 0, 1f);
    }

    private void setTimerList()
    {
        timerList.alignment = TextAnchor.UpperCenter;
        for(int i=0; i < checkpointManager.Checkpoints.Length; i++)
        {
            if (checkpointManager.Checkpoints[i].CheckPointReached)
            {
                int minutes = Mathf.FloorToInt(checkpointManager.Checkpoints[i].HitCheckpointTimer / 60F);
                int seconds = Mathf.FloorToInt(checkpointManager.Checkpoints[i].HitCheckpointTimer % 60F);
                int milliseconds = Mathf.FloorToInt((checkpointManager.Checkpoints[i].HitCheckpointTimer * 100F) % 100F);
                timerListText += "Checkpoint" + " " + (i + 1).ToString() + " " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00") + "\n";
            }
            else
            {
                timerListText += "Checkpoint" + " " + (i + 1).ToString() + " " + "incomplete" + "\n";
            }
        }
        timerList.text = timerListText;
    }

    private void SetGameEnd(bool gameOutcome)
    {
        gameEndPanel.SetActive(true);
        gameStatus.alignment = TextAnchor.UpperCenter;
        if(gameOutcome)
        gameStatus.text = "Game Won!!";
        else if(!gameOutcome)
                gameStatus.text = "Game Lost D:";
        else
            gameStatus.text = "Fuck";
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    private void SetTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
}
