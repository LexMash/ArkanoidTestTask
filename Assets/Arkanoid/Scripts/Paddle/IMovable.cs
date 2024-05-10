using UnityEngine;

namespace Arkanoid.Paddle
{
    public interface IMovable
    {
        Vector2 CurrentPosition { get; set; }
        Vector2 Velocity { get; set; }
        float Width { get; }
    }
}