using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State {
    State State.ear() {
        return new AgressiveState();
    }

    State State.view() {
        return new AgressiveState();
    }
}
