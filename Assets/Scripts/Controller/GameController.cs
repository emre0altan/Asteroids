using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NotStarted, Playing, Ended}

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static GameState gameState;

    public LevelValues[] m_LevelValues;

    private int m_CurrentLevel, m_PlayerScore = 0;
    private LevelValues m_CurrentLevelValues;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
        gameState = GameState.NotStarted;

        if (!PlayerPrefs.HasKey("Level")) {
            m_CurrentLevel = 0;
            PlayerPrefs.SetInt("Level", 0);
        }
        else {
            m_CurrentLevel = PlayerPrefs.GetInt("Level");
        }
        m_CurrentLevelValues = m_LevelValues[m_CurrentLevel];
    }

    public void StartGame() {
        gameState = GameState.Playing;
        MeteorSpawner.Instance.SpawnMeteors(m_CurrentLevelValues.bigMeteorCount);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public void NextLevel() {
        m_CurrentLevel++;
        if (m_CurrentLevel >= m_LevelValues.Length) m_CurrentLevel = m_LevelValues.Length - 1;
        PlayerPrefs.SetInt("Level", m_CurrentLevel);
        m_CurrentLevelValues = m_LevelValues[m_CurrentLevel];
        MeteorSpawner.Instance.SpawnNewBigMeteors(m_CurrentLevelValues.bigMeteorCount);
    }

    public void AddScore(int _amount) {
        m_PlayerScore += _amount;
        CanvasController.Instance.UpdatePlayerScore(m_PlayerScore);
    }
}
