using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    public TextMeshProUGUI m_CurrentControlTypeText, m_PlayerScoreText;


    private void Awake() {
        if(Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void UpdateControlTypeText() {
        if(ShipMovement.m_ControlType == ControlType.WASD) {
            m_CurrentControlTypeText.text = "<b>Control Type 1</b>\nWASD to move\nMouse to shoot";
        }
        else {
            m_CurrentControlTypeText.text = "<b>Control Type 2</b>\nMouse to move\nSpace to shoot";
        }
    }

    public void UpdatePlayerScore(int _currentScore) {
        m_PlayerScoreText.text = _currentScore.ToString("0000");
    }

    public void StartGameCanvas() {
        GameController.Instance.StartGame();
    }
}
