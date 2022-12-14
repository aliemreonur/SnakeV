using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeV.Utilities;

namespace SnakeV.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        private void Awake()
        {
            SingletonThisObj(this);
        }
    }
}


