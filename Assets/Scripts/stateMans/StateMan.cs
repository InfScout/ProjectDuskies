using UnityEngine;

public class StateMan : MonoBehaviour
{
    //states
    private Patrol _patrolState;
    private Chase _chaseState;
    private Attack _attackState;
    private Alert _alertState;

    //states getter
    public Patrol Patrol => _patrolState;
    public Chase Chase => _chaseState;
    public Attack Attack => _attackState;
    public Alert Alert => _alertState;

    //keeping track of state
    //       |
    //       V
    private Istate _currentState;

    [SerializeField] private PlayerChecker playerChecker;
    public PlayerChecker PlayerChecker => playerChecker;

    [SerializeField] private CharecterMover characterMover;
    public CharecterMover CharacterMover => characterMover;
    
    private void Awake()
    {
        _patrolState = new Patrol(this);
        _chaseState = new Chase(this);
        _alertState = new Alert(this);
        _attackState = new Attack(this);
    }

    private void Start()
    {
        ChangeState(_patrolState);
    }
    
    public void ChangeState(Istate newState)
    {

        _currentState?.Exit();
        
        _currentState = newState;

        _currentState.Enter();
    }

    void Update()
    {
        _currentState?.Update();
    }
    
    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
}

