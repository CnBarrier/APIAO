using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    [Header("人物")]
    public CharacterController controller;
    public Vector3 Velocity;
    public Vector3 targetPosition;
    public float Speed = 5f;
    private readonly float Gravity = -9.81f;
    private readonly float JumpHeight = 1f;
    public bool isGrounded;
    
    [Space]
    [Header("道路判断")]
    [SerializeField] private int CurrentLand = 0; // 左0 中1 右2
    [SerializeField] private float LandSpace = 2f;
 
    void Start()
    {
        
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        isGrounded = controller.isGrounded;

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * Mathf.Abs(Gravity) * 2.0f);
        }
        if(Input.GetKeyDown(KeyCode.A) && isGrounded)
        {
            CurrentLand = Mathf.Clamp(CurrentLand - 1, -1, 1);
        }
        if(Input.GetKeyDown(KeyCode.D) && isGrounded)
        {
            CurrentLand = Mathf.Clamp(CurrentLand + 1, -1, 1);
        }
        
        Vector3 Target = new(CurrentLand * LandSpace, transform.position.y, transform.position.z);
        Vector3 Direction = Target - transform.position;
        controller.Move(Speed * Time.deltaTime * (Direction + transform.forward)); 
        controller.Move(Velocity * Time.deltaTime);

        if (!isGrounded)
        {
            Velocity.y += Gravity * Time.deltaTime;
        }
    }
}
