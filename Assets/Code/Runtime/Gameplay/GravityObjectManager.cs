using System.Collections.Generic;
using PlanetNein.Runtime.Gameplay;
using THUtils;

namespace Runtime.Gameplay
{
    public class GravityObjectManager : Singleton<GravityObjectManager>
    {
        private List<IGravityObject> _gravityObjects = new List<IGravityObject>();
        public IReadOnlyList<IGravityObject> GravityObjects => _gravityObjects;

        public void AddObject(IGravityObject gravityObject)
        {
            _gravityObjects.Add(gravityObject);
        }

        public void RemoveObject(IGravityObject gravityObject)
        {
            _gravityObjects.Remove(gravityObject);
        }
    }
}