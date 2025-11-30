using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    public const string ANIM_RUN = "run";
    public const string ANIM_IDLE = "idle";
    public const string ANIM_DIE = "die";
    public const string ANIM_DANCE = "dance";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_WIN = "win";

    public const string TAG_CHARACTER = "Character";
    public const string TAG_BLOCK = "Block";


}

public enum WeaponType
{
    W_Arrow = PoolType.W_Arrow,
    W_Hammer = PoolType.W_Hammer,
    W_Fire_Axe = PoolType.W_Fire_Axe,
    W_2_Blade_Axe = PoolType.W_2_Blade_Axe,
    W_Lolipop = PoolType.W_Lolipop,
    W_Candy_Stick = PoolType.W_Candy_Stick,
    W_Ice_Cream = PoolType.W_Ice_Cream,
    W_Candy = PoolType.W_Candy,
    W_Boomerang = PoolType.W_Boomerang,
    W_Knife = PoolType.W_Knife,
    W_Z = PoolType.W_Z,
}

public enum BulletType
{
    Straight_Arrow = PoolType.Straight_Arrow,
    Rolate_Hammer = PoolType.Rolate_Hammer,
    Rolate_Fire_Axe = PoolType.Rolate_Fire_Axe,
    Rolate_2_Blade_Axe = PoolType.Rolate_2_Blade_Axe,
    Straight_Lolipop = PoolType.Straight_Lolipop,
    Rolate_Candy_Stick = PoolType.Rolate_Candy_Stick,
    Straight_Ice_Cream = PoolType.Straight_Ice_Cream,
    Straight_Candy = PoolType.Straight_Candy,
    TurnBack_Boomerang = PoolType.TurnBack_Boomerang,
    Straight_Knife = PoolType.Straight_Knife,
    TurnBack_Z = PoolType.TurnBack_Z,
}

public enum HatType
{
    HAT_None = 0,
    HAT_Arrow = PoolType.HAT_Arrow,
    HAT_Cap = PoolType.HAT_Cap,
    HAT_Cowboy = PoolType.HAT_Cowboy,
    HAT_Crown = PoolType.HAT_Crown,
    HAT_Ear = PoolType.HAT_Ear,
    HAT_StrawHat = PoolType.HAT_StrawHat,
    HAT_Headphone = PoolType.HAT_Headphone,
    HAT_Horn = PoolType.HAT_Horn,
    HAT_Police = PoolType.HAT_Police,
    HAT_Rau = PoolType.HAT_Rau,
}

public enum SkinType
{
    SKIN_Normal = PoolType.SKIN_Normal,
    SKIN_Devil = PoolType.SKIN_Devil,
    SKIN_Angle = PoolType.SKIN_Angle,
    SKIN_Witch = PoolType.SKIN_Witch,
    SKIN_Deadpool = PoolType.SKIN_Deadpool,
    SKIN_Thor = PoolType.SKIN_Thor,
}

public enum AccessoryType
{
    ACC_None = 0,
    ACC_Book = PoolType.ACC_Book,
    ACC_CaptainShield = PoolType.ACC_Captain,
    ACC_Headphone = PoolType.ACC_Headphone,
    ACC_Shield = PoolType.ACC_Shield,
}

public enum PantType
{
    Pant_1,
    Pant_2,
    Pant_3,
    Pant_4,
    Pant_5,
    Pant_6,
    Pant_7,
    Pant_8,
    Pant_9,
}