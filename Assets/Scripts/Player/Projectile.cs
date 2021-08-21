using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float m_LifeTime = 3;
    private Rigidbody2D m_Rigidbody2D;

    private void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (m_LifeTime > 0) m_LifeTime -= Time.deltaTime;
        else {
            ShipAttack.Instance.ProjectileRePool(this);
        }
    }

    public void ShootProjectile(Vector2 _origin, Vector2 _force) {
        if(m_Rigidbody2D == null) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(_force);
        transform.position = _origin;
        m_LifeTime = 3;
    }
}
