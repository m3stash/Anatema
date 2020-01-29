using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBrain : MonoBehaviour {

    // States
    /*[SerializeField]
    public Ennemy_States.Move state_Move = new Ennemy_States.Move();
    [SerializeField]
    public Ennemy_States.See state_See = new Ennemy_States.See();
    [SerializeField]
    public Ennemy_States.Ear state_Ear = new Ennemy_States.Ear();
    [SerializeField]
    public Ennemy_States.Touch state_Touch = new Ennemy_States.Touch();*/
    public IEnnemyState currentState;

    EnnemyConfig config;
    public void Setup(EnnemyConfig config) {
        this.config = config;
        if (this.config.CanSee()) {
            this.GetComponentInChildren<Eyes>().Setup();
        }
        currentState = new OnPatrol(/*this*/);
        // StartCoroutine(BrainLoop());
    }

    private IEnumerator BrainLoop() {
        while (true) {
            /*if (!state_See.onGround) {
                currentState.Ground();
            } else if (state_Touch.takeDamage) {
                Debug.Log("TAKE DAMAGE");
                // ==> si voit joueur
            } else if (state_See.player) {
                currentState.Attack();
                // ==> si entends quelque chose
            } else if (state_Ear.ear) {
                Debug.Log("EAR => FOLLOW");
                currentState.Move(state_Ear.earTransform.position);
                // ==> si suis quelque chose
            } else if (state_Move.moveToVector2Position.x != 0 && state_Move.moveToVector2Position.y != 0) {
                Debug.Log("IS FOLLOW A TRANSFORM => FOLLOW");
                currentState.Move(state_Move.moveToVector2Position);
                // ==> Si en mode Idle
            } else {
                //currentState.Idle();
                currentState.Patrol();
                // ==> sinon patrouille
            }/* else {
                currentState.Patrol();
            }*/
            // yield return new WaitForSeconds(0.25f);
            yield return new WaitForSeconds(0.10f);
        }
    }
}
