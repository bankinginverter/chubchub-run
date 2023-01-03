using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMovementPlayer : MonoBehaviour
{
    #region Unity Declarations

        public static FixedMovementPlayer Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [Header("Player Movement Section")]

        [SerializeField] private float laneDistanceValue = 2;

        [SerializeField] private float maxForwardSpeed = 10f;
        
        [SerializeField] private float laneChangeCooldown = 0.5f;

        [SerializeField] private float rollCooldown = 1.5f;
        
        public float smoothRateChangeLane = 5f;

        [Header("Player Spawner Section")]
        
        [SerializeField] private GameObject playerMesh;

        [HideInInspector] public int desiredLane;    // Desired Lane's condition 0 is Left lane, 1 is Middle lane and 2 is Right lane. #In hit condition 3 is all lane

        public Transform spawnPoint;
        
        public static float laneDistance;
                
        private float lastLaneChange;

        private float lastRoll;

        private float isRollAnimationTime = 0.75f;

        private float currentSpeed = 0;

        private float runHeight = 1.84f;

        private float rollHeight = 0.5f;

        private bool isGamePlaying = false;

        private bool isRolling = false;

        private bool isHitting = false;

        private string backFrameFaceState;

        private Animator animator;

        private Vector3 direction;

        private CharacterController controller;

        private Coroutine HitObstacleCoroutine;

    #endregion

    #region Unity Methods

        private void Start()
        {
            laneDistance = laneDistanceValue;

            animator = playerMesh.GetComponent<Animator>();

            controller = GetComponent<CharacterController>();

            currentSpeed = 0;

            isGamePlaying = false;

            InitialSetupPlayer();
        }

        private void Update() 
        {
            direction.z = currentSpeed;

            animator.SetFloat("VelocityZ", currentSpeed/maxForwardSpeed);

            switch(EnumulatorFaceState.FaceState)
            {
                case EnumulatorFaceState.FaceDetecState.LEFT:

                    ChangeLaneToWithCam(EnumulatorFaceState.FaceDetecState.LEFT);

                    break;

                case EnumulatorFaceState.FaceDetecState.CENTER:

                    ChangeLaneToWithCam(EnumulatorFaceState.FaceDetecState.CENTER);

                    break;

                case EnumulatorFaceState.FaceDetecState.RIGHT:

                    ChangeLaneToWithCam(EnumulatorFaceState.FaceDetecState.RIGHT);

                    break;
            }

            if(isGamePlaying)
            {
                if(Input.GetKeyDown(KeyCode.A))
                {
                    ChangeLaneTo("LEFT");
                } 

                if(Input.GetKeyDown(KeyCode.S))
                {
                    ActionRoll();
                }

                if(Input.GetKeyDown(KeyCode.D))
                {
                    ChangeLaneTo("RIGHT");
                }

                if(Input.GetKeyDown(KeyCode.B))
                {
                    EndGame();
                }

                if(Input.GetKeyDown(KeyCode.M))
                {
                    PlayerManager.Instance.LoadPlayer();
                }

                if(Input.GetKeyDown(KeyCode.N))
                {
                    PlayerManager.Instance.SavePlayerGameplay();
                }
            }

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            switch(desiredLane)
            {
                case 0:

                    targetPosition += Vector3.left * laneDistance;

                    break;

                case 2:

                    targetPosition += Vector3.right * laneDistance;

                    break;
            }

            controller.Move(direction * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothRateChangeLane * Time.fixedDeltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) 
        {
            if(hit.transform.tag == "Obstacle" && isHitting == false)
            {
                hit.transform.GetComponent<BoxCollider>().isTrigger = true;

                Destroy(hit.gameObject);
                
                int.TryParse(hit.transform.name[0].ToString(), out int hitIndex) ;
        
                if(hitIndex != 3)
                {
                    HitObstacleCoroutine = StartCoroutine(HitObstacle(hitIndex));      
                }
                else
                {
                    HitObstacleCoroutine = StartCoroutine(HitObstacle(desiredLane));
                }
   
            }
        }

    #endregion

    #region Private Methods

        public void InitialSetupPlayer()
        {
            desiredLane = 1;

            transform.position = spawnPoint.position;

            backFrameFaceState = null;

            StartGame();
        }

        public void StartGame()
        {
            isGamePlaying = true;

            currentSpeed = maxForwardSpeed;
        }

        public void EndGame()
        {
            isGamePlaying = false;

            currentSpeed = 0;

            animator.Play("FinishGame");

            PlayerManager.Instance.SaveGameAfterEndGame();
        }

        public void ActionRoll()
        {
            if(isHitting)
            {
                return;
            }

            if(Time.time - lastRoll <= rollCooldown)
            {
                return;
            }

            isRolling = true;

            controller.height = rollHeight;
            
            controller.center = new Vector3 (0, -0.75f, 0);

            Invoke("setRollBack", isRollAnimationTime);

            animator.Play("Roll");

            lastRoll = Time.time;
        }

        private void setRollBack()
        {
            isRolling = false;

            controller.height = runHeight;

            controller.center = new Vector3 (0, 0, 0);
        }

        public void ChangeLaneToWithCam(EnumulatorFaceState.FaceDetecState index)
        {
            if(isRolling || isHitting)
            {
                return;
            }

            if(Time.time - lastLaneChange <= laneChangeCooldown)
            {
                return;
            }

            switch(index)
            {
                case EnumulatorFaceState.FaceDetecState.LEFT:

                    desiredLane = 0;
                    
                    AnimationChangeLaneController("LEFT");

                    break;
                
                case EnumulatorFaceState.FaceDetecState.CENTER:

                    desiredLane = 1;

                    AnimationChangeLaneController("CENTER");

                    break;

                case EnumulatorFaceState.FaceDetecState.RIGHT:

                    desiredLane = 2;

                    AnimationChangeLaneController("RIGHT");

                    break;

                default:

                    return;
            }

            lastLaneChange = Time.time;
        }

        public void AnimationChangeLaneController(string currentState)
        {
            if(backFrameFaceState == null)
            {
                backFrameFaceState = currentState;

                return;
            }

            if(currentState != backFrameFaceState)
            {
                switch(backFrameFaceState)
                {
                    case "LEFT":

                        animator.Play("RightLane");

                        backFrameFaceState = currentState;

                        break;

                    case "CENTER":
                        
                        switch(currentState)
                        {
                            case "LEFT":

                                animator.Play("LeftLane");

                                break;

                            case "RIGHT":

                                animator.Play("RightLane");

                                break;
                        }

                        backFrameFaceState = currentState;

                        break;

                    case "RIGHT":

                        animator.Play("LeftLane");

                        backFrameFaceState = currentState;

                        break;
                }
            }
        }

        public void ChangeLaneTo(string index)
        {
            if(isRolling || isHitting)
            {
                return;
            }

            if(Time.time - lastLaneChange <= laneChangeCooldown)
            {
                return;
            }

            switch(index)
            {
                case "LEFT":

                    desiredLane--;

                    if(desiredLane <= -1)
                    {
                        desiredLane = 0;
                    }
                    else
                    {
                        animator.Play("LeftLane");
                    }
                    

                    break;
                
                case "RIGHT":

                    desiredLane++;

                    if(desiredLane >= 3)
                    {
                        desiredLane = 2;
                    }
                    else
                    {
                        animator.Play("RightLane");
                    }

                    break;

                default:

                    return;
            }

            lastLaneChange = Time.time;

        }

        IEnumerator HitObstacle(int hitIndex)
        {
            isHitting = true;
            
            desiredLane = hitIndex;

            animator.Play("FallByObstable");

            currentSpeed = 0;

            yield return new WaitForSeconds(3f);

            if(isGamePlaying)
            {
                currentSpeed = maxForwardSpeed;
            }
            
            yield return new WaitForSeconds(0.25f);

            isHitting = false;
            
            StopCoroutine(HitObstacleCoroutine);
        }

    #endregion
}