using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveState : State
{

    // temps maximum dans lequel on reste en état agressif après avoir vu disparaitre un ennemi
    private int timeLimit;

    public State ear() {
        return this;
    }

    public State view(ViewEvent ev) {
        if(ev.ennemyInView == false) {
            // l'ennemi a disparu, on redevient alerte
            return new AlertState();
        }
        return this;
    }

}
