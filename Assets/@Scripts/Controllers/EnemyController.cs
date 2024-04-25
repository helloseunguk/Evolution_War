using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public enum EnemyState
{
    WALK,
    ATTACK,
    FREZEN,
    BURN,
    DIE,
    STURN,
}
public class EnemyController : BaseController
{
    // Start is called before the first frame update
   public StateMachine stateMachine;

    public EnemyState state{ get; set; }
    public Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

    public IState Walk;
    public IState Die;
    public IState Attack;

    protected override bool Init()
    {
        Walk = new StateWalk();
        Die = new StateDie();
        Attack = new StateAttack();

        dicState.Add(EnemyState.WALK,Walk);
        dicState.Add(EnemyState.DIE, Die);



        stateMachine = new StateMachine(Walk);
        return base.Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
