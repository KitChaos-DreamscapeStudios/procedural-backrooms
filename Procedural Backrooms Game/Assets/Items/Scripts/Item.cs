using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool isHoldable;
    public bool isUsable;
    public GameObject DropObject;
    public Sprite Icon;
    public PlayerStats playerStats;
    public string ItemName;
    public abstract void UseInInventory();
    public abstract void UseHeld();
}
