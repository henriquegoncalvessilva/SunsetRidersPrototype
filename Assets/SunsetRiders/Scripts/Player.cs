using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Singleton
    static public Player _instance;


    #region Public Variables




    public SpriteRenderer sprite;

    public bool isGround;

    public bool isGroundUp;    

    public GameObject Trigger;


    #endregion

    #region Private Variables
    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce, defaultJump = 3.4f;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Rigidbody rig;

    [SerializeField]
    bool action;

    [SerializeField]
    Vector3 Weapon_Pos;

    [SerializeField]
    BoxCollider checkPlataform;

    #endregion

    #region Functions
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {

        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    #region Player Actions

    private void ForceUp()
    {

        rig.AddForce(new Vector3(0, jumpForce / 2, 0), ForceMode.Impulse);

    }


    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        if (action)
        {
            ResetJump(defaultJump);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (isGround)
                {
                    anim.SetBool("Walk", true);
                }
                sprite.flipX = false;

                if (!sprite.flipX)
                {
                    Trigger.transform.localPosition = Weapon_Pos;
                    Trigger.transform.localEulerAngles = new Vector3(0, 0, 0);

                }

                //rig.velocity = new Vector3(h * speed, 0, 0);
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {

                if (isGround)
                {
                    anim.SetBool("Walk", true);
                }
                if (sprite.flipX)
                {
                    Trigger.transform.localPosition = new Vector3(-Weapon_Pos.x, Weapon_Pos.y, Weapon_Pos.z);
                    Trigger.transform.localEulerAngles = new Vector3(0, 180f, 0);

                }
                sprite.flipX = true;
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }



            else
            {
                anim.SetBool("Walk", false);

            }


            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {

                anim.SetBool("Jump", true);
                anim.SetBool("Walk", false);
                rig.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);



            }



            /* if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space))
             {
                 anim.SetTrigger("JumpUP");
                 //rig.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
                 StartCoroutine(ActiveCol());

             }*/
            /*if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.Space))
            {
                // anim.SetTrigger("JumpUP");
                // rig.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
                rig.AddTorque(new Vector3(0, 1, 0), ForceMode.Impulse);
                StartCoroutine(ActiveCol());

            }*/


        }

       
    }

    private void ResetJump(float value) {

        jumpForce = value;
    }


   /* private void ChangeModForWeapon()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("P_Weapon", true);
        }
    }*/

    private void Update()
    {
/*        ChangeModForWeapon();
*/    }

    #endregion

  
    #region Collisions
        private void OnTriggerStay(Collider other)

        {
            Collider col;

            if (other.gameObject.tag == "Door" && Input.GetKeyDown(KeyCode.UpArrow))

            {
                col = other;
                Debug.Log(col);
                Destroy(col);



                StartCoroutine(OpenDoor(other.GetComponent<SpriteRenderer>()));




            }



        }

        private void OnCollisionStay(Collision collision)
        {

            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Roof")

            {

                isGround = true;

            }

            if (collision.gameObject.tag == "Roof" && isGroundUp)
            {

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ResetJump(0);   
                        anim.SetTrigger("JumpDown");
                        GetComponent<BoxCollider>().isTrigger = true;
                        StartCoroutine(DesActiveCol());


                    }

                }

            }



        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.tag == "Ground")

            {
                checkPlataform.enabled = true;
                anim.SetBool("Jump", false);

            }

            if (collision.gameObject.tag == "Roof")

            {

            checkPlataform.enabled = false;

            isGroundUp = true;

                anim.SetBool("Jump", false);



            }

        }

        private void OnCollisionExit(Collision collision)
        {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Roof")

        {

            isGround = false;

        }

    }
    #endregion

    #region IEnumerator
    IEnumerator OpenDoor(SpriteRenderer door)
    {

        //Begin
        sprite.enabled = false;

        action = false;

        door.GetComponent<SpriteRenderer>().enabled = true;

        transform.GetChild(1).gameObject.GetComponent<Weapon>().enabled = false;

        yield return new WaitForSeconds(1f);

        door.transform.GetChild(0).gameObject.SetActive(true);

        door.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1.7f);

        sprite.enabled = true;


        yield return new WaitForSeconds(1f);

        action = false;

        transform.GetChild(1).gameObject.GetComponent<Weapon>().enabled = true;

        door.GetComponent<SpriteRenderer>().enabled = false;

        GetComponent<Player>().enabled = false;



        door.transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        door.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(1f);

        action = true;
        GetComponent<Player>().enabled = true;

        //END

    }

    public IEnumerator ActiveCol()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider>().isTrigger = false;


    }

    public IEnumerator DesActiveCol()
    {
        //jumpForce = 0;
        GetComponent<BoxCollider>().isTrigger = true;
        yield return new WaitForSeconds(0.9f);
        GetComponent<BoxCollider>().isTrigger = false;
        jumpForce = 3.4f;


    }

    #endregion

    private void ActiveAction()
    {
        action = true;
        

    }

    private void DesactiveAction()
    {
        action = false;

    }

    public Animator GetAnimatorPlayer()
    {
        return this.GetComponent<Animator>();
    }

    public Rigidbody GetRigidBody()
    {
        return this.GetComponent<Rigidbody>();

    }

    public bool IsMoving()
    {
        return action;
    }


}

#endregion




