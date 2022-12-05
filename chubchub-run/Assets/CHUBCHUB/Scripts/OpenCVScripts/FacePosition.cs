using UnityEngine;
using OpenCvSharp.FaceLocationOnly;

public class FacePosition
{
    private int _yAxisPrivious = 0;
    private int _yAxisCurrent = 0;

    public void CheckDirectionX()
    {
        if (FaceDetector<WebCamTexture>._faceLocationX > 0 && FaceDetector<WebCamTexture>._faceLocationX < 100)
        {
            EnumulatorFaceState.FaceState = EnumulatorFaceState.FaceDetecState.RIGHT;
        }
        else if (FaceDetector<WebCamTexture>._faceLocationX > 100 && FaceDetector<WebCamTexture>._faceLocationX < 150)
        {
            EnumulatorFaceState.FaceState = EnumulatorFaceState.FaceDetecState.CENTER;
        }
        else if (FaceDetector<WebCamTexture>._faceLocationX > 150)
        {
            EnumulatorFaceState.FaceState = EnumulatorFaceState.FaceDetecState.LEFT;
        }
    }

    public void CheckDirectionY()
    {

        int _yAxisCurrent = FaceDetector<WebCamTexture>._faceLocationY;
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

    public int GetRawDataFaceLocationX()
    {
        return FaceDetector<WebCamTexture>._faceLocationX;
    }

    public int GetRawDataFaceLocationY()
    {
        return FaceDetector<WebCamTexture>._faceLocationY;
    }

}
