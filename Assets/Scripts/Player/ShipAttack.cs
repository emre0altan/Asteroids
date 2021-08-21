using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    public static ShipAttack Instance;

    public Projectile projectilePrefab;
    public float projectileSpeed;

    private Camera m_Camera;  
    private Stack<Projectile> projectilePool;
    private Projectile tmpGO;
    private float shootTimer = 0;


    private void Awake() {
        if(Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;

        m_Camera = Camera.main;
        projectilePool = new Stack<Projectile>();

        for(int i=0;i < 50; i++) {
            tmpGO = Instantiate(projectilePrefab);
            projectilePool.Push(tmpGO);
            tmpGO.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (GameController.gameState != GameState.Playing) return;

        if (shootTimer > 0) {
            shootTimer -= Time.deltaTime;
        }
        else if (Input.GetMouseButton(0) && ShipMovement.m_ControlType == ControlType.WASD) {
            Fire();
        }
        else if (Input.GetKey(KeyCode.Space) && ShipMovement.m_ControlType == ControlType.Mouse) {
            Fire();
        }
    }


    private void Fire() {
        tmpGO = projectilePool.Pop();
        tmpGO.gameObject.SetActive(true);
        tmpGO.ShootProjectile(transform.position + transform.up * 0.371f, (Input.mousePosition - m_Camera.WorldToScreenPoint(transform.position)).normalized * projectileSpeed);
        shootTimer = 0.2f;
    }


    public void ProjectileRePool(Projectile _GO) {
        _GO.gameObject.SetActive(false);
        projectilePool.Push(_GO);
    }
}
