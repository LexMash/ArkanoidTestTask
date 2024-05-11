namespace Arkanoid.Paddle
{
    /// <summary>
    /// Компонент для изменения ширины PaddleView
    /// </summary>
    public interface IWidthChanger
    {
        void Decrease();
        void Increase();
    }
}