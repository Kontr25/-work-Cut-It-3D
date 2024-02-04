using System;
using System.Collections;
using DG.Tweening;
using MeshSplitting.Splitables;
using UnityEngine;

namespace _Scripts.Environment
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private Vector3[] _movePoints;
        [SerializeField] private float _moveDuradion;

        private int _currentPointNumber;
        private WaitForSeconds _moveDelay;
        private Coroutine _moveRoutine;

        private void Start()
        {
            _moveDelay = new WaitForSeconds(_moveDuradion + 0.1f);
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }

            transform.position = _movePoints[_movePoints.Length - 1];
            StartCoroutine(MoveRoutine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Splitable splitable))
            {
                splitable.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Splitable splitable))
            {
                splitable.transform.SetParent(null);
            }
        }

        private IEnumerator MoveRoutine()
        {
            while (gameObject.activeInHierarchy)
            {
                Debug.Log("Repeat");
                for (int i = 0; i < _movePoints.Length; i++)
                {
                    transform.DOMove(_movePoints[i], _moveDuradion);
                    yield return _moveDelay;
                }
            }
        }
    }
}