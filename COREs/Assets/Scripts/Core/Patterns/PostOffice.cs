using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void ReceiveData(DataPack pack, string eventName);
}

public class PostOffice : SingletonMonobehavior<PostOffice>
{
    [Serializable]
    private class EventSubsciptions
    {
        public string EventName;
        public List<IObserver> subscribers;
    }
    [SerializeField]
    List<EventSubsciptions> availableEvents = new List<EventSubsciptions>();
    public void SendData(DataPack pack, string eventName)
    {
        var targetEvent = availableEvents.Find(availEvent => availEvent.EventName.Equals(eventName));
        if (targetEvent == null)
        {
            LogHelper.GetInstance().LogWarning("A data pack is sent to an Non-Existing event " + eventName, true);
            return;
        }

        LogHelper.GetInstance().Log("Sending data pack of event " + eventName + " to " + targetEvent.subscribers.Count + " subscribers", true);
        for (int i = 0; i < targetEvent.subscribers.Count; i++)
        {
            targetEvent.subscribers[i].ReceiveData(pack, eventName);
        }

    }
    public void Subscribes(IObserver newObserver, string eventToListen)
    {
        var targetEvent = availableEvents.Find(availEvent => availEvent.EventName.Equals(eventToListen));
        if (targetEvent == null)
        {
            targetEvent = CreateNewEvent(eventToListen);
        }

        if (targetEvent.subscribers.Contains(newObserver))
        {
            LogHelper.GetInstance().LogWarning(newObserver.ToString().Bolden() + " is being subsribed MORE THAN ONCE to event " + eventToListen, true);
            return;
        }

        targetEvent.subscribers.Add(newObserver);
        availableEvents.Add(targetEvent);
        LogHelper.GetInstance().Log(newObserver.ToString().Bolden() + " is added to event " + eventToListen, true);
    }

    private EventSubsciptions CreateNewEvent(string eventToListen)
    {
        var newEvent = new EventSubsciptions();
        newEvent.EventName = eventToListen;
        newEvent.subscribers = new List<IObserver>();
        return newEvent;
    }
}
