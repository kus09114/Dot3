using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float m_fMoveSpeed = 0;
	[SerializeField] Transform m_StartPos = null;
	[SerializeField] GameObject m_BoomEffect = null;

	public Action<Collider> OnMyCollision = null;

	private bool m_bMove = false;

	public void Initialize()
	{
		gameObject.transform.position = m_StartPos.position;
	}

	void FixedUpdate()
	{
		if (m_bMove)
		{
			float hor = Input.GetAxis("Horizontal");
			float ver = Input.GetAxis("Vertical");

			Vector3 pos = transform.position;

			pos.x += hor * Time.deltaTime * m_fMoveSpeed;
			pos.z += ver * Time.deltaTime * m_fMoveSpeed;

			transform.position = pos;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(OnMyCollision != null)
			OnMyCollision(other);
	}

	public void PlayerMoving(bool bMove)
	{
		m_bMove = bMove;
	}
}
