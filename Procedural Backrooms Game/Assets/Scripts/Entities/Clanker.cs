using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clanker : Damager
{
    public AudioSource Cry;
    public AudioSource FootStep;
    public float Speed;
    public PlayerStats Player;
    Vector3 Target;
    Vector3 BaseAngle;
    float ResetCharge;
    float OriginChargeElap;
    public enum State
    {
        Looking,
        Charging,
        Wandering,
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        OriginChargeElap += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 1.66f, transform.position.z);
        Target.y = 1.66f;
        if (OriginChargeElap > 2)
        {
            if (state == State.Wandering)
            {
                transform.LookAt(Target);

                if (Vector3.Distance(transform.position, Target) < 1)
                {
                    state = State.Looking;

                }
                transform.position += transform.forward / 5;

            }

            if (!Cry.isPlaying && gameObject.activeSelf)
            {
                if (state == State.Charging)
                {
                    Cry.Play();
                }


            }
            if (Vector3.Distance(transform.position, Player.transform.position) > 200)
            {
                Vector3 Warppos = Player.transform.position + Random.onUnitSphere * 30;
                Warppos.y = 1.66f;
                transform.position = Warppos;
            }
            if ((CheckPlayer() || Player.GetComponent<Movement3D>().SoundLevel > Vector3.Distance(transform.position, Player.transform.position)) && state != State.Charging)
            {
                Speed = 20;
                FootStep.Play();
                Target = Player.transform.position;
                state = State.Charging;

            }
            if (state == State.Charging)
            {
                if (Vector3.Distance(transform.position, Target) > 1)
                {
                    transform.LookAt(Target);
                    transform.position += transform.forward / 1.5f;
                }
                else
                {
                    state = State.Looking;
                }

            }
            if (state == State.Looking)
            {
                if (Vector3.Distance(transform.eulerAngles, BaseAngle) < 50)
                {
                    transform.eulerAngles += new Vector3(0, 0.5f, 0);
                }
                else
                {
                    Vector3 newPos = transform.position + Random.insideUnitSphere * 20;
                    newPos.y = transform.position.y;
                    Target = newPos;
                    state = State.Wandering;
                }
            }

        }






    }
    bool CheckPlayer()
    {
        RaycastHit Hit;
        if(Physics.Raycast(transform.position+transform.forward, transform.forward, out Hit))
        {
            if (Hit.collider.gameObject.layer == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public override void OnDamage()
    {
        throw new System.NotImplementedException();
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
