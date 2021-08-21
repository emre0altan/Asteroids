using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeteorType { Big, Medium, Small}

public class Meteor : MonoBehaviour
{
    public MeteorType meteorType;
    public float damageAmount = 10;

    private MeteorMovement m_MeteorMovement;
    private int m_SpawnIndex;

    private void Start() {
        m_MeteorMovement = GetComponent<MeteorMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 7) {
            DivideMeteor();
        }
    }

    public void MeteorSpawnForce(Vector2 _force, int _spawnIndex) {
        if(m_MeteorMovement == null) m_MeteorMovement = GetComponent<MeteorMovement>();
        m_MeteorMovement.continuousForce = _force;
        m_SpawnIndex = _spawnIndex;
    }

    public void DivideMeteor() {
        MeteorSpawner.Instance.SpawnDividedMeteors(meteorType, transform.position, m_MeteorMovement.continuousForce, m_SpawnIndex);
        gameObject.SetActive(false);
    }
}
