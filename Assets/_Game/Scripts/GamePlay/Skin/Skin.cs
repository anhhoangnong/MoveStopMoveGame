using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : GameUnit
{
    [SerializeField] private PantData pantData;
    [SerializeField] private Transform head;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Renderer pant;
    [SerializeField] bool isCanChange = false;
    [SerializeField] private Animator anim;

    Weapon currentWeapon;
    Accessory currentAccessory;
    Hat currentHat;

    public Animator Anim => anim;
    public Weapon Weapon => currentWeapon;

    //thay đổi vũ khí
    public void ChangeWeapon(WeaponType weaponType)
    {
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }

    //thay đổi phụ kiện
    public void ChangeAccessory(AccessoryType accessoryType)
    {
        if (isCanChange && accessoryType != AccessoryType.ACC_None)
        {
            currentAccessory = SimplePool.Spawn<Accessory>((PoolType)accessoryType, leftHand);
        }
    }

    //thay đổi mũ
    public void ChangeHat(HatType hatType)
    {
        if (isCanChange && hatType != HatType.HAT_None)
        {
            currentHat = SimplePool.Spawn<Hat>((PoolType)hatType, head);
        }
    }

    //thay đổi quần
    public void ChangePant(PantType pantType)
    {
        pant.material = pantData.GetPant(pantType);
    }

    //hủy skin khi nhân vật bị despawn
    public void OnDespawn()
    {
        SimplePool.Despawn(currentWeapon);
        if (currentAccessory) SimplePool.Despawn(currentAccessory);
        if (currentHat) SimplePool.Despawn(currentHat);
    }

    //hủy mũ
    public void DespawnHat()
    {
        if (currentHat) SimplePool.Despawn(currentHat);
    }

    //hủy phụ kiện
    public void DespawnAccessory()
    {
        if (currentAccessory) SimplePool.Despawn(currentAccessory);
    }

    //hủy vũ khí
    internal void DespawnWeapon()
    {
        if (currentWeapon) SimplePool.Despawn(currentWeapon);
    }
}
