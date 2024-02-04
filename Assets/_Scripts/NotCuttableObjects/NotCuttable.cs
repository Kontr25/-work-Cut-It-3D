using _Scripts.TakenObjects;
using UnityEngine;

namespace _Scripts.NotCuttableObjects
{
    public class NotCuttable : MonoBehaviour
    {
        [SerializeField] private ColorType _colorType;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material[] _materials;

        public ColorType Type => _colorType;

        private void Start()
        {
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
    }
}