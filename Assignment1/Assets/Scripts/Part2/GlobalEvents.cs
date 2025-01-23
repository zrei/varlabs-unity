using UnityEngine;

public delegate void IntEvent(int _);
public delegate void FloatEvent(float _);
public delegate void VoidEvent();

public static class GlobalEvents
{
    public delegate void FruitEvent(FruitCollectible _);
    public static FruitEvent ScoreEvent;
    public static VoidEvent PlayerDeathEvent;
    public static VoidEvent WallCollisionEvent;
}
