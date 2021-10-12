﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class EnemyShipCap : MonoBehaviour, IDamagableCube
{
    [Header("Enemy Data")]
    public EnemyDataCap data;

    [Header("Sounds")]
    [SerializeField] EffectsSounds _hitSound;
    [SerializeField] EffectsSounds _destroySound;

    [Header("References")]
    [SerializeField] private Transform _model;

    [Header("Drop")]
    [SerializeField] private bool hasDrop;
    [SerializeField] private EnemyDrop[] drops;

    private Collider _collider;
    
    void Awake()
    {
        _collider = GetComponent<Collider>();
        data.lifeAmount = data.maxLife;
    }

    // TODO: implement when you want enemy drops
    void OnDrop()
    {

    }

    public void SetDamage(int damage)
    {
        Debug.Log("Set Damage: " + damage);
        data.lifeAmount -= damage;

        if (data.lifeAmount <= 0) { Destroy(); return; }

        float timeShake = .25f;
        _model.DOShakePosition(timeShake);
        _model.DOLocalMove(Vector3.zero, timeShake).SetDelay(timeShake);

        // AudioManager.Instance.PlayerSFX(AudioEffectsHandler.GetAudioClip(_hitSound));
    }

    public void Destroy()
    {
        PlayerScore.OnAddScore(data.scoreValue);

        // AudioManager.Instance.PlayerSFX(AudioEffectsHandler.GetAudioClip(_destroySound));
        // ObjectPoolerSystem.SpawnFromPool("ExplosionEnemy", transform.position, Quaternion.identity);

        _model.gameObject.SetActive(false);
        _collider.enabled = false;

        // OnDrop();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            Debug.Log("Hit Enemy ship - ouch");
            // TODO: implement IDamageable?
            collision.transform.GetComponent<IDamagableCube>().SetDamage(data.damageToPlayer);
    }
}
