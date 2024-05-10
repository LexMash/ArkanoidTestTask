using System;
using UnityEngine;

namespace Arkanoid.Input
{
    public interface IInput
    {
        event Action ActionPerformed;
        event Action<Vector2> MovePerformed;
    }
}