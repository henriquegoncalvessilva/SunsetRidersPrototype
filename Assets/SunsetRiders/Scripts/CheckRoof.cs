using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRoof : MonoBehaviour
{
    public Collider obj;


    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.DownArrow)){

            transform.localPosition = new Vector3(0, -0.243f, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            transform.localPosition = new Vector3(0, 0.243f, 0);
        }*/
    }

    private void OnTriggerStay(Collider other)
    {

        
        if (other.gameObject.tag == "Roof")
        {
            obj = other;

            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space))
            {
                obj.GetComponent<Collider>().isTrigger = false;
                transform.parent.root.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                transform.parent.root.gameObject.GetComponent<Animator>().SetTrigger("JumpUP");
                transform.parent.root.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
                StartCoroutine(ActiveCol());

            }

        }
        
        


    }

    private void OnCollisionStay(Collision collision)
    {
        

        if (collision.gameObject.tag == "Roof")
        {

            if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Space))
            {
               // obj.GetComponent<Collider>().isTrigger = true;
                transform.parent.root.gameObject.GetComponent<BoxCollider>().isTrigger = true;
               // transform.parent.root.gameObject.GetComponent<Animator>().SetTrigger("JumpUP");
               // transform.parent.root.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
                StartCoroutine(DesActiveCol());

            }



        }


    }

    public IEnumerator  ActiveCol()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.root.gameObject.GetComponent<BoxCollider>().isTrigger = false;

    }
    public IEnumerator DesActiveCol()
    {
        transform.parent.root.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        yield return new WaitForSeconds(1f);
        transform.parent.root.gameObject.GetComponent<BoxCollider>().isTrigger = false;

    }
    public IEnumerator DesActiveColPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log(Vector3.Distance(this.gameObject.transform.position, obj.transform.position));
        yield return new WaitForSeconds(1.2f);
        transform.parent.root.gameObject.GetComponent<BoxCollider>().isTrigger = false;

    }







}
