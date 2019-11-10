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


        for (int i = 0; i < TarCandles.Count; i++)
        {
            if (TarCandles[i].id == thingy.id)
            {
                TarCandles[i].isLit = false;
                TarCandles[i].flame.SetActive(false);
                TarCandles.Remove(TarCandles[i]);
                i--;
            }
        }

        if (TarCandles.Count == 0)
        { 
            //Unlock Tar Area
            
            

        }   
    }

}
