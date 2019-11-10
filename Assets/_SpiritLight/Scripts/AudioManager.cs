using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Audio;

    [Header("Foot Steps")]
    [SerializeField]
    private Vector2 FootstepPitchRange;

    [SerializeField]
    private List<AudioSource> FootStepAudioSource;

    [SerializeField]
    private List<AudioClip> FootstepWoodClips;
    [SerializeField]
    private List<AudioClip> FootstepGrassClips;
    [SerializeField]
    private List<AudioClip> FootstepRockClips;

    private int PreviousFootstepOne;
    private int PreviousFootstepTwo;


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


    private BreathManager breath;

    void Awake()
    {
        if (Audio == null)
        {
            Audio = this;
        }

        breath = GetComponent<BreathManager>();  
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

        foreach (AudioSource audio in FootStepAudioSource)
        {
            audio.pitch = Random.Range(FootstepPitchRange.x, FootstepPitchRange.y);
        }

        switch (WorldMachine.World.CurrentWalkingSurface)
        {
            case WorldMachine.PlayerWalkingSurface.Wood:

                while (idx == PreviousFootstepOne || idx == PreviousFootstepTwo || idx == -1)
                {
                    idx = Random.Range(0, FootstepWoodClips.Count - 1);

                    if (FootstepWoodClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstepTwo = PreviousFootstepOne;
                PreviousFootstepOne = idx;

                foreach (AudioSource audio in FootStepAudioSource)
                { 
                     audio.clip = FootstepWoodClips[idx];
                     audio.Play();
                }

                break;

            case WorldMachine.PlayerWalkingSurface.Grass:

                while (idx == PreviousFootstepOne || idx == PreviousFootstepTwo || idx == -1)
                {
                    idx = Random.Range(0, FootstepGrassClips.Count - 1);

                    if (FootstepGrassClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstepTwo = PreviousFootstepOne;
                PreviousFootstepOne = idx;

                foreach (AudioSource audio in FootStepAudioSource)
                {
                    audio.clip = FootstepGrassClips[idx];
                    audio.Play();
                }
                break;
            case WorldMachine.PlayerWalkingSurface.Rock:

                while (idx == PreviousFootstepOne || idx == PreviousFootstepTwo || idx == -1)
                {
                    idx = Random.Range(0, FootstepRockClips.Count - 1);

                    if (FootstepRockClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstepTwo = PreviousFootstepOne;
                PreviousFootstepOne = idx;

                foreach (AudioSource audio in FootStepAudioSource)
                {
                    audio.clip = FootstepRockClips[idx];
                    audio.Play();
                }
                break;
        }
    }


    //Player SFX system

    public void PlayRealEnter()
    {
        foreach (AudioSource audio in RealmAudioSource)
        {
            audio.clip = RealmEnterExitClips[0];
            audio.Play();
        }

    }


    public void PlayRealmExit()
    {
        if (breath.BreathMeter / breath.BreathLimit > breath.GaspingThreshold)
        {
            foreach (AudioSource audio in RealmAudioSource)
            {
                audio.clip = RealmEnterExitClips[1];
                audio.Play();
            }
        }
        else
        {
            foreach (AudioSource audio in RealmAudioSource)
            {
                audio.clip = RealmEnterExitClips[2];
                audio.Play();
            }

        }
    }
}

