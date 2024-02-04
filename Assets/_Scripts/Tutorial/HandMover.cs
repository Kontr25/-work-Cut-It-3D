using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Tutorial
{
    public class HandMover : MonoBehaviour
    {
        [SerializeField] private Image _hand;
        [SerializeField] private Transform _movePoint;
        [SerializeField] private SlicedObject _slicedObject;
        [SerializeField] private bool _tutorWithSlicedObject;
        [SerializeField] private Image _punktir;
        [SerializeField] private Transform _star;

        private Vector3 _handDefaultPosition;
        private Vector3 _defaultHandScale =  new Vector3(0.7f,0.7f,0.7f);
        private Vector3 _smalScale = new Vector3(0.6f,0.6f,0.6f);
        private Vector3 _defaultStarScale;

        private void Start()
        {
            _handDefaultPosition = _hand.transform.position;
            if (_tutorWithSlicedObject)
            {
                _defaultStarScale = _star.transform.localScale;
            }

            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            while (true)
            {
                _hand.transform.position = _handDefaultPosition;
                _hand.transform.localScale = _defaultHandScale;
                if (_tutorWithSlicedObject)
                {
                    _star.transform.localScale = _defaultStarScale;
                    _star.transform.rotation = Quaternion.identity;
                    _star.gameObject.SetActive(true);
                    _slicedObject.WholeCube.gameObject.SetActive(true);
                }

                _hand.DOFade(1, .5f).onComplete = () =>
                {
                    _punktir.DOFade(1, .2f);
                };
                yield return new WaitForSeconds(.5f);
                _hand.transform.localScale = _smalScale;
                _hand.transform.DOMove(_movePoint.position, 2f).onComplete = () =>
                {
                    _hand.transform.localScale = _defaultHandScale;
                    _punktir.DOFade(0, .2f);
                };
                yield return new WaitForSeconds(2f);
                _hand.DOFade(0, .3f);

                if (_tutorWithSlicedObject)
                {
                    _slicedObject.SliceAction();
                    yield return new WaitForSeconds(3f);
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        public void CloseTutorial()
        {
            gameObject.SetActive(false);
        }
    }
}