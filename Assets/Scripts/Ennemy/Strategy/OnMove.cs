using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMove : IEnnemyState {

    // private readonly Ennemy_Brain brain;

    public OnMove(/*Ennemy_Brain brain*/) {
        // this.brain = brain;
    }

    public void Ground() {

    }

    public void Idle() {

    }

    public void Attack() {
        // brain.currentState = new Ennemy_OnAttack(brain);
    }

    public void Patrol() {

    }

    public void Move(Vector2 positionToFollow) {

    }

    public void Defend() {

    }
}
