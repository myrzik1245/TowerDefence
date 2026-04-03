
using System;
using _Project.Code.Runtime.Gameplay.AI.States;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Utility.Conditions;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.InputService;
using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Configs.Defence;
using _Project.Code.Runtime.Gameplay.DefenceFeature.Objects;
using _Project.Code.Runtime.Utility.Timer;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.Brains
{
    public class BrainsFactory
    {
        private readonly IInputService _inputService;
        private readonly BrainsContext _context;
        private readonly TimerFactory _timerFactory;

        public BrainsFactory(DIContainer container)
        {
            _inputService = container.Resolve<IInputService>();
            _context = container.Resolve<BrainsContext>();
            _timerFactory = container.Resolve<TimerFactory>();
        }

        public IBrain CreateTurretAIBrain(Turret turret, TurretConfig config)
        {
            RotateToTargetState rotateToTargetState = new (turret, turret, turret);
            TargetPositionAttackState attackState = new(turret, turret);
            EmptyState emptyState = new();
            
            TimerService attackCooldownTimer = _timerFactory.Create(config.Cooldown);
            
            IDisposable attackEnterSubscribe = attackState.Entered.Subscribe(attackCooldownTimer.Start);
            IDisposable emptyEnteredSubscribe = emptyState.Entered.Subscribe(attackCooldownTimer.Start);

            ICondition rotateToEmpty = new CompositeCondition(
                new FuncCondition(() =>
                {
                    if (turret.TryGetData(BlackboardKeys.Target, out Transform target))
                    {
                        Vector3 direction =  target.position - turret.Position.Value;
                        return Quaternion.Angle(turret.Rotation.Value, Quaternion.LookRotation(direction)) <= 5;
                    }

                    return false;
                }));

            ICondition emptyToRotate = new CompositeCondition(
                new FuncCondition(() =>
                {
                    if (turret.TryGetData(BlackboardKeys.Target, out Transform target))
                    {
                        Vector3 direction =  target.position - turret.Position.Value;
                        return Quaternion.Angle(turret.Rotation.Value, Quaternion.LookRotation(direction)) > 5;
                    }

                    return false;
                }));
            
            ICondition emptyToAttack = new CompositeCondition( 
                new FuncCondition(() => turret.TryGetData(BlackboardKeys.Target, out Transform target)),
                new FuncCondition(() => attackCooldownTimer.IsDone.Value));

            ICondition attackToEmpty = new CompositeCondition( 
                LogicOperation.Or,
                new FuncCondition(() => turret.TryGetData(BlackboardKeys.Target, out Transform target) == false),
                new FuncCondition(() => attackCooldownTimer.IsDone.Value == false));
            
            
            AIStateMachine stateMachine = new AIStateMachine(
                attackCooldownTimer,
                attackEnterSubscribe,
                emptyEnteredSubscribe);

            stateMachine
                .AddState(emptyState)
                .AddState(rotateToTargetState)
                .AddState(attackState);

            stateMachine
                .AddTransition(emptyState, rotateToTargetState, emptyToRotate)
                .AddTransition(rotateToTargetState, emptyState, rotateToEmpty)
                .AddTransition(emptyState, attackState, emptyToAttack)
                .AddTransition(attackState, emptyState, attackToEmpty);

            IBrain brain = new StateMachineBrain(stateMachine);
            
            _context.Register(turret, brain, new FuncCondition(() => turret.IsDestroyed));

            return brain;
        }
        
        public IBrain CreateShooterAIBrain(Shooter shooter, ShooterConfig config)
        {
            TimerService attackCooldownTimer = _timerFactory.Create(config.AttackCooldown);
            
            EmptyState emptyState = new();
            MoveToTargetState moveToTargetState = new(shooter, shooter);
            RotateAlongMovementDirectionState rotateAlongMovementDirectionState = new(shooter, shooter);

            TargetPositionAttackState attackState = new(shooter, shooter);
            
            AIParallelState movementState = new(moveToTargetState, rotateAlongMovementDirectionState);

            ICondition emptyToMovement = new CompositeCondition(
                new FuncCondition(() =>
                {
                    if (shooter.TryGetData(BlackboardKeys.Target, out Transform target))
                        return Vector3.Distance(target.position, shooter.Position.Value) > config.Range;

                    return false;
                }));

            ICondition movementToEmpty = new CompositeCondition(
                new FuncCondition(() =>
                {
                    if (shooter.TryGetData(BlackboardKeys.Target, out Transform target))
                        return Vector3.Distance(shooter.Position.Value, target.position) <= config.Range;

                    return true;
                }));

            ICondition emptyToAttack = new CompositeCondition(
                new FuncCondition(() => attackCooldownTimer.IsDone.Value),
                new FuncCondition(() =>
                {
                    if (shooter.TryGetData(BlackboardKeys.Target, out Transform target))
                        return Vector3.Distance(shooter.Position.Value, target.position) <= config.Range;

                    return false;
                }));

            ICondition attackToEmpty = new CompositeCondition(
                new FuncCondition(() => attackCooldownTimer.IsDone.Value == false));
            
            IDisposable attackEnterSubscribe = attackState.Entered.Subscribe(attackCooldownTimer.Start);
            IDisposable emptyEnteredSubscribe = emptyState.Entered.Subscribe(attackCooldownTimer.Start);
            
            AIStateMachine stateMachine = new(
                attackEnterSubscribe,
                attackCooldownTimer,
                emptyEnteredSubscribe);

            stateMachine
                .AddState(emptyState)
                .AddState(movementState)
                .AddState(attackState);

            stateMachine
                .AddTransition(emptyState, movementState, emptyToMovement)
                .AddTransition(movementState, emptyState, movementToEmpty)
                .AddTransition(emptyState, attackState, emptyToAttack)
                .AddTransition(attackState, emptyState, attackToEmpty);

            IBrain brain = new StateMachineBrain(stateMachine);

            _context.Register(shooter, brain, new FuncCondition(() => shooter.IsDead.Value));

            return brain;
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
            InputPositionAttackState inputPositionAttackState = new(tower, _inputService);

            ICondition emptyToAttack = new CompositeCondition(
                new FuncCondition(() => _inputService.Attack.Down));

            ICondition attackToEmpty = new CompositeCondition(
                new FuncCondition(() => _inputService.Attack.Down == false));

            AIStateMachine stateMachine = new();

            stateMachine
                .AddState(emptyState)
                .AddState(inputPositionAttackState);

            stateMachine
                .AddTransition(emptyState, inputPositionAttackState, emptyToAttack)
                .AddTransition(inputPositionAttackState, emptyState, attackToEmpty);

            IBrain brain = new StateMachineBrain(stateMachine);

            _context.Register(tower, brain, new FuncCondition(() => tower.IsDead.Value));

            return brain;
        }
    }
}