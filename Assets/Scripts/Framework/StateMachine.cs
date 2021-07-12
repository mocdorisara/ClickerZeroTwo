using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Framework
{
    public class StateMachine<T>
    {
        Dictionary<int, State<T>> stateDict = null;

        public StateMachine()
        {
            stateDict = new Dictionary<int, State<T>>();
        }

        public void OnUpdate(float dt)
        {
            if (currentState == null) return;

            if(chageState)
            {
                prevState = currentState;
                currentState.OnExiting();
                currentState = nextState;
                currentState.OnEntering();
                chageState = false;
            }

            currentState.OnUpdating(dt);
        }

        public void AddState(int id, State<T> state)
        {
            stateDict.Add(id, state);
        }

        public void SetState(int id)
        {
            if (currentState != null)
            {
                currentState.OnExiting();
                prevState = currentState;
            }
            
            currentState = stateDict[id];
            currentState.OnEntering();
        }
        
        public void ChangeState(int id)
        {
            nextState = stateDict[id];
            chageState = true;
        }

        public void Destory()
        {
            // TODO Dictionary iterator delete
            stateDict.Clear();
        }

        bool chageState = false;

        State<T> currentState;
        State<T> prevState;
        State<T> nextState;
    }

}
