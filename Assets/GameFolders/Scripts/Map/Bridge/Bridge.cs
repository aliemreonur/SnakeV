using UnityEngine;
using SnakeV.Abstracts;
using System;
using System.Collections;
using SnakeV.Core.Managers;

namespace SnakeV.Core.Bridges
{
    public class Bridge : MonoBehaviour
    {
        public int XPos { get; private set; }
        public int ZPos { get; private set; }
        public Action OnDirectionSet;
        public BridgeDirection BridgeDirection => _bridgeDirection;

        private PlayerController _playerController;
        private BridgeDirection _bridgeDirection;
        private Vector2Int _exitPoint;
        private Vector2Int _enterPoint;

        private void Awake()
        {
            _playerController = PlayerController.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if ((_playerController.XPos != _enterPoint.x) || (_playerController.ZPos != _enterPoint.y))
                    _playerController.Death();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if ((_playerController.transform.position.x != _exitPoint.x) || (_playerController.transform.position.z != _exitPoint.y))
                    _playerController.Death();
            }
        }

        public void SetBridgeDirection(BridgeDirection direction)
        {
            _bridgeDirection = direction;
            SetGateEnterAndExits(direction);
            OnDirectionSet?.Invoke();
            StartCoroutine(DisableBridgeRoutine());
        }

        private void SetGateEnterAndExits(BridgeDirection direction)
        {
            switch(direction)
            {
                case BridgeDirection.Up:
                    _enterPoint = new Vector2Int(XPos, ZPos-1);
                    _exitPoint = new Vector2Int(XPos, ZPos+1);
                    break;
                case BridgeDirection.Right:
                    _enterPoint = new Vector2Int(XPos - 1, ZPos);
                    _exitPoint = new Vector2Int(XPos + 1, ZPos);
                    break;
                case BridgeDirection.Down:
                    _enterPoint = new Vector2Int(XPos, ZPos+1);
                    _exitPoint = new Vector2Int(XPos, ZPos-1);
                    break;
                default:
                    _enterPoint = new Vector2Int(XPos + 1, ZPos);
                    _exitPoint = new Vector2Int(XPos - 1, ZPos);
                    break;
            }
        }

        public void SetXandZPos(Vector3 pos)
        {
            XPos = (int)pos.x;
            ZPos = (int)pos.z;
            FloorManager.Instance.allTiles[XPos, ZPos].TileFull();
        }

        private IEnumerator DisableBridgeRoutine()
        {
            yield return new WaitForSeconds(20f);
            FloorManager.Instance.allTiles[XPos, ZPos].TileEmpty();
            gameObject.SetActive(false);
        }
    }

}
