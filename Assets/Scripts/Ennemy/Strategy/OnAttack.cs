﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttack : IEnnemyState {

    // private readonly Ennemy_Brain brain;

    public OnAttack(/*Ennemy_Brain brain*/) {
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
