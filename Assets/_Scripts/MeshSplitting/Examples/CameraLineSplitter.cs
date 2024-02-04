using _Scripts.Tutorial;
using MeshSplitting.Splitters;
using UnityEngine;

namespace MeshSplitting.Examples
{
    [AddComponentMenu("Mesh Splitting/Examples/Camera Line Splitter")]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(LineRenderer))]
    public class CameraLineSplitter : MonoBehaviour
    {
        [SerializeField] private  float CutPlaneSize;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private SliceEffect _sliceEffect;
        [SerializeField] private HandMover _tutorial;
        [SerializeField] private LevelLoader _levelLoader;
        
        private float CutPlaneDistance = 0;
        private bool _hasStartPos = false;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private Ray _ray;
        private RaycastHit _hit;
        

        private void Awake()
        {
            _lineRenderer.enabled = false;
        }
        
        public void MouseDown()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
            {
                if (_levelLoader.CurrentLevel == 1)
                {
                    _tutorial.CloseTutorial();
                }
                _startPos = _hit.point;
                _endPos = _hit.point;
                _lineRenderer.SetPosition(0, _startPos);
                _lineRenderer.enabled = true;
                _hasStartPos = true;
            }
        }

        public void MouseDrag()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
            {
                _endPos = _hit.point;
            }
        }
        
        public void MouseUp()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
            {
                if (_hasStartPos)
                {
                    _endPos = _hit.point;
                    if (_startPos != _endPos)
                    {
                        _sliceEffect.Slice(_startPos, _endPos);
                        CutPlaneSize = Vector3.Distance(_startPos, _endPos);
                        CreateCutPlane();
                    }
                        

                    _hasStartPos = false;
                    _lineRenderer.enabled = false;
                }
            }
        }

        private void Update()
        {
            if (_hasStartPos)
            {
                _lineRenderer.SetPosition(1, _endPos);
            }
        }

        private void CreateCutPlane()
        {
            Vector3 center = Vector3.Lerp(_startPos, _endPos, .5f);
            Vector3 cut = (_endPos - _startPos).normalized;
            Vector3 fwd = (center - transform.position).normalized;
            Vector3 normal = Vector3.Cross(fwd, cut).normalized;

            GameObject goCutPlane = new GameObject("CutPlane", typeof(BoxCollider), typeof(Rigidbody), typeof(SplitterSingleCut));

            goCutPlane.GetComponent<Collider>().isTrigger = true;
            Rigidbody bodyCutPlane = goCutPlane.GetComponent<Rigidbody>();
            bodyCutPlane.useGravity = false;
            bodyCutPlane.isKinematic = true;

            Transform transformCutPlane = goCutPlane.transform;
            transformCutPlane.position = center;
            transformCutPlane.localScale = new Vector3(CutPlaneSize, .01f, CutPlaneSize);
            transformCutPlane.up = normal;
            float angleFwd = Vector3.Angle(transformCutPlane.forward, fwd);
            transformCutPlane.RotateAround(center, normal, normal.y < 0f ? -angleFwd : angleFwd);
        }
    }
}
