using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumulatorFaceState
{
    public enum FaceDetecState
    {
        LEFT,
        CENTER,
        RIGHT
    }

    public enum FaceVerticalState
    {
        RUN,
        IDLE
    }

    public static FaceDetecState FaceState;
    public static FaceVerticalState FaceRunState;
}
