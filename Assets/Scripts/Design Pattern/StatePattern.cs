// ���������� State

// <��������>
// ��ü���� �ѹ��� �ϳ��� ���¸��� ������ �ϸ� ��ü�� ������¿� �ش��ϴ� �ൿ���� ����

// ����:
// 1. ������ �ڷ������� ��ü�� ���� �� �ִ� ���µ��� ����
// 2. ���� ���¸� �����ϴ� ������ �ʱ� ���¸� ����
// 3. ��ü�� �ൿ�� �־ ���� ���¸��� �ൿ�� ����
// 4. ��ü�� ���� ������ �ൿ�� ������ �� ���� ��ȭ�� ���� �Ǵ�
// 5. ���� ��ȭ�� �־�� �ϴ� ��� ���� ���¸� ��� ���·� ���� 
// 6. ���°� ����� ��� ���� �ൿ�� �־ �ٲ� ���¸��� �ൿ�� ����

// ����:
// 1. ��ü�� ������ �ൿ�� ������ ���ǹ��� ���·� ó���� �����ϹǷ�, ���� ó���� ���� �δ��� ����
// 2. ��ü�� ������ ���� ���¿� ���� ������¸��� �����ϱ� ������, ����ӵ��� ������
// 3. ��ü�� ���õ� ��� ������ ������ ���¿� �л��Ű�Ƿ�, �ڵ尡 �����ϰ� �������� ����

// ����:
// 1. ������ ������ ��Ȯ���� �ʰų� ������ ���� ���, ���º��濡 ���� �ڵ尡 �������� �� �ִ�
// 2. ������ ������ ��ü�� ���������� �����ϴ� ���, ������ ������ ���� �ڵ差�� �����ϰ� �ȴ�
// 3. ���¸� ĸ��ȭ���� �ʴ� ��� ���°��� ������ �����ϹǷ�, ��������Ģ�� �ؼ����� ����


using UnityEngine;

namespace DesignPattern
{
	public class State
	{
		public class Mobile
		{
			public enum State { Off, Normal, Charge, FullCharged }

			private State state = State.Normal;
			private bool charging = false;
			private float battery = 50.0f;

			private void Update()
			{
				switch (state)
				{
					case State.Off:
						OffUpdate();
						break;
					case State.Normal:
						NormalUpdate();
						break;
					case State.Charge:
						ChargeUpdate();
						break;
					case State.FullCharged:
						FullChargedUpdate();
						break;
				}
			}

			private void OffUpdate()
			{
				// Off work
				// Do nothing

				if (charging)
				{
					state = State.Charge;
				}
			}

			private void NormalUpdate()
			{
				// Normal work
				battery -= 1.5f * Time.deltaTime;

				if (charging)
				{
					state = State.Charge;
				}
				else if (battery <= 0)
				{
					state = State.Off;
				}
			}

			private void ChargeUpdate()
			{
				// Charge work
				battery += 25f * Time.deltaTime;

				if (!charging)
				{
					state = State.Normal;
				}
				else if (battery >= 100)
				{
					state = State.FullCharged;
				}
			}

			private void FullChargedUpdate()
			{
				// FullCharged work

				if (!charging)
				{
					state = State.Normal;
				}
			}

			public void ConnectCharger()
			{
				charging = true;
			}

			public void DisConnectCharger()
			{
				charging = false;
			}
		}
	}
}