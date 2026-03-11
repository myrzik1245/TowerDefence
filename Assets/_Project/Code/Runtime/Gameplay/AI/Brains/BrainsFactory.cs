using _Project.Code.Runtime.Gameplay.AI.States;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Utility.Conditions;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.InputService;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.Brains
{
    public class BrainsFactory
    {
        private readonly IInputService _inputService;
        private readonly BrainsContext _context;
        
        public BrainsFactory(DIContainer container)
        {
            _inputService =  container.Resolve<IInputService>();
            _context = container.Resolve<BrainsContext>();
        }

        public IBrain CreateBomberAIBrain(Bomber bomber)
        {
            MoveToTargetState moveToTargetState = new MoveToTargetState(bomber, bomber);
            RotateAlongMovementDirectionState rotateAlongMovementDirectionState
                = new RotateAlongMovementDirectionState(bomber, bomber);
            
            ExplosionState explosionState = new ExplosionState(bomber, bomber, bomber);

            AIParallelState moveParallelState = new AIParallelState(
                moveToTargetState,
                rotateAlongMovementDirectionState);
            
            ICondition moveToExplosionCondition = new CompositeCondition(
                new FuncCondition(() => 
                {
                    if (bomber.TryGetData(BlackboardKeys.Target, out Transform target))
                        if (target != null)
                            return Vector3.Distance(target.position, bomber.Position.Value) < 1f;
                    
                    return false;
                }));
            
            AIStateMachine stateMachine = new AIStateMachine();

            stateMachine
                .AddState(moveParallelState)
                .AddState(explosionState);
            
            stateMachine
                .AddTransition(moveParallelState, explosionState, moveToExplosionCondition);
            
            IBrain brain = new StateMachineBrain(stateMachine);
            
            _context.Register(bomber, brain, new FuncCondition(() => bomber.IsDead.Value));
            
            return brain;
        }
        
        public IBrain CreateInputTowerBrain(Tower tower)
        {
            PositionAttackState positionAttackState = new PositionAttackState(tower, _inputService);
            
            AIStateMachine stateMachine = new AIStateMachine();
            stateMachine
                .AddState(positionAttackState);
            
            IBrain brain = new StateMachineBrain(stateMachine);
            
            _context.Register(tower, brain, new FuncCondition(() => tower.IsDead.Value));
            
            return brain;
        }
    }
}
