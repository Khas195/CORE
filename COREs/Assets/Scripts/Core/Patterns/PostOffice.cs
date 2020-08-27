﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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
        [ReadOnly]
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
            LogHelper.LogWarning("A data pack is sent to an Non-Existing event " + eventName, true);
            return;
        }
        LogHelper.Log("Sending data pack of event " + eventName + " to " + targetEvent.subscribers.Count + " subscribers", true);
        for (int i = 0; i < targetEvent.subscribers.Count; i++)
        {
            targetEvent.subscribers[i].ReceiveData(pack, eventName);
        }

    }

    public void Unsubscribes(IObserver observer, string eventToUnsubscribe)
    {
        var targetEvent = availableEvents.Find(availEvent => availEvent.EventName.Equals(eventToUnsubscribe));
        if (targetEvent == null)
        {
            LogHelper.LogWarning(observer.ToString().Bolden() + " is trying to unsubscribe to non-existing event " + eventToUnsubscribe, true);
            return;
        }

        if (targetEvent.subscribers.Contains(observer))
        {
            targetEvent.subscribers.Remove(observer);
            LogHelper.Log(observer.ToString().Bolden() + " is unsubscribe to event " + eventToUnsubscribe, true);
        }
        else
        {
            LogHelper.Log(observer.ToString().Bolden() + " had not subscribed to event " + eventToUnsubscribe, true);
        }

    }

    public void Subscribes(IObserver newObserver, string eventToListen)
    {
        var targetEvent = availableEvents.Find(availEvent => availEvent.EventName.Equals(eventToListen));
        if (targetEvent == null)
        {
            targetEvent = CreateNewEvent(eventToListen);
            availableEvents.Add(targetEvent);
        }

        if (targetEvent.subscribers.Contains(newObserver))
        {
            LogHelper.LogWarning(newObserver.ToString().Bolden() + " is being subsribed MORE THAN ONCE to event " + eventToListen, true);
            return;
        }

        targetEvent.subscribers.Add(newObserver);
        LogHelper.Log(newObserver.ToString().Bolden() + " is added to event " + eventToListen, true);
    }

    private EventSubsciptions CreateNewEvent(string eventToListen)
    {
        var newEvent = new EventSubsciptions();
        newEvent.EventName = eventToListen;
        newEvent.subscribers = new List<IObserver>();
        return newEvent;
    }
}
