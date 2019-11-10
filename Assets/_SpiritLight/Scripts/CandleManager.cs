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

    public void BlowCandle(Candle candle)
    {
        foreach (Candle cand in TarCandles)
        {
            /*
            if (cand.id == candle.id)
            {
                TarCandles.Remove(cand);
            }
            */
        }


        if (TarCandles.Count == 0)
        { 
            //Unlock Tar Area
            
            

        }   
    }

}
