using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    //Creates a new event.
    public UnityEvent m_Death = new UnityEvent();
    public UnityEvent m_Messages = new UnityEvent();

    void Start()
    {
        m_Death.AddListener(MyAction);
        m_Messages.AddListener(MyMessages);
    }

    //The private float value of scrap parts the player collects to fix things.
    private float ScrapCount;

    //The four private floats for Turrets.
    private float Turret1;
    private float Turret2;
    private float Turret3;
    private float Turret4;

    //The private float value of player's health.
    private float PlayerHealth;

    //The private float value of the castle health.
    private float CastleHealth;

    //The public float value which gets the private float value of ScrapCount.
    public float scrpcont { get { return ScrapCount; } }

    //The public float value which gets the private float value of Turrets 1-4.
    public float turr1 { get { return Turret1; } }
    public float turr2 { get { return Turret2; } }
    public float turr3 { get { return Turret3; } }
    public float turr4 { get { return Turret4; } }

    //The public float value which gets the private float value of PlayerHealth.
    public float PlyrHealth { get { return PlayerHealth; } }

    //The public float value which gets the private float value of PlayerHealth.
    public float CstlHealth { get { return CastleHealth; } }

//-------------------------------------------------------------------------------------

    void MyAction()
    {
        //Handle the death screen popup here.

    }

    void MyMessages()
    {
        //handle all the messages in the game here.
    }




}
