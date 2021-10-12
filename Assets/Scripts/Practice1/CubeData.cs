using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObjects/Practice1/CubeData")]
public class CubeData : ScriptableObject
{
    [Header("Movement Data")]
    public bool physicMovement;

    [Range(0, 100)]
    public float trackSpeed;

    [Range(0, 100)]
    public float allRangeSpeed;

    [Range(0, 1)]
    public float smoothAllRangeMovement;

    [Range(0, 100)]
    public float localSpeed;

    [Range(0, 1)]
    public float smoothLocalSpeed;

    [Range(0, 10000)]
    public float aimSpeed;

    [Range(0, 1)]
    public float smoothAimSpeed;

    [Range(0, 90)]
    public float leanAngle;

    [Range(0, 90)]
    public float horizontalLeanLimit;
    
    [Range(0, 1)]
    public float leanSpeed;

    [Header("Inputs")]
    [ReadOnly] public Vector2 movementInput;
    [ReadOnly] public int leanAxisInput;
    [ReadOnly] public int lifeAmount;
    [ReadOnly] public bool isDamageEffect;

    [Header("State")]
    [ReadOnly] public ShipStateCube shipState;
    [ReadOnly] public AcrobaticStateCube acrobaticState;
    [ReadOnly] public LeanStateCube leanState;
    [ReadOnly] public WeaponStateCube weaponState;
    [ReadOnly] public ChargeWeaponStateCube chargeWeaponState;
    [ReadOnly] public EnemyDetectionStateCube enemyDetectionState;


    #region Weapon Actions
    public event Action onFireLaser;
    public void OnFireLaser() { onFireLaser?.Invoke(); }

    public event Action onReleaseLaser;
    public void OnReleaseLaser() { onReleaseLaser?.Invoke(); }
    #endregion

    #region UI
    public event Action onUpdateHP;
    public void OnUpdateHP() { onUpdateHP?.Invoke(); }
    #endregion

    #region Input
    public event Action<bool> onInputActive;
    public void OnInputActive(bool state) { onInputActive?.Invoke(state); }
    #endregion

    #region Visual
    public event Action onDamageEffect;
    public void OnDamageEffect() { onDamageEffect?.Invoke();  }
    #endregion

    public void UpdateInputData(Vector2 newMovement)
    {
        movementInput = newMovement;
    }

    public void UpdateStateInput(ShipStateCube newShipState, LeanStateCube newLeanState)
    {
        shipState = newShipState;
        leanState = newLeanState;
    }
}

public enum ShipStateCube { AllRangeMode, TrackMode }
public enum AcrobaticStateCube { None, Somersult, UTurn };
public enum LeanStateCube { None, LeftLean, RightLean, BarrelRoll }
public enum WeaponStateCube { LvOne, LvTwo, LvThree }
public enum ChargeWeaponStateCube { None, Charging, Charged, Released }
public enum EnemyDetectionStateCube { None, CanDetect, Detected }
