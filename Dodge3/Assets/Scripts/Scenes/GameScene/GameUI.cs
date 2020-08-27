using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public Turrets[] m_Turrets = null;
	public PlayerController m_PlayerController = null;
	public ItemObjectMng m_ItemObjectMng = null;

	public void Initialize()
	{

	}

	public void Initialize_Items()
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;

		float fKeepTime = kGameInfo.m_AseetStage.m_fItemKeepTime;
		float fDelay = kGameInfo.m_AseetStage.m_fItemAppearDelay;

		m_ItemObjectMng.Initialize(fKeepTime, fDelay);
	}

	public void Initialize_ReadyState()
	{
		// 플레이어 초기화
		m_PlayerController.Initialize();

		m_PlayerController.OnMyCollision = MyCollision_Enter;
	}
	public void Initialize_GameState()
	{
		m_PlayerController.Playeroving(true);

		// 각 터렛 초기화
		for (int i = 0; i < m_Turrets.Length; i++)
		{
			m_Turrets[i].Initialize(m_PlayerController.transform);
		}
		Initialize_Items();
	}

	void MyCollision_Enter(Collider other)
	{
		if (other.gameObject.tag == "Bullet")
		{

		}
	}
}
