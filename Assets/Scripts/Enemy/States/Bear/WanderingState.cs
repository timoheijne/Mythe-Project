﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WanderingState : State {

	[SerializeField, Tooltip("The radius around the agent the next target could be placed.")]
	private float _wanderRadius = 10;
	private NavMeshAgent _navAgent;
	
	private void Start() {
		_navAgent = GetComponent<NavMeshAgent>();
		if(!_navAgent) throw new System.Exception($"WanderingState :: No NavMeshAgent was found on the object {transform.name}. ");
	}

	public override void EnterState(StateMachine machine) {
		base.EnterState(machine);
		
		// Set target position;
		_navAgent.SetDestination(GetRandomPosition());
		_navAgent.isStopped = false;
	}

	public override void Act(StateMachine machine) { }

	public override void Reason(StateMachine machine) {
		if (Vector3.Distance(_navAgent.destination, transform.position) < 1f) {
			machine.CurrentState = GetComponent<IdleState>();
		}
	}

	public override void ExitState(StateMachine machine) {
		_navAgent.isStopped = true;
	}

	private Vector3 GetRandomPosition() {
		Vector3 randomDirection = transform.position + (Random.insideUnitSphere * _wanderRadius);
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, _wanderRadius, 1);
		return hit.position;
	}

	private void OnDrawGizmosSelected() {
		if(_navAgent != null)
			Debug.DrawLine(_navAgent.destination, _navAgent.destination + Vector3.up * 10, Color.red);
	}
}
