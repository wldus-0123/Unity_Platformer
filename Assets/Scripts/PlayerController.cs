using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] Rigidbody2D rigid;  // 2D�� Rigidbody2D ����ؾ���
	[SerializeField] SpriteRenderer render;
	[SerializeField] Animator animator;

	[Header("Property")]
	[SerializeField] float movePower;    // ���� �󸶳� ������
	[SerializeField] float brakePower;
	[SerializeField] float maxXSpeed;
	[SerializeField] float maxYSpeed;

	[SerializeField] float jumpSpeed;

	[SerializeField] LayerMask groundCheck;

	private Vector2 moveDir;  // �Է¹��� ��ġ��
	private bool isGround;    // �÷��̾ ���ٴڿ� �ִ��� ����

	private void FixedUpdate()  // rigidbody (��������)
	{
		Move();
	}

	private void Move()

		// �÷����� ���� ���۰� �߿� : �������� �ݹ߷� Ȱ��
	{
		if (moveDir.x < 0 && rigid.velocity.x > -maxXSpeed)  // �������� �Է��ϰ� �����鼭, �ְ�ӵ����� ������
		{
			rigid.AddForce(Vector2.right * moveDir.x * movePower);
		}

		else if (moveDir.x > 0 && rigid.velocity.x < maxXSpeed)  // ���������� �Է��ϰ� �����鼭, �ְ�ӵ����� ���� ��
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

		// ��ǻ� ������ ���� �� �ƹ��͵� ���ϰ� ����
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
		if(groundCheck.Contain(collision.gameObject.layer))  // groundCheck ���̾�� �߿� collision.gameObject.layer�� �ֳ�?
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
