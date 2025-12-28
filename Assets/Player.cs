using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        input = new PlayerInputSet();
        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "Idle State");
        moveState = new Player_MoveState(this, stateMachine, "Move State");
    }

    private void OnEnable()
    {
        input.Enable();

        //input.Player.Movement.started - input just began!
        //input.Player.Movement.performed - input is performed!
        //input.Player.Movement.canceled - input stops, when you release the key!

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
    }
}
