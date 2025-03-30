using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        private Coroutine _moveCor;

        private const string IsWalkingKey = "IsWalking";

        private bool IsWalking
        {
            set => _animator.SetBool(IsWalkingKey, value);
        }

        private Vector3 Position => transform.position;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Move(Vector3 position, Action actionComplete)
        {
            if (_moveCor != null)
            {
                StopCoroutine(_moveCor);
            }

            if (Position == position)
            {
                actionComplete?.Invoke();
                return;
            }

            _moveCor = StartCoroutine(MoveCor(position, actionComplete));
        }

        private IEnumerator MoveCor(Vector3 position, Action actionComplete)
        {
            IsWalking = true;

            _agent.destination = position;

            yield return null;

            var pathEndPosition = _agent.pathEndPosition;

            while (Position != pathEndPosition)
            {
                yield return null;
            }

            IsWalking = false;

            _moveCor = null;
            actionComplete?.Invoke();
        }
    }
}
