using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ // 아이템 타입
    Equipable, // 착용 가능
    Resource, // 재료 수급
    Consumable, // 음식
    Decoration, // 장식
}

public enum ConsumableType
{ // 음식 타입
    Hunger, // 허기
    Health, // 체력
}

[System.Serializable]
public class ItemDataConsumable
{ // 음식 기능 정보
    public ConsumableType type;
    public float value; // 회복량
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")] // 아이템의 정보
    public string displayName; // 이름
    public string description; // 설명
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack; // 다중 소지 가능 여부
    public int maxStackAmount; // 최대 중복 갯수

    [Header("Consumable")]
    public ItemDataConsumable[] consumables; // 음식 기능
}
