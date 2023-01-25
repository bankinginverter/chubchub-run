using UnityEngine;

public class FacePosition
{
    private static float _yAxisPrivious = 0;

    private static float _yAxisCurrent = 0;

    public static void CheckDirection(OpenCvSharp.Point index)
    {
        CheckDirectionX(index.X);

        CheckDirectionY(index.Y);
    }

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

    public static void CheckDirectionY(float y_index)
    {
        _yAxisCurrent = y_index;

        if (_yAxisPrivious > _yAxisCurrent + 50 || _yAxisPrivious < _yAxisCurrent - 50)
        {
            EnumulatorFaceState.FaceRunState = EnumulatorFaceState.FaceVerticalState.RUN;

            _yAxisPrivious = _yAxisCurrent;
        }
        else
        {
            EnumulatorFaceState.FaceRunState = EnumulatorFaceState.FaceVerticalState.IDLE;
        }
    }
}
