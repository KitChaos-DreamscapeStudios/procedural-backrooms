    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Experimental.AI;
    using UnityEngine.AI;
    using System.Threading.Tasks;
    using Unity.AI.Navigation;
    public class Movement3D : MonoBehaviour
    {
        //Make sure the player object has a camera as a child, an empty object as a child set as "Orient" a rigidbody with gravity off and all rotation directions locked, a collider, and a constant downwards force
        public Rigidbody body;
        public float speed;
        public LayerMask ground;
        public bool isOnGround;
        float LR;
        public float FB;
        public bool sprinting;
        public GameObject Orient;
        public GameObject cam;
        float TimerBreathe;
        float TimerBob;
        public Inventory inventory;
        public float SoundLevel;
        public NavMeshSurface nav;
    public float SpeedBoost;
        #region Stolen Camera Script
        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -360F;
        public float maximumX = 360F;

        public float minimumY = -60F;
        public float maximumY = 60F;

        float rotationY = 0F;
        #endregion
        // Start is called before the first frame update
    
        void Start()
        {
            //BuildNav();
            body = GetComponent<Rigidbody>();
            DontDestroyOnLoad(this);
            //Hit = GetComponent<AudioSource>();
           // Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level1"); //CHEATING REMOVE LATER
        }
            TimerBreathe += Time.deltaTime;
            TimerBob += (Time.deltaTime * 8)*speed/10;
            if (!inventory.InventScreen.activeSelf)
            {
                if (((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && body.velocity != new Vector3(0, 0, 0)) && GetComponent<PlayerStats>().Stamina > 1)
                {
                    sprinting = true;
                }
                else
                {

                    sprinting = false;
                }
                if (sprinting)
                {
                    speed = 20 + SpeedBoost;
                    SoundLevel = 85;
                }
                else
                {
                speed = 10 + SpeedBoost; ;
                    SoundLevel = 55;
                }
                if (Input.GetKey(KeyCode.C))
                {
                    cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, -2.5f, cam.transform.localPosition.z);
                    gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
               
               
                
                        speed = 5 + SpeedBoost;
                        SoundLevel = 25;
                
                
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                    if (FB != 0)
                    {
                        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, Mathf.Sin(TimerBob));
                    }
                    else
                    {
                        cam.transform.localPosition = new Vector3(0, Mathf.Sin(TimerBreathe) * 0.13f, 0);
                        SoundLevel = 35;
                    }
                }

                #region Stolen Camera Script
                if (axes == RotationAxes.MouseXAndY)
                {
                    //Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    float rotationX = Orient.transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
               
                        Camera.main.transform.eulerAngles = new Vector3(-rotationY, rotationX, Camera.main.transform.eulerAngles.z);

                                    Orient.transform.eulerAngles = new Vector3(0, rotationX, 0);
                }
                else if (axes == RotationAxes.MouseX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }
                #endregion
                LR = Input.GetAxisRaw("Horizontal");
                FB = Input.GetAxisRaw("Vertical");
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
       
        
            body.angularVelocity = new Vector3(0, 0, 0);
            if(FB == 0 && LR == 0)
            {
                body.velocity = new Vector3(0, body.velocity.y, 0);
                if (isOnGround)
                {

                }
            
            }
        }
        private void FixedUpdate()
        {

            body.velocity =new Vector3((Orient.transform.forward * (FB * speed) + Orient.transform.right * (LR * speed)).x, body.velocity.y, (Orient.transform.forward * (FB * speed) + Orient.transform.right * (LR * speed)).z);
       
        }
  
    }
