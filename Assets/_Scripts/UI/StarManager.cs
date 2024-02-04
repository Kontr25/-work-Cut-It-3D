using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class StarManager : MonoBehaviour
    {
        public static StarManager Instance;

        [SerializeField] private Image[] _starBGs;
        [SerializeField] private Star[] _stars;
        [SerializeField] private float _giveStarDelayValue;

        private WaitForSeconds _giveStarDelay;

        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _giveStarDelay = new WaitForSeconds(_giveStarDelayValue);
        }

        public void GiveStar(int starCount)
        {
            StartCoroutine(GiveStarRoutine(starCount));
        }

        private IEnumerator GiveStarRoutine(int starCount)
        {
            for (int i = 0; i < _starBGs.Length; i++)
            {
                _starBGs[i].DOFade(1, .2f);
            }
            Debug.Log("DUVESTAR");

            yield return new WaitForSeconds(.2f);
            
            for (int i = 0; i < starCount; i++)
            {
                _stars[i].GiveStar();
                yield return _giveStarDelay;
            }
        }
    }
}