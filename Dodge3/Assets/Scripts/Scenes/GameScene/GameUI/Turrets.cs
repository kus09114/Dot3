using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
	[SerializeField] GameObject m_Head = null;
	[SerializeField] Transform m_Target = null;
	[SerializeField] GameObject m_Bullet = null;
	[SerializeField] Transform m_FirePos = null;
	[SerializeField] Transform m_FireParent = null;

	[SerializeField] float m_fDelayTime = 1f;
	[SerializeField] float m_fBulletSpeed = 0;

	AudioSource m_EffectSound;

	bool m_bStart = false;

	private void Start()
	{
		m_EffectSound = GetComponent<AudioSource>();
	}

	public void Initialize(Transform target)
	{
		SetIsFire(true);
		m_Target = target;
		CreateBulletObject();
	}

	void Update()
	{
		FollowTarget();
	}

	void FollowTarget()
	{
		m_Head.transform.LookAt(m_Target);
	}

	public void CreateBulletObject()
	{
		int nValue = Random.Range(1, 10);
		float fDelay = m_fDelayTime + (float)nValue * 0.1f;
		StartCoroutine("IEnumFunc_CreateBullet", fDelay);
	}

	IEnumerator IEnumFunc_CreateBullet(float delay)
	{
		while (m_bStart)
		{
			m_EffectSound.Play();
			CreateBullet(m_Target, m_FireParent);

			yield return new WaitForSeconds(delay);
		}
	}

	void CreateBullet(Transform kTarget, Transform kParent)
	{
		GameObject go = Instantiate(m_Bullet, kParent);
		go.transform.position = m_FirePos.position;
		Bullet kBullet = go.GetComponent<Bullet>();

		kBullet.Initialize(kTarget.position, m_fBulletSpeed, kTarget);
	}

	public void SetIsFire(bool bStart)
	{
		m_bStart = bStart;
	}
}
