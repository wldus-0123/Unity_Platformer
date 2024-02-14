using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] Rigidbody2D rigid;  // 2D는 Rigidbody2D 사용해야함
	[SerializeField] SpriteRenderer render;
	[SerializeField] Animator animator;

	[Header("Property")]
	[SerializeField] float movePower;    // 힘을 얼마나 받을지
	[SerializeField] float brakePower;
	[SerializeField] float maxXSpeed;
	[SerializeField] float maxYSpeed;

	[SerializeField] float jumpSpeed;

	[SerializeField] LayerMask groundCheck;

	private Vector2 moveDir;  // 입력받을 위치값
	private bool isGround;    // 플레이어가 땅바닥에 있는지 여부

	private void FixedUpdate()  // rigidbody (물리엔진)
	{
		Move();
	}

	private void Move()

		// 플랫포머 게임 조작감 중요 : 물리엔진 반발력 활용
	{
		if (moveDir.x < 0 && rigid.velocity.x > -maxXSpeed)  // 왼쪽으로 입력하고 있으면서, 최고속도보다 느릴때
		{
			rigid.AddForce(Vector2.right * moveDir.x * movePower);
		}

		else if (moveDir.x > 0 && rigid.velocity.x < maxXSpeed)  // 오른쪽으로 입력하고 있으면서, 최고속도보다 느릴 때
		{
			rigid.AddForce(Vector2.right * moveDir.x * movePower);
		}

		else if (moveDir.x == 0 && rigid.velocity.x > 0.1f)
		{
			rigid.AddForce(Vector2.left * brakePower);
		}

		else if (moveDir.x == 0 && rigid.velocity.x < -0.1f)
		{
			rigid.AddForce(Vector2.right * brakePower);
		}

		if (rigid.velocity.y < -maxYSpeed)
		{
			Vector2 velocity = rigid.velocity;
			velocity.y = -maxYSpeed;
			rigid.velocity = velocity;
		}

		animator.SetFloat("YSpeed", rigid.velocity.y);
	}

	private void Jump()
	{
		Vector2 velocity = rigid.velocity;
		velocity.y = jumpSpeed;
		rigid.velocity = velocity;
	}

	private void OnMove(InputValue value)
	{
		moveDir = value.Get<Vector2>();

		if(moveDir.x < 0)
		{
			render.flipX = true;
			animator.SetBool("Run", true);
		}
		else if(moveDir.x > 0)
		{
			render.flipX = false;
			animator.SetBool("Run", true);
		}
		else
		{
			animator.SetBool("Run", false);
		}

		// 사실상 가만히 있을 땐 아무것도 안하고 유지
	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed  && isGround)
		{
			Jump();
		}
	}

	private int groundCount;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(groundCheck.Contain(collision.gameObject.layer))  // groundCheck 레이어들 중에 collision.gameObject.layer가 있나?
		{
			groundCount++;
			isGround = groundCount > 0;
			animator.SetBool("IsGround", isGround);
		}

	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (groundCheck.Contain(collision.gameObject.layer))
		{
			groundCount--;
			isGround = groundCount > 0;
			animator.SetBool("IsGround", isGround);
		}
	}
}
