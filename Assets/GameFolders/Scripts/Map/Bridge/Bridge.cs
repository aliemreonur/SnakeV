using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Abstracts;
using System;

namespace SnakeV.Core.Bridge
{
    public class Bridge : MonoBehaviour
    {
        public int XPos { get; private set; }
        public int ZPos { get; private set; }
        public Action OnDirectionSet;
        public BridgeDirection BridgeDirection => _bridgeDirection;

        private BridgeDirection _bridgeDirection;
        private Vector2Int _exitPoint;
        private Vector2Int _enterPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //too many reaches to instance!!
                if ((PlayerController.Instance.XPos != _enterPoint.x) || (PlayerController.Instance.ZPos != _enterPoint.y))
                {
                    PlayerController.Instance.Death();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if ((PlayerController.Instance.transform.position.x != _exitPoint.x) || (PlayerController.Instance.transform.position.z != _exitPoint.y))
                    PlayerController.Instance.Death();
            }
        }

        public void SetBridgeDirection(BridgeDirection direction)
        {
            _bridgeDirection = direction;
            SetGateEnterAndExits(direction);
            OnDirectionSet?.Invoke();
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

        private void OnEnable()
        {
            Destroy(this.gameObject, 30f);
        }

        private bool CheckEnterOrExitPoint(Vector2Int pointToCheck)
        {
            //better to reach out to this via interface
            PlayerController playerController = PlayerController.Instance;

            return (playerController.XPos == pointToCheck.x && playerController.ZPos == pointToCheck.y);
        }

        public void SetXandZPos(Vector3 pos)
        {
            XPos = (int)pos.x;
            ZPos = (int)pos.z;
        }
    }

}
