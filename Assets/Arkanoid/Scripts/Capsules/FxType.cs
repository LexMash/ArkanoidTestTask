namespace Arkanoid.Capsules
{
    public enum FxType
    {
        None,
        SlowBall,   //замедляет мяч
        Magnet,     //позволяет ловить мяч
        Expand,     //расширяет paddle
        Shrink,     //уменьшает paddle
        SplitBall,  //разбивает мяч на три
        EnergyBall, //мяч проходит сквозь все блоки уничтожая их
        Gun,        //позволяет стрелять
        Barier      //активирует барьер, что бы не дать мячу упасть
    }
}
