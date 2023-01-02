using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Unity Declarations

       [SerializeField] private Transform target;

       [SerializeField] private float factorRate;

       private Vector3 offset;

       private float xPositionCamera;

    #endregion

    #region Unity Methods

        private void Start() 
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate() 
        {
            switch(FixedMovementPlayer.Instance.desiredLane)
            {
                case 0:

                    xPositionCamera = -1f;

                    break;

                case 1:

                    xPositionCamera = 0f;

                    break;

                case 2:

                    xPositionCamera = 1f;
                    
                    break;
            }

            Vector3 newPosition = new Vector3(xPositionCamera, transform.position.y, offset.z + target.position.z);
            
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, newPosition.x, factorRate), newPosition.y, newPosition.z);
        }
    
    #endregion
}
