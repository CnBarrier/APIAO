using System.Threading;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    [Header("玩家")]
    public CharacterController controller;
    public Vector3 Velocity;
    public Vector3 targetPosition;
    public float Speed = 5f;
    private readonly float Gravity = -9.81f;
    private readonly float JumpHeight = 1f;
    public int Health = 3;
    public bool isGrounded = true;
    public bool isAlive = true;
    
    [Space]
    [Header("道路判断")]
    [SerializeField] private int CurrentLand = 0; // 左0 中1 右2
    [SerializeField] private float LandSpace = 2f;

    [Space]
    [Header("计时器")]
    public float timer = 60.0f;
 
    void Start()
    {
        
    }

    void Update()
    {
        PlayerMovement();
        PlayerHealth();
        CountDown();
    }

    private void PlayerMovement()
    {
        isGrounded = controller.isGrounded;

        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
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

        if(!isGrounded)
        {
            Velocity.y += Gravity * Time.deltaTime;
        }
    }

    private void PlayerHealth()
    {
        if(Health <= 0)
        {
            Dead();
        }
    }
    
    private void CountDown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            Dead();
            return;
        }
    }

    private void Dead()
    {
        isAlive = false;
        Speed = 0.0f;
        Debug.Log("Player is DEAD now");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Money"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Obstacles"))
        {
            Health -= 1;
            Destroy(other.gameObject);
        }
    }

}
