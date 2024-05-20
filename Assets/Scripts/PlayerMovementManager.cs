using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    Animator playerAnimator;
    float animatorSpeed;
    bool  stateCrouch;

    [SerializeField] float rotationSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchWalkSpeed;
    [SerializeField] float gravity=-9.81f;
   
    
    public CharacterController playerMovement;
    float moveInputX, moveInputZ;
    Vector3 move;
    Vector3 velocityY;


    Camera mainCamera;
    Vector3 rotatingPlayer;

    
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        mainCamera=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        stateCrouch = false;
    }

    
    void Update()
    {
        RayToScreen();
        KeyInputs();
        Move();
    }

    void KeyInputs()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputZ = Input.GetAxisRaw("Vertical");
        move = moveInputX * Vector3.right + moveInputZ * Vector3.forward;

        animatorSpeed = Mathf.Abs(moveInputX) + Mathf.Abs(moveInputZ);
        playerAnimator.SetFloat("Speed", animatorSpeed);

        if (Input.GetKeyDown(KeyCode.LeftControl) && stateCrouch != true)
        {
            stateCrouch = true;
            playerAnimator.SetBool("StateCrouch", stateCrouch);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && stateCrouch == true)
        {
            stateCrouch = false;
            playerAnimator.SetBool("StateCrouch", stateCrouch);
        }
        
    }
 

    void RayToScreen()
    {
        RaycastHit hit;
        Ray ray=mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hit))
        {
            rotatingPlayer = hit.point;
        }
        RotatePlayerWithMouse();
    }

    void RotatePlayerWithMouse()
    {
        Vector3 lookPos = rotatingPlayer - transform.position;
        lookPos.y = 0;
        var rotation=Quaternion.LookRotation(lookPos);
        Vector3 aimDirection = new Vector3(rotatingPlayer.x, 0, rotatingPlayer.z);

        if (aimDirection!=Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.7f);
        }
    }

    void Move()
    {
        Vector3 axisInput=new Vector3(moveInputX,0,moveInputZ);
        if (axisInput!=Vector3.zero)
        {
           
            playerMovement.Move(move*runSpeed*Time.deltaTime);
        }
        velocityY.y += gravity*Time.deltaTime;
        playerMovement.Move(velocityY * Time.deltaTime);
    }

}
