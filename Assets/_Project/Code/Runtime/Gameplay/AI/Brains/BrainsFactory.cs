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
            MoveToTargetState moveToTargetState = new(bomber, bomber);
            RotateAlongMovementDirectionState rotateAlongMovementDirectionState = new(bomber, bomber);
            
            ExplosionState explosionState = new(bomber, bomber, bomber);

            AIParallelState moveParallelState = new(
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
            
            AIStateMachine stateMachine = new();

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
            EmptyState emptyState = new();
            PositionAttackState positionAttackState = new(tower, _inputService);

            ICondition emptyToAttack = new CompositeCondition(
                new FuncCondition(() => _inputService.Attack.Down));

            ICondition attackToEmpty = new CompositeCondition(
                new FuncCondition(() => _inputService.Attack.Down == false));
            
            AIStateMachine stateMachine = new();
            
            stateMachine
                .AddState(emptyState)
                .AddState(positionAttackState);

            stateMachine
                .AddTransition(emptyState, positionAttackState, emptyToAttack)
                .AddTransition(positionAttackState, emptyState, attackToEmpty);
            
            IBrain brain = new StateMachineBrain(stateMachine);
            
            _context.Register(tower, brain, new FuncCondition(() => tower.IsDead.Value));
            
            return brain;
        }
    }
}
