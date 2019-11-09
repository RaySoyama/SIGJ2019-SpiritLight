using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
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

    public enum PlayerWalkingSurface
    { 
        Wood,
        Grass,
        Rock
    }


    public GameObject realityPlayer;
    public GameObject realmPlayer;

    public GAMESTATE CurrentGameState;
    public PlayerWalkingSurface CurrentWalkingSurface;

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

    [SerializeField] [ReadOnlyField]
    private Vector3 huskLocation;

    [SerializeField]
    private GameObject husk;


    [SerializeField]
    private CinemachineVirtualCamera RealityCam;

    [SerializeField]
    private CinemachineVirtualCamera RealmCam;

    [SerializeField]
    private Camera mainCamera;
    public Camera MainCamera
    {
        get
        {
            return mainCamera;
        }
    }

    public Transform CurrentCameraTransform {
        get {
            if (isInRealm) {
                return RealmCam.transform;
            }
            else {
                return RealityCam.transform;
            }
        }
    }

    void Awake()
    {
        if (World == null)
        {
            World = this;
        }
        
    }

    void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            OnEnterRealm();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            OnExitRealm();
        }

        realmPlayer.transform.position = realityPlayer.transform.position + realmOffset;
    }

    public void OnEnterRealm()
    {
        isInRealm = true;
        RealmCam.Priority = 20;
        RealityCam.Priority = 10;

        huskLocation = realmPlayer.transform.position;
        husk.transform.position = huskLocation;
    }

    public void OnExitRealm()
    {
        StartCoroutine(ExitRealm());
    }
    public void OnEnter()
    { 

    }

    private IEnumerator ExitRealm()
    {
        isInRealm = false;
        RealmCam.Priority = 10;
        RealityCam.Priority = 20;

        float time = 0;
        Vector3 StartPos = realityPlayer.transform.position;

        while (time < 1.0f)
        {
            realityPlayer.transform.position = Vector3.Lerp(StartPos, huskLocation - realmOffset,time);


            //if (time > 0.8)
            //{
            //    RealityCam.m_Lens.FieldOfView = Mathf.Lerp(40, 80, time * 1.25f);
            //}
            //else
            //{
            //    RealityCam.m_Lens.FieldOfView = Mathf.Lerp(40, 80, time * 1.25f);
            //}
            time += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }

        RealityCam.m_Lens.FieldOfView = 40;
    }

}
