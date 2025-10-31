using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace JosueCore.Managers
{
    public class TurnManager : MonoBehaviour
    {
        [Serializable]
        public class TurnEvent
        {
            public uint TurnNumber;
            public UnityEvent OnTurnStarted;
            public UnityEvent OnTurnEnded;
        }

        [Serializable]
        public class Settings
        {
            [Header("Turn Settings: ")]
            public uint NumberOfTurns;
            public uint StartingTurn;

            [Header("Start Turn Events: ")]
            public UnityEvent OnTurnStarted;
            public UnityEvent<string> GetCurrentTurnOutOfMaxTurnEvent;

            [Header("End Turn Events: ")]
            public UnityEvent OnTurnEnded;
            
            [Header("Specific Turn Events")]
            public List<TurnEvent> SpecificTurnEvents = new();
        }

        [SerializeField] private Settings settings;

        private TurnEvent currentTurnEvent;
        private uint currentTurn = 0;

        public uint CurrentTurn => currentTurn;
        public uint MaxTurns => settings.NumberOfTurns;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            currentTurn = settings.StartingTurn - 1;
            GoToNextTurn();
        }

        private void UpdateCurrentTurn(bool goingToNextTurn)
        {
            FireEndTurnEvents();
            currentTurn = goingToNextTurn ? currentTurn + 1 : currentTurn -1;
            currentTurnEvent = GetTurnEventForCurrentTurn();
            FireStartTurnEvents();
        }

        public void GoToNextTurn()
        {
            if(currentTurn != MaxTurns)
            {
                UpdateCurrentTurn(true);
            }
        }

        public void GotoPreviousTurn()
        {
            if(currentTurn > 0)
            {
                UpdateCurrentTurn(false);
            }
        }

        private void FireStartTurnEvents()
        {
            settings.OnTurnStarted?.Invoke();
            settings.GetCurrentTurnOutOfMaxTurnEvent?.Invoke(GetCurrentTurnOutOfMaxTurnsText());
            FireCurrentTurnEvent(true);
        }

        private void FireEndTurnEvents()
        {
            if(currentTurn < 1)
            {
                return;
            }

            settings.OnTurnEnded?.Invoke();
            FireCurrentTurnEvent(false);
        }

        private TurnEvent GetTurnEventForCurrentTurn()
        {
            return settings.SpecificTurnEvents.Find(x => x.TurnNumber == currentTurn);
        }

        private void FireCurrentTurnEvent(bool fireStartEvent)
        {
            if (currentTurnEvent == null)
            {
                return;
            }

            UnityEvent eventToFire = fireStartEvent ? currentTurnEvent.OnTurnStarted : currentTurnEvent.OnTurnEnded;
            eventToFire?.Invoke();
        }

        public string GetCurrentTurnOutOfMaxTurnsText()
        {
            return $"Turn {CurrentTurn} / {MaxTurns}";
        }
    }
}
