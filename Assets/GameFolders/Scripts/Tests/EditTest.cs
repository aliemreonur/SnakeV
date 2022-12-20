using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SnakeV.Abstracts;
using SnakeV.Inputs;
using NSubstitute;
using SnakeV.Utilities;

namespace Test.Movements
{
    public class EditTest
    {
        // A Test behaves as an ordinary methods
        [Test]
        public void TestMoveRight()
        {
            //player is moving to forward by default. Test will be to right.
            IControllable _playerController = Substitute.For<IControllable>();
            IInputConverter _inputConverter = new VectorConverter(_playerController);
            IInputReader _inputReader = new InputGatherer();
            
            _inputReader.Horizontal = 1;
            _inputConverter.InputReader = _inputReader;
            _inputReader.Horizontal = 1;
            _inputConverter.NormalUpdate();
            _inputConverter.Test();

            Assert.AreEqual(_inputConverter.MoveDirection, Vector3.right);
        }

    }
}


