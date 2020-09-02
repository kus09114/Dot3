using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public Turrets[] m_Turrets = null;
	public PlayerController m_PlayerController = null;
	public ItemObjectMng m_ItemObjectMng = null;
	public GameObject m_BulletParent = null;

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
		m_PlayerController.PlayerMoving(true);

		// 각 터렛 초기화
		for (int i = 0; i < m_Turrets.Length; i++)
		{
			m_Turrets[i].Initialize(m_PlayerController.transform);
		}
		Initialize_Items();
	}
	public void Initialize_ResultState()
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;
		m_PlayerController.PlayerMoving(false);

		DestroyAllBullet();

		for (int i = 0; i < m_Turrets.Length; i++)
		{
			m_Turrets[i].SetIsFire(false);
		}
	}

	void MyCollision_Enter(Collider other)
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;

		if (other.gameObject.tag == "Bullet")
		{
			kGameInfo.AddDamage();
		}
		if(other.gameObject.tag == "Item")
		{
			m_ItemObjectMng.HideItem(other.gameObject);

			// CItemObj에 있는 m_ID의 값을 가져온다.
			// 먹었던 아이템의 type을 AssetItem에 있는 m_nItemType과 비교
			CItemObj kItemObj = other.gameObject.GetComponent<CItemObj>();
			AssetItem kAssetItem = AssetMng.Inst.GetAssetItem(kItemObj.m_ID);

			if( kAssetItem.m_nItemType == (int)ItemInfo.Type.eHealing )
			{
				kGameInfo.m_ActorInfo.AddHP((int)kAssetItem.m_fValue);
				ActionHealingEffect();
			}
			if( kAssetItem.m_nItemType == (int)ItemInfo.Type.eExplose)
			{
				TurretFire(false);
				DestroyAllBullet();
				ActionExploseEffect();
				Invoke("CI_ReStartFire", kAssetItem.m_fValue);
			}
		}
	}

	void ActionHealingEffect()
	{
		m_ItemObjectMng.ActionEffect((int)ItemInfo.Type.eHealing);
	}
	void ActionExploseEffect()
	{
		m_ItemObjectMng.ActionEffect((int)ItemInfo.Type.eExplose);
	}

	public void DestroyAllBullet()
	{
		Bullet[] kBullets = m_BulletParent.GetComponentsInChildren<Bullet>();
		foreach (Bullet kBullet in kBullets)
		{
			Destroy(kBullet.gameObject, 0.01f);
		}
	}

	public void TurretFire(bool bStart)
	{
		for (int i = 0; i < m_Turrets.Length; i++)
		{
			if (bStart)
				m_Turrets[i].ReStart();
			else
				m_Turrets[i].Stop();
		}
	}

	public void CI_ReStartFire()
	{
		TurretFire(true);
	}
}