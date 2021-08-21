using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public static MeteorSpawner Instance;

    public Meteor bigMeteorPrefab, midMeteorPrefab, smallMeteorPrefab;
    public ParticleSystem[] explosionParticles;
    public int minMeteorSpeed = 5, maxMeteorSpeed = 8;
   
    private List<Meteor> bigMeteors, midMeteors, smallMeteors;

    private void Awake() {
        if(Instance != null) {
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
        float spawnPerRow = _amount / 2, spawnPerColumn = 2;
        float spawnStepRow = 36 / spawnPerRow, spawnStepColumn = 20 / spawnPerColumn;

        for(int i = 0; i < _amount; i++) {
            bigMeteors.Add(Instantiate(bigMeteorPrefab, transform));
            bigMeteors[i].transform.position = new Vector3(Random.Range(0,spawnStepRow) + (i%spawnPerRow) *spawnStepRow - 18, Random.Range(0,spawnStepColumn) + (i%spawnPerColumn) *spawnStepColumn - 10, 0);
            bigMeteors[i].MeteorSpawnForce(Random.insideUnitCircle * Random.Range(minMeteorSpeed, maxMeteorSpeed), i);

            for(int j = 0;j < 2; j++) {
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
    public void UpdateSetBigMeteors(int _amount) {
        float spawnPerRow = _amount / 2, spawnPerColumn = 2;
        float spawnStepRow = 36 / spawnPerRow, spawnStepColumn = 20 / spawnPerColumn;

        for (int i = 0; i < _amount; i++) {
            if(i >= bigMeteors.Count) bigMeteors.Add(Instantiate(bigMeteorPrefab, transform));
            bigMeteors[i].transform.position = new Vector3(Random.Range(0, spawnStepRow) + (i % spawnPerRow) * spawnStepRow - 18, Random.Range(0, spawnStepColumn) + (i % spawnPerColumn) * spawnStepColumn - 10, 0);
            bigMeteors[i].MeteorSpawnForce(Random.insideUnitCircle * Random.Range(minMeteorSpeed, maxMeteorSpeed), i);
        }
    }

    //Divided Meteor Spawn
    public void SpawnDividedMeteors(MeteorType _meteorType ,Vector2 _dividePosition, Vector2 _goingDirection, int _spawnIndex) {

        if (_meteorType == MeteorType.Big) {
            midMeteors[_spawnIndex * 2].gameObject.SetActive(true);
            midMeteors[_spawnIndex * 2].transform.position = _dividePosition;
            midMeteors[_spawnIndex * 2].MeteorSpawnForce(Quaternion.Euler(0, 0, 45) * _goingDirection * 1.5f, _spawnIndex * 2);
            midMeteors[_spawnIndex * 2 + 1].gameObject.SetActive(true);
            midMeteors[_spawnIndex * 2 + 1].transform.position = _dividePosition;
            midMeteors[_spawnIndex * 2 + 1].MeteorSpawnForce(Quaternion.Euler(0, 0, -45) * _goingDirection * 1.5f, _spawnIndex * 2 + 1);
            explosionParticles[0].transform.position = _dividePosition;
            explosionParticles[0].Play();

            GameController.Instance.AddScore(20);
        }
        else if (_meteorType == MeteorType.Medium) {
            smallMeteors[_spawnIndex * 2].gameObject.SetActive(true);
            smallMeteors[_spawnIndex * 2].transform.position = _dividePosition;
            smallMeteors[_spawnIndex * 2].MeteorSpawnForce(Quaternion.Euler(0, 0, 45) * _goingDirection * 1.5f, _spawnIndex * 2);
            smallMeteors[_spawnIndex * 2 + 1].gameObject.SetActive(true);
            smallMeteors[_spawnIndex * 2 + 1].transform.position = _dividePosition;
            smallMeteors[_spawnIndex * 2 + 1].MeteorSpawnForce(Quaternion.Euler(0, 0, -45) * _goingDirection * 1.5f, _spawnIndex * 2 + 1);
            explosionParticles[1].transform.position = _dividePosition;
            explosionParticles[1].Play();

            GameController.Instance.AddScore(50);
        }
        else {
            explosionParticles[2].transform.position = _dividePosition;
            explosionParticles[2].Play();

            GameController.Instance.AddScore(100);
        }
    }   

}
