using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmState : State
{
    State State.ear() {
        return this;
    }

    State State.view() {
        return this;
    }
}
