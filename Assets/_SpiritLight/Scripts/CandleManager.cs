using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleManager : MonoBehaviour
{
    [SerializeField]
    private List<Candle> TarCandles;

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
        foreach (Candle cand in TarCandles)
        {
            
            if (cand.id == thingy.id)
            {
                thingy.flame.SetActive(false);
                TarCandles.Remove(cand);
                break;
            }
            
        }


        if (TarCandles.Count == 0)
        { 
            //Unlock Tar Area
            
            

        }   
    }

}
