using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Tutorial
{
    public class SlicedObject : MonoBehaviour
    {
        [SerializeField] private Image _wholeCube;
        [SerializeField] private Image[] _parts;
        [SerializeField] private Transform[] _partPoint;
        [SerializeField] private Image _star;

        private Vector3 _defaultSmallPartPosition;
        private Vector3 _defaultStarScale;

        public Image WholeCube
        {
            get => _wholeCube;
            set => _wholeCube = value;
        }

        private void Start()
        {
            _defaultSmallPartPosition = _parts[1].transform.position;
            _defaultStarScale = _star.transform.localScale;
        }

        public void SliceAction()
        {
            _parts[1].transform.rotation = quaternion.identity;
            _wholeCube.gameObject.SetActive(false);
            _parts[1].transform.position = _defaultSmallPartPosition;
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].DOFade(1, 0f);
                _parts[i].gameObject.SetActive(true);
            }
            Invoke(nameof(RotatePart), .3f);    
            
            _parts[1].transform.DOMove(_partPoint[0].position, .7f, true).onComplete = () =>
            {
                Vector3 rotate = transform.eulerAngles;
                rotate.z = 180;
                _star.transform.DORotateQuaternion(Quaternion.Euler(rotate), .5f);
                _star.transform.DOScale(_star.transform.localScale * 1.2f, .2f).onComplete = () =>
                {
                    _star.transform.DOScale(0.01f, .3f).onComplete = () =>
                    {
                        _star.gameObject.SetActive(false);
                    };
                };
                
                _parts[1].transform.position = _partPoint[0].position;
                _parts[1].transform.DOMove(_partPoint[1].position, .4f).onComplete = () =>
                {
                    for (int i = 0; i < _parts.Length; i++)
                    {
                        _parts[i].DOFade(0, 0.3f);
                    }
                    _parts[1].transform.position = _partPoint[1].position;
                };
            };
        }

        private void RotatePart()
        {
            Vector3 partRotate = transform.eulerAngles;
            partRotate.z = 45;
            _parts[1].transform.DORotateQuaternion(Quaternion.Euler(partRotate), .4f);
        }
    }
}