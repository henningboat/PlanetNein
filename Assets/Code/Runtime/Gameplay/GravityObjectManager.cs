using System.Collections.Generic;
using PlanetNein.Runtime.Gameplay;
using THUtils;

namespace Runtime.Gameplay
{
    public class GravityObjectManager : Singleton<GravityObjectManager>
    {
        private List<GravityObject> _gravityObjects = new List<GravityObject>();
        public IReadOnlyList<GravityObject> GravityObjects => _gravityObjects;

        public void AddObject(GravityObject gravityObject)
        {
            _gravityObjects.Add(gravityObject);
        }

        public void RemoveObject(GravityObject gravityObject)
        {
            _gravityObjects.Remove(gravityObject);
        }
    }
}