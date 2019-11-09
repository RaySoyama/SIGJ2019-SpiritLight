﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Audio;

    [Header("Foot Steps")]
    [SerializeField]
    private AudioSource FootStepAudioSource;

    [SerializeField]
    private List<AudioClip> FootstepWoodClips;
    [SerializeField]
    private List<AudioClip> FootstepGrassClips;
    [SerializeField]
    private List<AudioClip> FootstepRockClips;

    private int PreviousFootstep;


    [Header("SFX")]

    [SerializeField]
    private AudioSource PlayerSFXAudioSource;

    [SerializeField]
    private List<AudioClip> PlayerSFXClips;

    [Header("Realm SFX")]

    [SerializeField]
    private AudioSource RealmAudioSource;

    [SerializeField]
    private List<AudioClip> RealmEnterExitClips;

    void Awake()
    {
        if (Audio == null)
        {
            Audio = this;
        }
    }


    public void PlayFootstep()
    {
        int idx = -1;

        switch (WorldMachine.World.CurrentWalkingSurface)
        {
            case WorldMachine.PlayerWalkingSurface.Wood:

                while (idx == PreviousFootstep)
                {
                    idx = Random.Range(0, FootstepWoodClips.Count - 1);

                    if (FootstepWoodClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstep = idx;

                FootStepAudioSource.clip = FootstepWoodClips[idx];
                FootStepAudioSource.Play();

                break;

            case WorldMachine.PlayerWalkingSurface.Grass:

                while (idx == PreviousFootstep)
                {
                    idx = Random.Range(0, FootstepGrassClips.Count - 1);

                    if (FootstepGrassClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstep = idx;

                FootStepAudioSource.clip = FootstepGrassClips[idx];
                FootStepAudioSource.Play();

                break;
            case WorldMachine.PlayerWalkingSurface.Rock:

                while (idx == PreviousFootstep)
                {
                    idx = Random.Range(0, FootstepRockClips.Count - 1);

                    if (FootstepRockClips.Count < 3)
                    {
                        break;
                    }
                }

                PreviousFootstep = idx;

                FootStepAudioSource.clip = FootstepRockClips[idx];
                FootStepAudioSource.Play();

                break;
        }
    }


    //Player SFX system

    public void PlayRealEnter()
    {
        RealmAudioSource.clip = RealmEnterExitClips[0];
        RealmAudioSource.Play();
    }

    public void PlayRealmExit()
    {
        RealmAudioSource.clip = RealmEnterExitClips[1];
        RealmAudioSource.Play();
    }
}
