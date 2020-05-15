using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_SunsetRiders : MonoBehaviour
{
    //Singleton
    static public Manager_SunsetRiders _instance;

    [SerializeField]
    Animator anim;

    [SerializeField]
    bool playerMoving;

    [SerializeField]
    Rigidbody rigdPlayer;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
       
    }

    private void Start()
    {

        //Get info of Player
        rigdPlayer = Player._instance.GetRigidBody();
        anim = Player._instance.GetAnimatorPlayer();

    }

    private void Update()
    {
        
        playerMoving = Player._instance.IsMoving();
        
    }




}
