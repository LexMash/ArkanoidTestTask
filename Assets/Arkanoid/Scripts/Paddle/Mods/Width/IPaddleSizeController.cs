using UnityEngine;

namespace Arkanoid.Paddle
{
    /// <summary>
    /// Компонент для изменения ширины PaddleView
    /// </summary>
    public interface IPaddleSizeController
    {
        public float Width { get; }
        void SetInitialSize();
        void Decrease();
        void Increase();
        bool CanIncrease();
        bool CanDecrease();
    }
}