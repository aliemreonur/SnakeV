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

        [Test]
        public void test_first_up_then_try_down()
        {
            IControllable playerController = Substitute.For<IControllable>();
            IInputConverter vectorConverter = new VectorConverter(playerController);
            playerController.Direction.Returns(Vector3.forward);
            IInputReader inputReader = Substitute.For<IInputReader>();
            vectorConverter.InputReader = inputReader;
            inputReader.Vertical.Returns(-1);
            vectorConverter.NormalUpdate();

            Assert.AreEqual(Vector3.forward, vectorConverter.MoveDirection);
        }

        
        // A Test behaves as an ordinary methods
        [Test]
        public void test_first_up_then_direction_right()
        {
            IControllable playerController = Substitute.For<IControllable>();
            IInputConverter vectorConverter = new VectorConverter(playerController);
            playerController.Direction.Returns(Vector3.forward);
            IInputReader inputReader = Substitute.For<IInputReader>();
            vectorConverter.InputReader = inputReader;
            inputReader.Horizontal.Returns(1);
            vectorConverter.NormalUpdate();

            Assert.AreEqual(Vector3.right, vectorConverter.MoveDirection);

        }

    }
}


