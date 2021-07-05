using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied = false;
    void OnCollisionEnter2D(Collision2D other) {
        if (ShouldDieFromCollision(other)) {
            StartCoroutine(Die());
        }

    }

    private bool ShouldDieFromCollision(Collision2D other)
    {
        if (_hasDied) return false;
        Bird bird = other.gameObject.GetComponent<Bird>();
        if (bird != null) return true;
        if (other.contacts[0].normal.y < -0.5) return true;
        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
