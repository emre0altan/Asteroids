using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType { WASD, Mouse}

public class ShipMovement : MonoBehaviour
{
    public static ControlType m_ControlType;

    public float speed = 10, angularSpeed = 10;

    private Rigidbody2D m_Rigidbody2D;
    private Camera m_Camera;
    

    private void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Camera = Camera.main;
        m_ControlType = ControlType.WASD;
    }

    private void Update() {
        if (GameController.gameState != GameState.Playing) return;

        if (Input.GetKeyDown(KeyCode.E)) {
            ChangeControlType();
        }

        if(m_ControlType == ControlType.WASD) {
            MoveWASD();
        }
        else {
            MoveMouse();
        }
        
        CheckEdges();
    }

    private void ChangeControlType() {
        if(m_ControlType == ControlType.Mouse) {
            m_ControlType = ControlType.WASD;
        }
        else {
            m_ControlType = ControlType.Mouse;
        }
        CanvasController.Instance.UpdateControlTypeText();
    }

    private void MoveWASD() {

        Vector2 direction = Input.mousePosition - m_Camera.WorldToScreenPoint(transform.position);
        if (direction != Vector2.zero) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), angularSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W)) {
            m_Rigidbody2D.AddForce(Vector2.up * speed);
        }

        if (Input.GetKey(KeyCode.S)) {
            m_Rigidbody2D.AddForce(-Vector2.up * speed);
        }

        if (Input.GetKey(KeyCode.A)) {
            m_Rigidbody2D.AddForce(-Vector2.right * speed);
        }

        if (Input.GetKey(KeyCode.D)) {
            m_Rigidbody2D.AddForce(Vector2.right * speed);
        }
    }

    private void MoveMouse() {
        Vector2 direction = Input.mousePosition - m_Camera.WorldToScreenPoint(transform.position);
        if (direction != Vector2.zero) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), angularSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(0)) {
            m_Rigidbody2D.AddForce(direction.normalized * speed);
        }

    }

    private void CheckEdges() {
        Vector3 targetPosition = transform.position;
        if (targetPosition.y < -10.5f) targetPosition.y = 10;
        else if (targetPosition.y > 10.5f) targetPosition.y = -10;

        if (targetPosition.x < -18.3) targetPosition.x = 18;
        else if (targetPosition.x > 18.3f) targetPosition.x = -18;
        transform.position = targetPosition;
    }
}
