
using UnityEngine;
using SnakeV.Core.Managers;

namespace SnakeV.Utilities
{
    [RequireComponent(typeof(Camera))]
    public class CameraManager : MonoBehaviour
    {
        private float _width, _height;
        private Camera _camera;

        private float _aspectRatio;
        private float _targetRatio;

        void Start()
        {
            _camera = GetComponent<Camera>();
            _width = FloorManager.Instance.Width;
            _height = FloorManager.Instance.Height;
            _aspectRatio = (float)Screen.width / (float)Screen.height;
            _targetRatio = (float)_width / (float)_height;
            AdjustCamera();
        }

        private void AdjustCamera()
        {
            float xPos;
            float zPos;

            //TODO: this will be set to public for adjusting the camera on level changes 
            //TODO: OR ON change of screen orientation
            SetOrthographicSize();
            xPos = Mathf.CeilToInt(_width / 2);
            zPos = Mathf.CeilToInt(_height / 2);
            _camera.transform.position = new Vector3(xPos, _camera.transform.position.y, zPos);
        }

        private void SetOrthographicSize()
        {
            if (_aspectRatio >= _targetRatio)
            {
                _camera.orthographicSize = _height / 2;
            }
            else
            {
                float sizeDifference = _targetRatio / _aspectRatio;
                _camera.orthographicSize = _height / 2 * sizeDifference;
            }
        }
    }
}


