using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    public TextMeshProUGUI m_CurrentControlTypeText, m_PlayerScoreText;
    public GameObject[] healthImages;
    public GameObject restartButton;


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

    public void RestartGameCanvas() {
        GameController.Instance.RestartGame();
    }

    public void UpdateHealth(int _amount) {
        for(int i = 0; i < healthImages.Length; i++) {
            if (i < _amount) healthImages[i].SetActive(true);
            else healthImages[i].SetActive(false);
        }
    }
}
