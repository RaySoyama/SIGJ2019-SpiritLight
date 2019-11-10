using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] bool isLit = true;

    public void Extinguish() {
        if (isLit) {
            Debug.Log("Extinguished!");
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
