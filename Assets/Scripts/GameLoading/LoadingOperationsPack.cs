using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace GameLoading.LoadingOperations
{
    public sealed class LoadingOperationsPack : LoadingOperation
    {
        public override float Progress => _progress;

        [SerializeField] private List<LoadingOperation> _operations;
        [SerializeField] private ExecutionMethod _executionMethod;

        private float _progress;

        protected override void OnBegin()
        {
            StartCoroutine(_executionMethod switch
            {
                ExecutionMethod.Parallel => ParallelExecutionCoroutine(),
                ExecutionMethod.Sequently => SequentlyExecutionCoroutine(),
                _ => null
            });
        }

        private IEnumerator ParallelExecutionCoroutine()
        {
            var operationCount = _operations.Count;
            _operations.ForEach(operation => operation.Begin());

            while (_operations.Any(operation => !operation.IsDone))
            {
                _progress = _operations.Sum(operation => operation.Progress) / operationCount;
                yield return new WaitForEndOfFrame();
            }

            _progress = 1f;
            SetStateDone();

            yield break;
        }

        private IEnumerator SequentlyExecutionCoroutine()
        {
            var operationsCount = _operations.Count;
            var completedOperationsCount = 0;

            foreach (var operation in _operations)
            {
                operation.Begin();

                while (!operation.IsDone)
                {
                    _progress = (operation.Progress + completedOperationsCount) / operationsCount;
                    yield return new WaitForEndOfFrame();
                }

                ++completedOperationsCount;
            }

            _progress = 1f;
            SetStateDone();

            yield break;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        public enum ExecutionMethod
        {
            Parallel = 0,
            Sequently = 1
        }
    }
}