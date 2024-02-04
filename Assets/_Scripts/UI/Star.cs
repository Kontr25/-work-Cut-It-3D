using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private Image _star;

        public void GiveStar()
        {
            _star.DOFade(1, .2f);
            _star.transform.DOScale(Vector3.one/2, .2f).onComplete = () =>
            {
                _star.transform.DOScale(Vector3.one, .1f).onComplete = () =>
                {
                    _star.transform.localScale = Vector3.one;
                };
            };
        }
    }
}