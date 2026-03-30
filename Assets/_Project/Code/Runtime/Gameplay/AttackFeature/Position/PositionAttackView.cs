using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.ViewCore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Position
{
    public class PositionAttackView : CharacterViewBase
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private ParticleSystem _vfxPrefab;
        private IPositionAttack _positionAttack;

        private List<PositionAttackProcessView> _processViews = new();

        protected override void Initialize()
        {
            _positionAttack = GetComponent<IPositionAttack>();
            
            _positionAttack.PositionAttacked.Subscribe(OnAttacked);
        }

        private void OnAttacked(PositionAttackProcess attackProcess)
        {
            PositionAttackProcessView processView = new(attackProcess, _projectilePrefab, _vfxPrefab);
            _processViews.Add(processView);
            processView.Initialize();
        }
        
        private void OnDisable()
        {
            foreach(IDisposable processView in _processViews)
                processView.Dispose();
        }
    }
}
