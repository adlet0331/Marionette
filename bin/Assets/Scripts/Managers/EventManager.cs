using System;

public class Event {
    public event EventHandler scriptEvent;
    public void OnScriptEvent() {
        scriptEvent(this, EventArgs.Empty);
    }
}

public class EventManager : Singleton<EventManager> {
    
}
