using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScripts : MonoBehaviour
{
    private FacePosition _facePosition;

    void Start()
    {
        _facePosition = new FacePosition();    
    }

    private void Update()
    {
        _facePosition.CheckDirectionY();
    }

}
