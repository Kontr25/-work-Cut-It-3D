using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MeshSplitting.Examples
{
    public class SliceObject : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private float _sliceTimeValue;
        [SerializeField] private AudioSource[] _sliceSounds;

        private Coroutine _sliceRoutine;
        private WaitForSeconds _sliceTime;

        private void Awake()
        {
            _sliceTime = new WaitForSeconds(_sliceTimeValue);
        }

        public void Slice(Vector3 startPoint, Vector3 endPoint)
        {
            if (_sliceRoutine != null)
            {
                StopCoroutine(_sliceRoutine);
            }

            _sliceRoutine = StartCoroutine(SliceRoutine(startPoint, endPoint));
        }

        private IEnumerator SliceRoutine(Vector3 startPoint, Vector3 endPoint)
        {
            _sliceSounds[Random.Range(0, _sliceSounds.Length)].Play();
            transform.position = startPoint;
            _trailRenderer.Clear();
            _trailRenderer.emitting = true;
            transform.DOMove(endPoint, _sliceTimeValue);
            yield return _sliceTime;
            _trailRenderer.emitting = false;
            gameObject.SetActive(false);
        }
    }
}