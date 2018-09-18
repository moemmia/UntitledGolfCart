﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    /*
     * Metodo para marcar el objeto cuando el jugador lo mira
     * */
    void StartUsing(GameObject user);

    void EndUsing();

    void OnStart();
    void OnEnd();

    string MessageToUse();
    string MessageOnUse();
}


public abstract class AbstractUsable : MonoBehaviour, IUsable
{
    protected GameObject user;
    protected bool isUsing;

    public void StartUsing(GameObject user)
    {
        this.user = user;
        OnStart();
        isUsing = true;
    }
    public void EndUsing()
    {
        isUsing = false;
        OnEnd();
        this.user = null;
    }

    public abstract void OnStart();
    public abstract void OnEnd();
    public abstract string MessageToUse();
    public abstract string MessageOnUse();
}