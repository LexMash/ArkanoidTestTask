using UnityEngine;

namespace Arkanoid.Input
{
    public interface IPaddleMoveController
    {
        void SetTargetPosition(Vector3 position);
    }
}