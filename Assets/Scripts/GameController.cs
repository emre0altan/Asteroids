using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddScore(int _amount) {
        m_PlayerScore += _amount;
        CanvasController.Instance.UpdatePlayerScore(m_PlayerScore);
    }
}
