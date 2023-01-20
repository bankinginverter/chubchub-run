using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class FaceDetector : MonoBehaviour
{
    #region Unity Declarations

        private WebCamTexture _webCamTexture;
        
        private CascadeClassifier cascade;

        // OpenCvSharp.Rect MyFace; // Display Cast

        public static FaceDetector Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private Coroutine FaceDetectorCoroutine;

    #endregion

    #region Private Methods

        void findNewFace(Mat frame)
        {
            var faces = cascade.DetectMultiScale(frame, 1.1, 2,HaarDetectionType.ScaleImage);

            if(faces.Length >= 1)
            {
                FacePosition.CheckDirectionX(faces[0].Location.X);

                // MyFace = faces[0]; // Display Cast
            }
        }

        public void SetupCamera()
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            _webCamTexture = new WebCamTexture(devices[0].name);
            
            _webCamTexture.Play();
            
            cascade = new CascadeClassifier(Application.dataPath+ @"/haarcascade_frontalface_default.xml");

            FaceDetectorCoroutine = StartCoroutine(FaceDetectorCameraActive());

            // GetComponent<Renderer>().material.mainTexture = _webCamTexture; // Display Cast
        }

        public void DeactivateCamera()
        {
            _webCamTexture.Stop();

            StopCoroutine(FaceDetectorCoroutine);
        }

        IEnumerator FaceDetectorCameraActive()
        {
            while(true)
            {
                Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

                findNewFace(frame); 
                
                // display(frame); // Display Cast

                yield return new WaitForSeconds(.15f);
            }
        }

        // Display Cast Function
        // void display(Mat frame)
        // {
        //     if(MyFace != null)
        //     {
        //         frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
        //     }

        //     Texture newTexture = OpenCvSharp.Unity.MatToTexture(frame);

        //     GetComponent<Renderer>().material.mainTexture = newTexture;
        // }

    #endregion
}