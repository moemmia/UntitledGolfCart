﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCar : AbstractUsable{

    public Transform CharPosIn;
    public Transform CharPosOutR;
    public Transform CharPosOutL;

    void Update () {
        if (isUsing)
        {
            user.transform.position = CharPosIn.position;
            user.transform.rotation = CharPosIn.rotation;
            if ( Input.GetKeyDown(KeyCode.E) && this.GetComponent<Rigidbody>().velocity.magnitude<10)
            {
                if(!Physics.Raycast(this.transform.position,-this.transform.right, 2f))
                    user.transform.position = CharPosOutL.position;
                else if (!Physics.Raycast(this.transform.position, this.transform.right, 2f))
                {
                    user.transform.position = CharPosOutR.position;
                }
                else
                {
                    user.transform.position = this.transform.position + Vector3.up * 2 - this.transform.forward;
                }
                EndUsing();
            }
        }
    }

    public override void OnStart()
    {
        EnterCar();
        cc.EnterCar();
    }

    public override void OnEnd()
    {
        cc.ExitCar();
        ExitCar();
    }




    internal void EnterCar()
    {
        GetComponent<CarController>().enabled = true;
        GetComponent<CarController>().HandBrake = false;
    }
    internal void ExitCar()
    {
        StartCoroutine(untilBreak());
    }

    IEnumerator untilBreak()
    {
        GetComponent<CarController>().HandBrake = true;
        yield return new WaitUntil(() => GetComponent<Rigidbody>().velocity.magnitude == 0);
        GetComponent<CarController>().enabled = false;
    }

    public override string MessageToUse()
    {
        return "[E] Use Car";
    }

    public override string MessageOnUse()
    {
        return "[E] Exit";
    }
}
