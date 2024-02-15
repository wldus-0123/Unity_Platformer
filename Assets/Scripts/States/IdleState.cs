using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]  // ����ȭ �����ϴٴ� ��
public class IdleState <T> : IState where T : Monster
{
	[SerializeField] T owner;

	private Transform playerTransform;
	private float findRange = 5;

	public IdleState(T owner)
	{
		this.owner = owner;
	}

	public void Enter()
	{
		playerTransform = GameObject.FindWithTag("Player").transform;
	}
	public void Update()
	{
		// �ƹ��͵� ����
	}
	public void Exit()
	{
	}
}
