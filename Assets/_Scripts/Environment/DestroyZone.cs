using UnityEngine;

namespace _Scripts.Environment
{
    public class DestroyZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}