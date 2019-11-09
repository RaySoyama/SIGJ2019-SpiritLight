using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMachine : MonoBehaviour
{
    public static WorldMachine World;

    public enum GAMESTATE
    {
        Menu,
        HallOne,
        Tar,
        HallTwo,
        Boss,
        End
    }

    public GameObject player;

    public GAMESTATE CurrentGameState;

    [SerializeField]
    private bool isInRealm;
    public bool IsInRealm
    {
        get
        {
            return isInRealm;
        }
    }

    [SerializeField]
    private Vector3 realmOffset;



    void Start()
    {
        if (World == null)
        {
            World = this;
        }
        else
        {
            return;
        }



    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            EnterRealm();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ExitRealm();
        }

    }

    public void EnterRealm()
    {
        isInRealm = true;
        player.transform.position += realmOffset;

    }

    public void ExitRealm()
    {
        isInRealm = false;
        player.transform.position -= realmOffset;

    }
    public void OnEnter()
    { 

    }



}
