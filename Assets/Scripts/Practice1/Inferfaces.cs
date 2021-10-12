using UnityEngine;

public interface IDamagableCube
{
    void SetDamage(int damage);

    void Destroy();
}

public interface IUpgradeItemCube
{
    void UpgradeSmartBomb();

    void HealRing(int healValue, bool isGoldRing);

    void AddLife();
}
public interface IDetectableCube
{
    Transform GetTransform();

    void ShowArrow(bool state);
}

public interface IReflectCube
{
    bool IsReflective();
}

public interface IStageLimitCube
{
    void DOUTurn(Transform direction);
}

public interface IUpgradeWeaponCube
{
    void UpgradeWeapon();
}

public class ReadOnlyAttributeCube : PropertyAttribute { }