using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitnessScript : MonoBehaviour
{
    [SerializeField]
    float delay = 2f;

    public void Watch()
    {
        Invoke("TurnMeOff", delay);
    }

    private void TurnMeOff()
    {
        GameManager.instance.RemoveTarget(transform);
    }

}
