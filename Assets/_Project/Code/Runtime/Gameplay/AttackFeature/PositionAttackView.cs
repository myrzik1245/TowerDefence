using _Project.Code.Runtime.Gameplay.ViewCore;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public class PositionAttackView : CharacterViewBase
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private ParticleSystem _vfxPrefab;
        private IPositionAttack _positionAttack;

        private List<IDisposable> _disposables = new();

        protected override void Initialize()
        {
            _positionAttack = GetComponent<IPositionAttack>();
            
            _positionAttack.Attacked.Subscribe(OnAttacked);
        }

        private void OnAttacked(PositionAttackProcess attackProcess)
        {
            StartCoroutine(AttackProcess(attackProcess.StartPosition, attackProcess.EndPosition, attackProcess.Progress));
        }
        
        private void OnDisable()
        {
            foreach(IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private IEnumerator AttackProcess(Vector3 startPosition, Vector3 endPosition, IReadOnlyReactiveVariable<float> progress)
        {
            GameObject projectile = Instantiate(_projectilePrefab, startPosition, Quaternion.LookRotation(endPosition - startPosition));
            
            while (Vector3.Distance(projectile.transform.position, endPosition) > 0.1f)
            {
                projectile.transform.position = Vector3.Lerp(startPosition, endPosition, progress.Value);
                
                yield return null;
            }
            
            Destroy(projectile.gameObject);
            Instantiate(_vfxPrefab, endPosition, Quaternion.identity);
        }
    }
}
