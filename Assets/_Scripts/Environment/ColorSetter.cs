using UnityEngine;

namespace _Scripts.Environment
{
    public class ColorSetter : MonoBehaviour
    {
        [SerializeField] private Material[] _materials;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private NotSplitableColor _notSplitableColor;

        private void Start()
        {
            SetColor();
        }

        private void SetColor()
        {
            switch (_notSplitableColor)
            {
                case NotSplitableColor.Pink:
                    _meshRenderer.material = _materials[0];
                    break;
                case NotSplitableColor.Violet:
                    _meshRenderer.material = _materials[1];
                    break;
                case NotSplitableColor.Red:
                    _meshRenderer.material = _materials[2];
                    break;
                case NotSplitableColor.Lazur:
                    _meshRenderer.material = _materials[3];
                    break;
                case NotSplitableColor.Green:
                    _meshRenderer.material = _materials[4];
                    break;
                case NotSplitableColor.Bordo:
                    _meshRenderer.material = _materials[5];
                    break;
                case NotSplitableColor.Brown:
                    _meshRenderer.material = _materials[6];
                    break;
            }
        }
    }
}