using System;
/* Event 제어 매니저
 * 
 * 잘 몰?루
 * 
 */
public class EventManager : Singleton<EventManager> {
    //Collider Object
    private EventHandler _Collide;
    public event EventHandler Collide
    {
        add
        {
            _Collide += value;
        }
        remove
        {
            _Collide -= value;
        }
    }
    public void PlayerCollide()
    {
        if(this._Collide != null)
        {
            _Collide(this, EventArgs.Empty);
        }
    }

    private void Start()
    {
        
    }
}