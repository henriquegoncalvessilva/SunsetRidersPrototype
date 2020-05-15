using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject Bullet;

    public GameObject Effect;

    public float delay, delay_default;


    void Update()
    {

       /* if (Input.GetMouseButtonDown(0))
        {

            Shoot();
            Effect.SetActive(true);
           

        }

        else if (Input.GetMouseButtonUp(0))
        {

            Effect.SetActive(false);
         

        }*/



    }

    public void Shoot()
    {
        
        if (Time.time >= delay)
        {

            Instantiate(Bullet, transform.position, transform.rotation);
            delay += delay_default;
        }

    }
}
