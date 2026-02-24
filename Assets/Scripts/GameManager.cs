using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using System;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState {
        [InspectorName("Gameplay")] GAME,
        [InspectorName("Pause")] PAUSE_MENU,
        [InspectorName("Level Completed")] LEVEL_COMPLETED,
        [InspectorName("Game over loose")] LEVEL_LOOSE,
        [InspectorName("Options")] OPTIONS
    }
    public GameState currentGameState = GameState.PAUSE_MENU;

    public Canvas inGameCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas optionsCanvas;
    public Canvas levelCompletedCanvas;
    public Canvas gameOverCanvas;
    public Image[] livesTab;
    public Image[] keysTab;
    public TMP_Text scoreText;
    public TMP_Text enemiesKilledText;
    public TMP_Text timeText;
    public TMP_Text scoreLevelCompletedText;
    public TMP_Text highScoreLevelCompletedText;
    public TMP_Text setGrapficsQualityText;

    private int score = 0;
    private int enemiesKilled = 0;
    private int keydsFound = 0;
    private int lives = 3;
    private float timer = 0.0f;
    const string keyHighScore = "HighScoreLevel1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timeText.text = string.Format("{0:00}:{1:00}", MathF.Floor(timer/60), timer - MathF.Floor(timer / 60)*60);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GAME) PauseMenu();
            else if (currentGameState == GameState.PAUSE_MENU) InGame();
        }
    }

    private void Awake()
    {

        setGrapficsQualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        if(!PlayerPrefs.HasKey(keyHighScore)) PlayerPrefs.SetInt(keyHighScore, 0);
        if (Instance == null) Instance = this;
        else Debug.Log("Duplicated Game Manager", gameObject);

        InGame();
        scoreText.text = score.ToString();
        enemiesKilledText.text = enemiesKilled.ToString();
        for (int i = 0; i < keysTab.Length; i++) 
        {
            keysTab[i].color = Color.gray;    
        }
        for (int i = 0; i < livesTab.Length; i++)
        {
            if (i < lives) livesTab[i].enabled = true;
            else livesTab[i].enabled = false;
        }
    }
    void setGameState(GameState newState)
    {
        if (newState == GameState.LEVEL_COMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Level1")
            {
                int highScore = PlayerPrefs.GetInt(keyHighScore);
                if (highScore < score)
                {
                    PlayerPrefs.SetInt(keyHighScore, score);
                    highScore = score;
                }
                scoreLevelCompletedText.text = "Score = "+score.ToString();
                highScoreLevelCompletedText.text = "Highscore = "+highScore.ToString();
            }
        }
        currentGameState = newState;
        inGameCanvas.enabled = (currentGameState == GameState.GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.PAUSE_MENU);
        levelCompletedCanvas.enabled = (currentGameState == GameState.LEVEL_COMPLETED);
        optionsCanvas.enabled = (currentGameState == GameState.OPTIONS);
        gameOverCanvas.enabled = (currentGameState == GameState.LEVEL_LOOSE);
    }
    public GameState getGameState()
    {
        return currentGameState;
    }
    public void PauseMenu()
    {
        setGameState(GameState.PAUSE_MENU);
    }
    public void InGame()
    {
        setGameState(GameState.GAME);
        Time.timeScale = 1.0f;
    }
    public void Options()
    {
        Time.timeScale = 0.0f;
        setGameState(GameState.OPTIONS);
    }
    public void LevelCompleted()
    {
        setGameState(GameState.LEVEL_COMPLETED);
    }
    public void GameOver()
    {
        setGameState(GameState.LEVEL_LOOSE);
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public void AddEnemiesKilled()
    {
        enemiesKilled++;
        enemiesKilledText.text = enemiesKilled.ToString();
    }

    public void AddKeys(Color color)
    {
        keysTab[keydsFound].color = color;
        keydsFound++;
    }
    public void AddLives()
    {
        livesTab[lives++].enabled = true;
    }
    public void DecLives()
    {
        livesTab[--lives].enabled = false;    
    }
    public int GetLives()
    {
        return lives;
    }
    public int GetKeysFound()
    {
        return keydsFound;
    }
    public void onResumeButtonClicked()
    {
        InGame();
    }
    public void onOptionsButtonClicked()
    {
        Options();
    }
    public void onRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void onOptionsButtonGraphicsUp()
    {
        QualitySettings.IncreaseLevel();
        setGrapficsQualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
    public void onOptionsButtonGraphicsDown()
    {
        QualitySettings.DecreaseLevel();
        setGrapficsQualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
    public void onExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
