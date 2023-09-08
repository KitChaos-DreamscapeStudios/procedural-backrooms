using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableDoor : Interactable
{
    public bool IsSafe;
    public List<ClosableDoor> NeededDoor;
    public bool IsClose;
    public Vector3 OffPos;
    public Vector3 OnPos;
    public FirebrickGen gen;
    AudioSource OpenClose;
    public override void OnInteract()
    {
        IsClose = !IsClose;
        OpenClose.Play();
    }
    private void Start()
    {
        OpenClose = GetComponent<AudioSource>();
        OffPos = transform.position;
        OnPos = transform.position + OnPos;

        gen = GameObject.Find("Generation").GetComponent<FirebrickGen>();
    }
    private void Update()
    {
        if (IsClose)
        {
           // InteractText = "Open Door";
            transform.position = Vector3.Lerp(transform.position, OnPos, 0.2f);

        }
        else
        {
           // InteractText = "Close Door";
            transform.position = Vector3.Lerp(transform.position, OffPos, 0.2f);
        }
        bool ValidateSafe = true;
        foreach (ClosableDoor d in NeededDoor)
        {
            if (!d.IsClose)
            {
                ValidateSafe = false;
            }
        }
        if (!IsClose)
        {
            ValidateSafe = false;
        }
        IsSafe = ValidateSafe;
        if (IsSafe && !gen.SafeDoors.Contains(this))
        {
            gen.SafeDoors.Add(this);
        }
        if (!IsSafe)
        {
            if (gen.SafeDoors.Contains(this))
            {
                gen.SafeDoors.Remove(this);
            }
        }
    }

}
