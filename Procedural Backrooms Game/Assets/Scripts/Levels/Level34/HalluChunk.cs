using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalluChunk : MonoBehaviour
{
    float ElapForExplos;
    public Chunk In;
    public Generation Parent;
    public GameObject Bubble;
    GameObject ActiveBubble;
    public AudioSource Explosion;
    // Start is called before the first frame update
    void Start()
    {
        Explosion = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        ElapForExplos += Time.deltaTime;
        if (ActiveBubble)
        {
            if (ActiveBubble.transform.localScale.x < 80)
            {
                ActiveBubble.transform.localScale += new Vector3(5, 5, 5) * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        foreach (Light l in GetComponents<Light>()) 
        {
            if(ElapForExplos > 30)
            {
                l.intensity = Mathf.Sin(ElapForExplos);
            }
        }
        if(ElapForExplos>60)
        {
            if(Parent.PlayerIn == In)
            {
                if (!Explosion.isPlaying)
                {
                    Explosion.Play();
                }
                EZCameraShake.CameraShaker.Instance.ShakeOnce(5, 2, 0.5f, 4f);
                if (!ActiveBubble)
                {
                    ActiveBubble = Instantiate(Bubble, transform.position, Quaternion.identity);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
