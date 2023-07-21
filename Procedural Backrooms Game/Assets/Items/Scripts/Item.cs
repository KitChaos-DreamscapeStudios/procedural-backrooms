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
    //public Sprite HeldGFX;
    public Vector3 HeldPos;
    public bool PointForward;
    private void LateUpdate()
    {
        if (PointForward)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
    public abstract void Use();
   // public abstract void UseHeld();
}
