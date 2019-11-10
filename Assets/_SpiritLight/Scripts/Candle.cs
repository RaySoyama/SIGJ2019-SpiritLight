using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public bool isLit = true;

    void OnTriggerEnter(Collider collider) {
        Debug.Log("Trigger");
        // if ((collider.tag == "RealityPlayer" && !WorldMachine.World.IsInRealm) ||
        //     (collider.tag == "RealmPlayer" && WorldMachine.World.IsInRealm)) {
        //         Debug.Log("Collision!");
        // }
    }
}
