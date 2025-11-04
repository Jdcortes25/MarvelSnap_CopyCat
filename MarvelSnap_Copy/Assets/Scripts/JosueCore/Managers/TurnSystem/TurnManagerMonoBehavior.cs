using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JosueCore.Managers
{
    public class TurnManagerMonoBehavior : MonoBehaviour
    {
        [Serializable]
        public class StartTurnEvents
        {
            public UnityEvent OnTurnStarted;
            public UnityEvent<string> GetCurrentTurnOutOfMaxTurnEvent;
        }

        [Serializable]
        public class EndTurnEvents
        {
            public UnityEvent OnTurnEnded;
        }

        [Serializable]
        public class TurnEvent
        {
            public uint TurnNumber;
            public UnityEvent OnTurnStarted;
            public UnityEvent OnTurnEnded;
        }

        [SerializeField] private TurnManager.TurnSettings settings;
        [SerializeField] private StartTurnEvents startTurnEvents;
        [SerializeField] private EndTurnEvents endTurnEvents;

        [Header("Other Settings:")]
        [SerializeField] private List<TurnEvent> specificTurnEvents = new();

        private TurnManager turnManager;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            TurnManager.StartTurnEvents startTurnEventsConverted = new()
            {
                OnTurnStarted = startTurnEvents.OnTurnStarted.Invoke,
                GetCurrentTurnOutOfMaxTurnEvent = startTurnEvents.GetCurrentTurnOutOfMaxTurnEvent.Invoke
            };

            TurnManager.EndTurnEvents endTurnEventsConverted = new()
            {
                OnTurnEnded = endTurnEvents.OnTurnEnded.Invoke,
            };

            List<TurnManager.TurnEvent> specificTurnEventsConverted = new();

            foreach (TurnEvent turnEvent in specificTurnEvents)
            {
                TurnManager.TurnEvent turnEventConverted = new()
                {
                    TurnNumber = turnEvent.TurnNumber,
                    OnTurnStarted = turnEvent.OnTurnStarted.Invoke,
                    OnTurnEnded = turnEvent.OnTurnEnded.Invoke
                };

                specificTurnEventsConverted.Add(turnEventConverted);
            }

            turnManager = new TurnManager(settings, startTurnEventsConverted, endTurnEventsConverted, specificTurnEventsConverted);
        }

        public void GoToNextTurn() => turnManager.GoToNextTurn();

        public void GotoPreviousTurn() => turnManager.GotoPreviousTurn();

        public string GetCurrentTurnOutOfMaxTurnsText() => turnManager.GetCurrentTurnOutOfMaxTurnsText();
    }
}
