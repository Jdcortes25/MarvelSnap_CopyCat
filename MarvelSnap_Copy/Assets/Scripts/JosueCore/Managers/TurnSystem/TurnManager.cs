using System;
using System.Collections.Generic;

namespace JosueCore.Managers
{
    public class TurnManager
    {
        [Serializable]
        public class TurnSettings
        {
            public uint NumberOfTurns;
            public uint StartingTurn;
        }

        [Serializable]
        public class StartTurnEvents
        {
            public Action OnTurnStarted;
            public Action<string> GetCurrentTurnOutOfMaxTurnEvent;
        }

        [Serializable]
        public class EndTurnEvents
        {
            public Action OnTurnEnded;
        }

        [Serializable]
        public class TurnEvent
        {
            public uint TurnNumber;
            public Action OnTurnStarted;
            public Action OnTurnEnded;
        }

        private TurnSettings settings;
        private StartTurnEvents startTurnEvents;
        private EndTurnEvents endTurnEvents;
        private List<TurnEvent> specificTurnEvents = new();
        private TurnEvent currentTurnEvent;
        private uint currentTurn = 0;

        public uint CurrentTurn => currentTurn;
        public uint MaxTurns => settings.NumberOfTurns;

        public TurnManager(TurnSettings settings, StartTurnEvents startTurnEvents, EndTurnEvents endTurnEvents, List<TurnEvent> specificTurnEvents)
        {
            this.settings = settings;
            this.startTurnEvents = startTurnEvents;
            this.endTurnEvents = endTurnEvents;
            this.specificTurnEvents = specificTurnEvents;

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
            startTurnEvents.OnTurnStarted?.Invoke();
            startTurnEvents.GetCurrentTurnOutOfMaxTurnEvent?.Invoke(GetCurrentTurnOutOfMaxTurnsText());
            FireCurrentTurnEvent(true);
        }

        private void FireEndTurnEvents()
        {
            if(currentTurn < 1)
            {
                return;
            }

            endTurnEvents.OnTurnEnded?.Invoke();
            FireCurrentTurnEvent(false);
        }

        private TurnEvent GetTurnEventForCurrentTurn()
        {
            return specificTurnEvents.Find(x => x.TurnNumber == currentTurn);
        }

        private void FireCurrentTurnEvent(bool fireStartEvent)
        {
            if (currentTurnEvent == null)
            {
                return;
            }

            Action eventToFire = fireStartEvent ? currentTurnEvent.OnTurnStarted : currentTurnEvent.OnTurnEnded;
            eventToFire?.Invoke();
        }

        public string GetCurrentTurnOutOfMaxTurnsText()
        {
            return $"Turn {CurrentTurn} / {MaxTurns}";
        }
    }
}
