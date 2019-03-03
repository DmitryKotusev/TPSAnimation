using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private class TimedEvent
    {
        public float timeToExecute;
        public CallBack method;
    }

    private List<TimedEvent> events;

    public delegate void CallBack();

    private void Awake()
    {
        events = new List<TimedEvent>();
    }

    public void Add(CallBack method, float inSeconds)
    {
        events.Add(new TimedEvent
        {
            method = method,
            timeToExecute = Time.time + inSeconds
        });
    }

    public void Update()
    {
        if(events.Count == 0)
        {
            return;
        }

        List<TimedEvent> happenedEvents = new List<TimedEvent>();

        for (int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if(timedEvent.timeToExecute <= Time.time)
            {
                timedEvent.method();
                happenedEvents.Add(timedEvent);
            }
        }

        for(int i = 0; i < happenedEvents.Count; i++)
        {
            events.Remove(happenedEvents[i]);
        }
    }
}
