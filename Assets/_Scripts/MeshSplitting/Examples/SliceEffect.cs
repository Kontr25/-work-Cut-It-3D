using Pool;
using UnityEngine;

namespace MeshSplitting.Examples
{
    public class SliceEffect : MonoBehaviour
    {
        [SerializeField] private int _pollCapacity;
        [SerializeField] private SliceObject _sliceObject;
        private Pool<SliceObject> _pool;

        private void Awake()
        {
            _pool = new Pool<SliceObject>(_sliceObject, _pollCapacity, null)
            {
                AutoExpand = true
            };
        }

        public void Slice(Vector3 startPoint, Vector3 endPoint)
        {
            Debug.Log("SLICE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            var sliceObj = _pool.GetFreeElement();
            sliceObj.Slice(startPoint, endPoint);
        }
    }
}