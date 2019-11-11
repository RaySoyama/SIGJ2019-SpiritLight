using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] public bool isLit = true;

    public int id;
    public GameObject flame;

    [SerializeField]
    private AudioSource audioPlayer;

    [SerializeField]
    private AudioClip candleAmbient;
    
    [SerializeField]
    private AudioClip candleExtinguish;



    public void Extinguish() {
        if (isLit) {
            Debug.Log("Extinguished!");
            CandleManager.CandleMan.BlowCandle(this);

            audioPlayer.loop = false;
            audioPlayer.clip = candleExtinguish;
            audioPlayer.Play(); 


        }
        else {
            Debug.Log("Already extinguished.");
        }
        isLit = false;
    }

    public void Light() {
        isLit = true;
    }
}
