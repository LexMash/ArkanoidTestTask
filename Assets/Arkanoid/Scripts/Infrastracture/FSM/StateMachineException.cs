using System;

namespace Infrastructure
{
    public class StateMachineException : SystemException
    {
        public StateMachineException(string message) : base(message)
        {
        }
    }
}
