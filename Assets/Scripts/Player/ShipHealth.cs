using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipHealth : MonoBehaviour
{
    public ParticleSystem crashExplosion;

    int health;
    float healthLoseTimer = 0;
    Tween damagedTween;

    private void Start() {
        health = 5;
    }

    private void Update() {
        if (healthLoseTimer > 0) healthLoseTimer -= Time.deltaTime;
        else if (damagedTween != null && damagedTween.IsPlaying()) {
            damagedTween.Kill();
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            CrashToMeteor(collision.gameObject.GetComponent<Meteor>().meteorType);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 9) {
            GainHealth(1);
        }
    }

    void CrashToMeteor(MeteorType _meteorType) {
        if(healthLoseTimer <= 0) {
            LoseHealth(1);
            healthLoseTimer = 3;
            damagedTween = GetComponent<SpriteRenderer>().DOFade(0.3f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    void LoseHealth(int _amount) {
        health -= _amount;
        if (health < 0) health = 0;
        if (CanvasController.Instance != null) CanvasController.Instance.UpdateHealth(health);

        if (health == 0) Explode();
    }

    void GainHealth(int _amount) {
        health += _amount;
        if (health > 5) health = 5;
        if (CanvasController.Instance != null) CanvasController.Instance.UpdateHealth(health);
    }

    void Explode() {
        crashExplosion.transform.SetParent(null);
        crashExplosion.Play();
        gameObject.SetActive(false);

        CanvasController.Instance.restartButton.SetActive(true);
    }
}
