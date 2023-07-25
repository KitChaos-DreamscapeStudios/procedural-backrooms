using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLock : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Offset;
    float Wait;
    Vector3 BaseOffset;
    public Vector3 RiseOffset;
    bool IsRisen;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        BaseOffset = Offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + Offset;
        var PS = Player.GetComponent<PlayerStats>();
        if(PS.Sanity < 40)
        {
            Wait += Time.deltaTime;
            if(Wait > 30)
            {
                Wait = 0;
                if (!IsRisen)
                {
                    PS.TryModSleepCon("HighWater");
                }
                else
                {
                    PS.TryModSleepCon("HighWater", true);
                }
                IsRisen = !IsRisen;
              

            }
            if (IsRisen)
            {

                Offset = Vector3.Lerp(Offset, RiseOffset, 0.06f);
                PS.GetComponent<CurLevelManager>().footStep.pitch = 0.5f;
                PS.SleepPrevented = true;
                PS.CantSleepText = "You Cannot Sleep, You Will Surely Drown";

            }
            else
            {
                PS.GetComponent<CurLevelManager>().footStep.pitch = 1f;
                Offset = Vector3.Lerp(Offset, BaseOffset, 0.06f);
                PS.SleepPrevented = false;
            }
        }
        else
        {
            PS.GetComponent<CurLevelManager>().footStep.pitch = 1f;
            Offset = Vector3.Lerp(Offset, BaseOffset, 0.06f);
            IsRisen = false;
        }
    }
    private void OnDestroy()
    {
        Player.GetComponent<PlayerStats>().TryModSleepCon("HighWater", true);
    }
}
