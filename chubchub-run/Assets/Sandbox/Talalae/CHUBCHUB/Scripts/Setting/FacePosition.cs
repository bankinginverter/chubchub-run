using UnityEngine;

public class FacePosition
{
    private float _yAxisPrivious = 0;
    private float _yAxisCurrent = 0;

    public static void CheckDirectionX(float x_index)
    {
        if (x_index > 0 && x_index  < 300)
        {
            EnumulatorFaceState.FaceState = EnumulatorFaceState.FaceDetecState.RIGHT;
        }
        else if (x_index  > 300 && x_index  < 700)
        {
            EnumulatorFaceState.FaceState = EnumulatorFaceState.FaceDetecState.CENTER;
        }
        else if (x_index > 700)
        {
            EnumulatorFaceState.FaceState = EnumulatorFaceState.FaceDetecState.LEFT;
        }
    }

    public void CheckDirectionY()
    {
        //float _yAxisCurrent = FaceDetector.Instance.DetectedFacePosition.y;

        if (_yAxisPrivious > _yAxisCurrent + 3 || _yAxisPrivious < _yAxisCurrent - 3)
        {
            EnumulatorFaceState.FaceRunState = EnumulatorFaceState.FaceVerticalState.RUN;
            _yAxisPrivious = _yAxisCurrent;
        }
        else if(_yAxisPrivious == _yAxisCurrent)
        {
            EnumulatorFaceState.FaceRunState = EnumulatorFaceState.FaceVerticalState.IDLE;
        }
    }

    // public float GetRawDataFaceLocationX()
    // {
    //    return FaceDetector.Instance.DetectedFacePosition.x;
    // }

    // public float GetRawDataFaceLocationY()
    // {
    //     return FaceDetector.Instance.DetectedFacePosition.y;
    // }

}
