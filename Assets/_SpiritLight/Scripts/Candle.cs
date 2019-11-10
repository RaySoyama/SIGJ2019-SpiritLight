using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] bool isLit = true;

    public void Extinguish() {
        isLit = false;
    }

    public void Light() {
        isLit = true;
    }
}
