using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public float TimeLeft;
    public PlayerStats player;
    public Sprite icon;
    public string KeyName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void TickActivation();
    public abstract void OnRemove();
}
