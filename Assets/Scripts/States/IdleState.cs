using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]  // 직렬화 가능하다는 뜻
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
		// 아무것도 안함
	}
	public void Exit()
	{
	}
}
