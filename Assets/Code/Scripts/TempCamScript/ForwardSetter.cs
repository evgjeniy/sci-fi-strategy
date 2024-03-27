using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardSetter : MonoBehaviour
{
    void Start()
    {
        SustainTheStrain.Units.HPBar._camForward = transform.forward;
    }
}
