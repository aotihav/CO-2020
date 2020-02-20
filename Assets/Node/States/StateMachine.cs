using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachineCode
{
    public class StateMachine<T>
    {

        protected State<T> currentState { get; private set; }
        public T Node;

        public StateMachine(T _node)
        {
            Node = _node;
            currentState = null;
        }

        public void changeState(State<T> _state)
        {
            if (currentState != null)
                currentState.exitState(Node);

            currentState = _state;
            currentState.enterState(Node);
        }

        public void Update()
        {
            if (currentState != null)
                currentState.updateState(Node);
        }

    }
    public abstract class State<T>
    {
        public abstract void enterState(T _node);
        public abstract void exitState(T _node);
        public abstract void updateState(T _node);

    }

}