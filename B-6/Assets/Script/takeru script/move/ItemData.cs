using UnityEngine;

public enum ItemType
{
    Consumable,
    Material,
    KeyItem
}

public enum ItemEffect
{
    None,
    Damage,
    Heal,
    Buff,
    Debuff,
    EncounterReduce,
    WallMaterial
}

[CreateAssetMenu(menuName = "RPG/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName = "Potion";

    public ItemType type = ItemType.Consumable;
    public ItemEffect effect = ItemEffect.Heal;

    public int effectValue = 50;

    public bool infiniteUse = false;
    public int uses = 1;

    public bool usableInBattle = true;
    public bool usableInField = false;

    public bool sellable = true;
    public int sellPrice = 10;
}