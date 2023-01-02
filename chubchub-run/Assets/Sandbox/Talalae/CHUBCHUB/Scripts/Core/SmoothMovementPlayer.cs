using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovementPlayer : MonoBehaviour
{
    #region Unity Declarations

        [SerializeField] private float forwardSpeedValue = 10f;

        [SerializeField] private float sideSpeedValue = 15F;

        [SerializeField] private float offset = 4f;

        [SerializeField] private Transform spawnPoint;

        private float _horizontalInput;

        private float forwardPosition;

    #endregion

    #region Unity Methods

        private void Start()
        {
            InitialSetupPlayer();
        }

        private void Update() 
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }
        
        private void FixedUpdate() 
        {
            forwardPosition += forwardSpeedValue * Time.deltaTime;

            float horizontalMove = _horizontalInput * sideSpeedValue * Time.deltaTime;

            float rawHorizontalPostion = transform.position.x + horizontalMove; 

            float clampedHorizontalPostion = Mathf.Clamp(rawHorizontalPostion, -offset, offset);

            transform.position = new Vector3(clampedHorizontalPostion, transform.position.y, forwardPosition);
        }

    #endregion

    #region Private Methods

        public void InitialSetupPlayer()
        {
            forwardPosition = 0;

            transform.position = spawnPoint.position;
        }

    #endregion
}
