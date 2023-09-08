using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string InteractText;
    public bool DoNotAllowInteraction;
    // Start is called before the first frame update
    public abstract void OnInteract();
}
