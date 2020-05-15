using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Damage;

    public Rigidbody rig;

    public float speed;

    public Camera cam;

    public Sprite _sprite;

    private void Awake()
    {
       

    }

    private void Start()
    {

        rig = GetComponent<Rigidbody>();
        cam = Camera.main;
        rig.velocity = transform.right * speed;
        //DestroyObject(this.gameObject, 3);
        

    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Bug")
        {
           
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreLayerCollision(15, 17);

        if (collision.gameObject.layer == 12)
        {

            //this.gameObject.GetComponent<Animator>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = _sprite;
            //rig.bodyType = RigidbodyType2D.Static;
            //GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this.gameObject);

        }



    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("Destroyed");
    }



    public void EffectBullet()
    {
        Destroy(this.gameObject);
       
    }



}
