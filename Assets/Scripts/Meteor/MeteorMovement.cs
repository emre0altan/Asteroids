using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public Vector2 continuousForce;

    private Rigidbody2D m_Rigidbody2D;

    private void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        m_Rigidbody2D.velocity = continuousForce;
        CheckEdges();
    }

    public void CheckEdges() {
        Vector3 targetPosition = transform.position;
        if (targetPosition.y < -10.5f) targetPosition.y = 10;
        else if (targetPosition.y > 10.5f) targetPosition.y = -10;

        if (targetPosition.x < -18.3) targetPosition.x = 18;
        else if (targetPosition.x > 18.3f) targetPosition.x = -18;
        transform.position = targetPosition;
    }
}
