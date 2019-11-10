using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathManager : MonoBehaviour {
    public static BreathManager Breath;


    [SerializeField] float breathLimit = 10.0f;

    public float BreathLimit
    {
        get
        {
            return breathLimit;
        }
    }

    [SerializeField] [ReadOnlyField] float breathMeter;

    public float BreathMeter
    {
        get
        {
            return breathMeter;
        }
    }


    [SerializeField] [Range(0.0f, 1.0f)]
    private float gaspingThreshold = 0.2f;

    public float GaspingThreshold
    {
        get
        {
            return gaspingThreshold;
        }
    }

    [SerializeField] float breathBlowCost = 2.0f;

    [SerializeField] float breathDepletionRate = 1.0f;

    [SerializeField] float breathRecoveryRate = 1.0f;
    
    void Awake() {
      if (Breath == null) {
          Breath = this;
      }  

      breathMeter = breathLimit;
    }
    
    void Update() {
        if (WorldMachine.World.IsInRealm) {
            breathMeter = Mathf.Clamp(breathMeter - Time.deltaTime * breathDepletionRate, 0, breathLimit);

            if (breathMeter == 0) {
                WorldMachine.World.OnExitRealm();
                breathMeter = breathLimit;
            }
        }
        else {
            breathMeter = Mathf.Clamp(breathMeter += Time.deltaTime *breathRecoveryRate, 0, breathLimit);
        }
    }

    public void Blow() {
        Debug.Log("Blow");
        breathMeter = Mathf.Clamp(breathMeter - breathBlowCost, 0, breathLimit);
    }
}
