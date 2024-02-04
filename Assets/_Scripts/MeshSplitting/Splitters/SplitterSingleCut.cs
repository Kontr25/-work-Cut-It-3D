using _Scripts.UI;
using MeshSplitting.Splitables;
using UnityEngine;

namespace MeshSplitting.Splitters
{
    [AddComponentMenu("Mesh Splitting/Splitter Single Cut")]
    public class SplitterSingleCut : Splitter
    {
        private bool _hasCut = false;
        private float _time = .1f;
        private bool _wasContact = false;
        protected override void SplitObject(ISplitable splitable, GameObject go)
        {
            splitable.Split(_transform);
            if (!_wasContact)
            {
                _wasContact = true;
                AttempsCounter.Instance.CurrentAttemps++;
            }
            _hasCut = true;
        }

        protected virtual void Update()
        {
            _time -= Time.deltaTime;
            if (_hasCut || _time <= 0f)
                Destroy(gameObject);
        }
    }
}