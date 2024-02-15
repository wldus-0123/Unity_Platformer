using UnityEngine;

public class Eagle : Monster
{


	[SerializeField] float moveSpeed;


	public Transform playerTransform;
	public Vector3 startPos;  // 처음 위치


	public enum State { Idle, Trace, Return, Die }

	[Header("FSM")]
	private StateMachine<State, Eagle> fsm;  // 상태 머신

	private IdleState<Eagle> idleState;



	private void Awake()
	{
		fsm = new StateMachine<State, Eagle>();

		fsm.AddState(State.Idle, idleState);
		fsm.SetInitState(State.Idle);
		//fsm.AddState(State.Idle, traceState);
		//fsm.AddState(State.Idle, returnState);
	}

	void Start()
	{
		//playerTransform = GameObject.FindWithTag("Player").transform;       // Update에서는 Find 사용하지말것

		startPos = transform.position;
	}


	private class TraceState : IState
	{
		private Eagle owner;
		private Transform playerTransform;
		private float findRange;
		private float moveSpeed;

		public TraceState(Eagle owner)
		{
			this.owner = owner;
		}

		public void Enter()
		{
			playerTransform = GameObject.FindWithTag("Player").transform;
		}
		public void Update()
		{
			Vector3 dir = (playerTransform.position - owner.transform.position).normalized;
			owner.transform.Translate(dir * moveSpeed * Time.deltaTime);

			if (Vector2.Distance(playerTransform.position, owner.transform.position) > findRange)
			{
				owner.ChangeState("Return");
			}
		}
		public void Exit()
		{

		}
	}

	private class ReturnState : IState
	{
		private Eagle owner;
		private Transform playerTransform;
		private float moveSpeed = 10;

		public ReturnState(Eagle owner)
		{
			this.owner = owner;
		}

		public void Enter()
		{
			playerTransform = GameObject.FindWithTag("Player").transform;
		}
		public void Update()
		{
			Vector3 dir = (owner.startPos - owner.transform.position).normalized;
			owner.transform.Translate(dir * moveSpeed * Time.deltaTime);

			if (Vector2.Distance(owner.transform.position, owner.startPos) < 0.01f)
			{
				owner.ChangeState("Idle");
			}

		}
		public void Exit()
		{

		}
	}


	void Update()
	{
		//      Vector3 playerPos = playerTransform.position;

		//      if((playerPos-transform.position).magnitude < findRange)
		//      {
		//	Vector3 dir = (playerPos - transform.position).normalized;   // 벡터에서 도착지 - 출발지 = 방향
		//	// float scale = (playerPos - transform.position).magnitude;    // 크기

		//	transform.Translate(dir * moveSpeed * Time.deltaTime);
		//}

		fsm.Update();

		if ()
		{
			// 상태변경
			fsm.ChangeState(State.Trace);
		}

	}

	public void ChangeState(string stateName)
	{
		switch (stateName)
		{
			case "Idle":
				fsm.ChangeState(State.Idle);
				break;
			case "Trace":
				fsm.ChangeState(State.Trace);
				break;
			case "Return":
				fsm.ChangeState(State.Return);
				break;
		}
	}




	//private void IdleUpdate()
	//{
	//	if (Vector2.Distance(playerTransform.position, transform.position) < findRange)
	//	{
	//		curState = State.Trace;
	//	}
	//}
	//private void TraceUpdate()
	//{
	//	Vector3 dir = (playerTransform.position - transform.position).normalized;
	//	transform.Translate(dir * moveSpeed * Time.deltaTime);

	//	if (Vector2.Distance(playerTransform.position, transform.position) > findRange)
	//	{
	//		curState = State.Return;
	//	}
	//}
	//private void ReturnUpdate()
	//{
	//	Vector3 dir = (startPos - transform.position).normalized;
	//	transform.Translate(dir * moveSpeed * Time.deltaTime);

	//	if(Vector2.Distance(transform.position, startPos) < 0.01f)
	//	{
	//		curState = State.Idle;
	//	}
	//	if(Vector2.Distance(transform.position, playerTransform.position) < findRange)
	//	{
	//		curState = State.Trace;
	//	}
	//}
	//private void DiedUpdate()
	//{

	//}
}
