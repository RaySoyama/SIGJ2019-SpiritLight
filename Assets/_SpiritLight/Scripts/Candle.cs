using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] public bool isLit = true;

    public int id;
    public GameObject flame;
    public void Extinguish() {
        if (isLit) {
            Debug.Log("Extinguished!");
            CandleManager.CandleMan.BlowCandle(this);
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
