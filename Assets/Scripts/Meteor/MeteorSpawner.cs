using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {
    public static MeteorSpawner Instance;

    public Meteor bigMeteorPrefab, midMeteorPrefab, smallMeteorPrefab;
    public ParticleSystem[] explosionParticles;
    public int minMeteorSpeed = 5, maxMeteorSpeed = 8;

    List<Meteor> bigMeteors, midMeteors, smallMeteors;
    int currentActiveMeteorCount = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;

        bigMeteors = new List<Meteor>();
        midMeteors = new List<Meteor>();
        smallMeteors = new List<Meteor>();
    }

    //First Spawn
    public void SpawnMeteors(int _amount) {
        float spawnPerRow = _amount / 2f, spawnPerColumn = 2f;
        float spawnStepRow = 36f / spawnPerRow, spawnStepColumn = 20f / spawnPerColumn;

        for (int i = 0; i < _amount; i++) {
            bigMeteors.Add(Instantiate(bigMeteorPrefab, transform));
            bigMeteors[i].transform.position = new Vector3(Random.Range(0, spawnStepRow) + (i % spawnPerRow) * spawnStepRow - 18, Random.Range(0, spawnStepColumn) + (i % spawnPerColumn) * spawnStepColumn - 10, 0);
            bigMeteors[i].MeteorSpawnForce(Random.insideUnitCircle * Random.Range(minMeteorSpeed, maxMeteorSpeed), i);
            currentActiveMeteorCount++;

            for (int j = 0; j < 2; j++) {
                midMeteors.Add(Instantiate(midMeteorPrefab, transform));
                midMeteors[i * 2 + j].gameObject.SetActive(false);
            }

            for (int j = 0; j < 4; j++) {
                smallMeteors.Add(Instantiate(smallMeteorPrefab, transform));
                smallMeteors[i * 4 + j].gameObject.SetActive(false);
            }
        }
    }

    //Next Level Spawn
    public void SpawnNewBigMeteors(int _amount) {
        float spawnPerRow = _amount / 2f, spawnPerColumn = 2f;
        float spawnStepRow = 36f / spawnPerRow, spawnStepColumn = 20f / spawnPerColumn;

        for (int i = 0; i < _amount; i++) {
            if (i >= bigMeteors.Count) {
                bigMeteors.Add(Instantiate(bigMeteorPrefab, transform));

                for (int j = 0; j < 2; j++) {
                    midMeteors.Add(Instantiate(midMeteorPrefab, transform));
                    midMeteors[i * 2 + j].gameObject.SetActive(false);
                }

                for (int j = 0; j < 4; j++) {
                    smallMeteors.Add(Instantiate(smallMeteorPrefab, transform));
                    smallMeteors[i * 4 + j].gameObject.SetActive(false);
                }
            }
            bigMeteors[i].transform.position = new Vector3(Random.Range(0, spawnStepRow) + (i % spawnPerRow) * spawnStepRow - 18, Random.Range(0, spawnStepColumn) + (i % spawnPerColumn) * spawnStepColumn - 10, 0);
            bigMeteors[i].MeteorSpawnForce(Random.insideUnitCircle * Random.Range(minMeteorSpeed, maxMeteorSpeed), i);
            bigMeteors[i].gameObject.SetActive(true);
            currentActiveMeteorCount++;
        }
    }

    //Divided Meteor Spawn
    public void SpawnDividedMeteors(MeteorType _meteorType, Vector2 _dividePosition, Vector2 _goingDirection, int _spawnIndex) {

        if (_meteorType == MeteorType.Big) {
            ActivateMidSizeMeteor(_spawnIndex * 2, _dividePosition, _goingDirection, 45);
            ActivateMidSizeMeteor(_spawnIndex * 2 + 1, _dividePosition, _goingDirection, -45);
            currentActiveMeteorCount++;

            PlayExplosionParticle(0, _dividePosition);
            GameController.Instance.AddScore(20);
        }
        else if (_meteorType == MeteorType.Medium) {
            ActivateSmallSizeMeteor(_spawnIndex * 2, _dividePosition, _goingDirection, 45);
            ActivateSmallSizeMeteor(_spawnIndex * 2 + 1, _dividePosition, _goingDirection, -45);
            currentActiveMeteorCount++;

            PlayExplosionParticle(1, _dividePosition);
            GameController.Instance.AddScore(50);
        }
        else {
            currentActiveMeteorCount--;
            if (currentActiveMeteorCount == 0) GameController.Instance.NextLevel();
            PlayExplosionParticle(2, _dividePosition);
            GameController.Instance.AddScore(100);
        }
    }

    void ActivateMidSizeMeteor(int _index, Vector2 _dividePosition, Vector2 _goingDirection, float angle) {
        midMeteors[_index].gameObject.SetActive(true);
        midMeteors[_index].transform.position = _dividePosition;
        midMeteors[_index].MeteorSpawnForce(Quaternion.Euler(0, 0, angle) * _goingDirection * 1.5f, _index);
    }

    void ActivateSmallSizeMeteor(int _index, Vector2 _dividePosition, Vector2 _goingDirection, float angle) {
        smallMeteors[_index].gameObject.SetActive(true);
        smallMeteors[_index].transform.position = _dividePosition;
        smallMeteors[_index].MeteorSpawnForce(Quaternion.Euler(0, 0, angle) * _goingDirection * 1.5f, _index);
    }

    //0 for big, 1 for mid, 2 for small
    void PlayExplosionParticle(int _index, Vector2 _dividePosition) {
        explosionParticles[_index].transform.position = _dividePosition;
        explosionParticles[_index].Play();
    }

}
