﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CubeWeaponBehaviour : MonoBehaviour
{
    [Header("Player Settings")]
    public CubeData data;

    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    [Header("VFX")]
    [SerializeField] VisualEffect chargeLaserVFS;
    [SerializeField] VisualEffect arrowVFX;

    [Header("Lasers")]
    [SerializeField] ParticleSystem[] laserLevels;

    [Header("References")]
    [SerializeField] Transform pointChargeLaser;
    [SerializeField] Transform pointDetect;

    [Header("Layers")]
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LayerMask _laserColliderLayer;

    private CustomFixedUpdate FU_LogicInstance;
    private Transform detectedEnemy;
    private RaycastHit hit;
    private Vector3 fwd;
    private float holdTimer;
    private bool canFireBomb = true;

    private readonly string string_chargeLaser = "ChargedLaser";
    // private readonly string string_arrowCharge = "Arrow Charge";

    void Awake()
    {
        // FU_LogicInstance = new CustomFixedUpdate(OnFixedUpdate, fps);

        for (int i = 0; i < laserLevels.Length; i++)
        {
            ParticleSystem.CollisionModule collision = laserLevels[i].collision;
            collision.collidesWith = _laserColliderLayer;
        }

        // data.onReleaseLaser += () => ReleaseChargeLaser();
        data.onFireLaser += () => FireWeapon();
    }

    void Update()
    {
        // FU_LogicInstance.Update();
    }

    void OnFixedUpdate(float aDeltaTime)
    {
        // todo: once fps settings this is built
        // if (data.chargeWeaponState == ChargeWeaponStateCube.Charging)
        //    ChargeLaser();

        if (data.enemyDetectionState == EnemyDetectionStateCube.CanDetect)
            DetectEnemy(pointDetect);
    }

    #region LaserWeapon
    public void UpdateLaser()
    {
        if ((int)data.weaponState + 1 >= System.Enum.GetValues(typeof(WeaponStateCube)).Length)
            return;

        data.weaponState = (WeaponStateCube)((int)data.weaponState + 1);

        if ((int)data.weaponState + 1 == System.Enum.GetValues(typeof(WeaponStateCube)).Length)
            // data.OnOpenHyperLaser(true);
            Debug.Log("todo update laser");
    }

    public void FireWeapon()
    {
        // I think this fires the particle effect .Play()
        laserLevels[(int)data.weaponState].Play();

        if (data.weaponState == WeaponStateCube.LvOne)
            // AudioManager call 
            Debug.Log("Pew Pew todo FireWeaponSound1");
        else if (data.weaponState == WeaponStateCube.LvTwo)
            // AudioManager call 
            Debug.Log("Pew Pew todo FireWeaponSound2");
        else
            // AudioManager call 
            Debug.Log("Pew Pew todo FireWeaponSound3");
    }
    #endregion

    #region Logic
    void DetectEnemy(Transform pointLaser)
    {
        fwd = pointLaser.TransformDirection(Vector3.forward);

        if (Physics.BoxCast(pointLaser.position, Vector3.one, fwd, out hit, pointLaser.rotation, 800))
        {
            if (((1 << hit.transform.gameObject.layer) & _enemyLayer) != 0)
            {
                if (hit.transform.TryGetComponent(out IDetectableCube dtc))
                {
                    detectedEnemy = dtc.GetTransform();
                    dtc.ShowArrow(true);
                    // arrowVFX.SetFloat(string_arrowFade, 0);
                    data.enemyDetectionState = EnemyDetectionStateCube.Detected;
                    // AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.TargetLock));
                }
            }
        }
    }
    #endregion
}
