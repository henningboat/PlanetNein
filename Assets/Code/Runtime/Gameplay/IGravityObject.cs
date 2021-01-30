using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public interface IGravityObject
    {
        float Radius { get; }
        float ForceMultiplier { get; }
        Vector2 Position { get; }
        bool AffectCamera { get; }
    }
}