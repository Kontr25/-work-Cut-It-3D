using UnityEngine;

namespace _Scripts.Joints
{
    public class Joint : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody JointRigidbody
        {
            get => _rigidbody;
            set => _rigidbody = value;
        }
    }
}