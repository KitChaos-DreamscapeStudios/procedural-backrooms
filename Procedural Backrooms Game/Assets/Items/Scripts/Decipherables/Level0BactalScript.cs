using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0BactalScript : Decipherable
{
    // Start is called before the first frame update
    public override void FailEffect()
    {
        playerStats.Sanity -= 15;
        playerStats.PingAlert(FailText, Color.red);
    }
    public override void SuccessEffect()
    {
        playerStats.Sanity += 0.05f * Time.deltaTime;
    }
}
