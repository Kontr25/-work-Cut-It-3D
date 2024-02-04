using _Scripts.NotCuttableObjects;
using _Scripts.UI;
using DG.Tweening;
using MeshSplitting.Splitables;
using UnityEngine;

namespace _Scripts.TakenObjects
{
    public class TakenObject : MonoBehaviour
    {
        [SerializeField] private GameObject _mesh;
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _starExplosion;
        [SerializeField] private ColorType _colorType;
        [SerializeField] private Material[] _materials;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private AudioSource[] _starsSound;
        private void Start()
        {
            AttempsCounter.Instance.TakenObjectsList.Add(this);
            
            switch (_colorType)
            {
                case ColorType.None:
                    break;
                case ColorType.LightYellow:
                    _meshRenderer.material = _materials[0];
                    break;
                case ColorType.White:
                    _meshRenderer.material = _materials[1];
                    break;
                case ColorType.Yellow:
                    _meshRenderer.material = _materials[2];
                    break;
                case ColorType.LightOrange:
                    _meshRenderer.material = _materials[3];
                    break;
                case ColorType.Beige:
                    _meshRenderer.material = _materials[4];
                    break;
                case ColorType.DarkBrown:
                    _meshRenderer.material = _materials[5];
                    break;
                case ColorType.Orange:
                    _meshRenderer.material = _materials[6];
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Splitable splitable) && _colorType == splitable.SplitableColorType ||
                other.TryGetComponent(out NotCuttable notsplitable) && _colorType == notsplitable.Type)
            {
                Taken(true);
            }
        }

        private void Taken(bool value)
        {
            if (value)
            {
                AttempsCounter.Instance.RemoveFromList(this);
                _collider.enabled = false;
                int soundNumber = Random.Range(0, _starsSound.Length);
                _starsSound[soundNumber].Play();
                _mesh.transform.DOScale(_mesh.transform.localScale * 1.3f, .2f).onComplete = () =>
                {
                    _mesh.transform.DOScale(.1f, .1f).onComplete = () =>
                    {
                        _mesh.SetActive(false);
                        _starExplosion.Play();
                    };
                };
            }
        }
    }
}