using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    [SerializeField]
    [ReadOnlyField]
    private Vector3 huskLocation;

    [SerializeField]
    private GameObject husk;

    [SerializeField] Image fadeScreen;


    [Header("Cam")]
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

    public Transform CurrentCameraTransform
    {
        get
        {
            if (isInRealm)
            {
                return RealmCam.transform;
            }
            else
            {
                return RealityCam.transform;
            }
        }
    }


    [Header("Triggers")]
    public GameObject triggerOne;
    public bool triggerOneTriggered = false;
    public Animator triggerOneAnim;

    [Space(10)]

    public GameObject triggerTwo;
    public bool triggerTwoTriggered = false;
    public AudioSource triggerTwoAS;

    [Space(10)]
    public GameObject triggerThree;
    public bool triggerThreeTriggered = false;
    


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
            OnEnterRealmStart();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            OnExitRealm();
        }

        realmPlayer.transform.position = realityPlayer.transform.position + realmOffset;
    }

    public void OnEnterRealmStart()
    {
        AudioManager.Audio.PlayRealmEnter();
        realityPlayer.GetComponent<Animator>().SetTrigger("enterRealm");
        realmPlayer.GetComponent<Animator>().SetTrigger("enterRealm");


        StartCoroutine(EnterRealm());
    }

    public void OnEnterRealmEntry()
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

    private IEnumerator EnterRealm()
    {
        yield return new WaitForSeconds(1.16f);
        OnEnterRealmEntry();
    }

    private IEnumerator ExitRealm()
    {
        isInRealm = false;
        RealmCam.Priority = 10;
        RealityCam.Priority = 20;

        float time = 0;
        Vector3 StartPos = realityPlayer.transform.position;
        AudioManager.Audio.PlayRealmExit();

        while (time < 1.0f * 1.563f)
        {
            realityPlayer.transform.position = Vector3.Lerp(StartPos, huskLocation - realmOffset, time / 1.563f);

            time += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }

        RealityCam.m_Lens.FieldOfView = 40;
    }



    public void OnEventTriggerEnter(GameObject trigger)
    {
        if (trigger == triggerOne && triggerOneTriggered == false)
        {
            triggerOneTriggered = true;
            //Do Spooky shit
            Debug.Log("Spook 1");
            triggerOneAnim.SetTrigger("spook");
            AudioManager.Audio.CricketAmbient.Pause();
            StartCoroutine(CicadaPause());

        }
        else if (trigger == triggerTwo && triggerTwoTriggered == false)
        {
            triggerTwoTriggered = true;
            //Do Spooky shit
            Debug.Log("Spook 2");
            triggerTwoAS.Play();
        }
    }

    public void OnTarTrigger(GameObject trigger) {
        Debug.Log("Tar'd");
        AudioManager.Audio.PlayDeath();

        // Freeze Player
        Controller controller = realityPlayer.GetComponent<Controller>();
        HeadBob headBob = realityPlayer.GetComponent<HeadBob>();

        if (controller) {
            controller.StopMovement();
        }

        StartCoroutine(Die());
    }

    private IEnumerator Die() {
        yield return StartCoroutine(FadeOut());
        yield return StartCoroutine(Restart());
    }

    private IEnumerator FadeOut() {
        float fadeOutTime = 2f;
        float currentTime = 0f;

        if (fadeScreen == null) {
            yield return new WaitForSeconds(fadeOutTime);
            yield break;
        }

        Color startColor = fadeScreen.color;
        Color endColor = startColor;
        endColor.a = 1;
        Color currentColor = startColor;
        while (currentTime < fadeOutTime) {
            currentTime += Time.deltaTime;
            currentColor = Color.Lerp(startColor, endColor, currentTime/fadeOutTime);
            fadeScreen.color = currentColor;
            yield return null;
        }
    }

    private IEnumerator Restart() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator CicadaPause()
    {
        yield return new WaitForSeconds(2);
        AudioManager.Audio.CricketAmbient.Play();
    }

}
