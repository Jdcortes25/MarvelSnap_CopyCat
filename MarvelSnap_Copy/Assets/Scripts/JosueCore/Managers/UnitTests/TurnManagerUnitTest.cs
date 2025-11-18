using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace JosueCore.Managers.UnitTest
{
    public class TurnManagerUnitTest : MonoBehaviour
    {
        public TurnManager CreateSimpleTurnManager()
        {

            TurnManager.TurnSettings turnSettings = new()
            {
                NumberOfTurns = 10,
                StartingTurn = 1
            };

            TurnManager.StartTurnEvents startTurnEvents = new()
            {
                GetCurrentTurnOutOfMaxTurnEvent = null,
                OnTurnStarted = null
            };

            TurnManager.EndTurnEvents endTurnEvents = new()
            {
                OnTurnEnded = null
            };

            TurnManager turnManager = new(turnSettings, startTurnEvents, endTurnEvents, null);
            return turnManager;
        }

        [Test]
        public void Initialized()
        {
            TurnManager turnManager = CreateSimpleTurnManager();
            Assert.IsTrue(turnManager.CurrentTurn == 1);
        }

        [Test]
        public void GoToNexTurn()
        {
            TurnManager turnManager = CreateSimpleTurnManager();
            turnManager.GoToNextTurn();
            Assert.IsTrue(turnManager.CurrentTurn == 2);
        }

        [Test]
        public void GoToPreviousTurn()
        {
            TurnManager turnManager = CreateSimpleTurnManager();
            turnManager.GoToNextTurn();
            turnManager.GotoPreviousTurn();
            Assert.IsTrue(turnManager.CurrentTurn == 1);
        }
    }
}
