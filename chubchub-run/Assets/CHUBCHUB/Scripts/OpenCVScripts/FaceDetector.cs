using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Demo;

public class FaceDetector : MonoBehaviour
{
    WebCamTexture _webCamTexture;
    CascadeClassifier _cascade;
    OpenCvSharp.Rect _myface;

    void Awake()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();
        _cascade = new CascadeClassifier(System.IO.Path.Combine(Application.dataPath, "haarcascade_frontalface_default.xml"));
    }

    void Update()
    {
        //GetComponent<Renderer>().material.mainTexture = _webCamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);
        //FindNewFace(frame);
        Display(frame);
    }

    private void FindNewFace(Mat frame)
    {
        var faces = _cascade.DetectMultiScale(frame, 1.1f, 2, HaarDetectionType.ScaleImage);
        if (faces.Length >= 1)
        {
            //Debug.Log(faces[0].Location);
            _myface = faces[0];
        }
    }

    private void Display(Mat frame)
    {
        var faces = _cascade.DetectMultiScale(frame, 1.1f, 2, HaarDetectionType.ScaleImage);
        if (_myface != null)
        {
            _myface = faces[0];
            frame.Rectangle(_myface, new Scalar(250, 0, 0), 2);
        }
        Texture _newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = _newtexture;
    }
}
