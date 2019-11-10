using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Audio;

    [Header("Foot Steps")]
    [SerializeField]
    private List<AudioSource> FootStepAudioSource;

    [SerializeField]
    private List<AudioClip> FootstepWoodClips;
    [SerializeField]
    private List<AudioClip> FootstepGrassClips;
    [SerializeField]
    private List<AudioClip> FootstepRockClips;

    private int PreviousFootstep;


    [Header("SFX")]

    [SerializeField]
    private List<AudioSource> PlayerSFXAudioSource;

    [SerializeField]
    private List<AudioClip> PlayerSFXClips;

    [Header("Realm SFX")]

    [SerializeField]
    private List<AudioSource> RealmAudioSource;

    [SerializeField]
    private List<AudioClip> RealmEnterExitClips;

    void Awake()
    {
        if (Audio == null)
        {
            Audio = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayFootstep();
        }
           

    }


    public void PlayFootstep()
    {
        int idx = -1;

        switch (WorldMachine.World.CurrentWalkingSurface)
        {
            case WorldMachine.PlayerWalkingSurface.Wood:

                while (idx == PreviousFootstep || idx == -1)
                {
                    idx = Random.Range(0, FootstepWoodClips.Count - 1);

                    if (FootstepWoodClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstep = idx;

                foreach (AudioSource audio in FootStepAudioSource)
                { 
                     audio.clip = FootstepWoodClips[idx];
                     audio.Play();
                }

                break;

            case WorldMachine.PlayerWalkingSurface.Grass:

                while (idx == PreviousFootstep || idx == -1)
                {
                    idx = Random.Range(0, FootstepGrassClips.Count - 1);

                    if (FootstepGrassClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstep = idx;

                foreach (AudioSource audio in FootStepAudioSource)
                {
                    audio.clip = FootstepGrassClips[idx];
                    audio.Play();
                }
                break;
            case WorldMachine.PlayerWalkingSurface.Rock:

                while (idx == PreviousFootstep || idx == -1)
                {
                    idx = Random.Range(0, FootstepRockClips.Count - 1);

                    if (FootstepRockClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstep = idx;

                foreach (AudioSource audio in FootStepAudioSource)
                {
                    audio.clip = FootstepRockClips[idx];
                    audio.Play();
                }
                break;
        }
    }


    //Player SFX system

    public void PlayRealmEnter()
    {
        //RealmAudioSource.clip = RealmEnterExitClips[0];
        //RealmAudioSource.Play();
    }

    public void PlayRealmExit()
    {

        //RealmAudioSource.clip = RealmEnterExitClips[1];
        //RealmAudioSource.Play();
    }
}

