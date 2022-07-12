using System.Collections.Generic;
using InGameObjects.Object;

namespace Managers
{
    public class MovingAnimaControllManager : Singleton<MovingAnimaControllManager>
    {
        public MovingObject ControllingMainAnima;
        public List<MovingObject> ControllingSubAnimasList;
    
    
    }
}
