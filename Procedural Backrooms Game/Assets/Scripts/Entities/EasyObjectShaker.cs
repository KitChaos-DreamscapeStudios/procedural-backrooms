using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyObjectShaker : MonoBehaviour
{
    public float ShakeAmount;
    public int ShakeTimes;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.ShakeObject(ShakeAmount, ShakeTimes);
    }
    private void Awake()
    {
        gameObject.ShakeObject(ShakeAmount, ShakeTimes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public EasyObjectShaker(float Amount, int Times)
    {
        this.ShakeAmount = Amount;
        this.ShakeTimes = Times;
    }
}
