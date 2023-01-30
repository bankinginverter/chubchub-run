using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAnimation : MonoBehaviour
{


    [SerializeField] private float XturnRate = 60f;

    [SerializeField] private float YturnRate = 60f;

    [SerializeField] private float ZturnRate = 60f;
    

    private void Update() 
    {
        transform.Rotate(XturnRate * Time.deltaTime, YturnRate * Time.deltaTime, ZturnRate * Time.deltaTime);
    }
}
