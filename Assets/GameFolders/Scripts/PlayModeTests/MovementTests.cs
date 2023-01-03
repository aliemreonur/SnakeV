using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SnakeV.Core;
using SnakeV.Abstracts;
using NSubstitute;
using SnakeV.Inputs;
using SnakeV.Core.Managers;

namespace Test.PlayMode
{
    public class MovementTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MovementTestsWithEnumeratorPasses()
        {
            yield return null;
        }
    }

}

