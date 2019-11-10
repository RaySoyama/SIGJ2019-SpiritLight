using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleManager : MonoBehaviour
{
    [SerializeField]
    private List<Candle> TarCandles;

    [SerializeField] GameObject tarObject;

    public static CandleManager CandleMan;

    void Start()
    {
        if (CandleMan == null)
        {
            CandleMan = this;
        }
    }

    public void BlowCandle(Candle thingy)
    {


        for (int i = 0; i < TarCandles.Count; i++)
        {
            if (TarCandles[i].id == thingy.id)
            {
                thingy.isLit = false;
                thingy.flame.SetActive(false);
                TarCandles.Remove(TarCandles[i]);
                i--;
            }
        }

        if (TarCandles.Count == 0)
        { 
            //Unlock Tar Area
            if (tarObject) {
                Collider collider = tarObject.GetComponentInChildren<BoxCollider>();
                Debug.Log("Tar area unlocked");
                if (collider) {
                    collider.enabled = false;
                }
            }
            

        }   
    }

}
