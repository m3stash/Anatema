using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmState : State
{
    State State.ear() {
        return new AlertState();
    }

    State State.view() {
        return new AgressiveState();
    }
}
