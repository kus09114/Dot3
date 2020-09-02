using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
	public int m_nStage = 1;
	public bool m_bSuccess = false;
	public float m_fDurationTime = 0;

	public ActorInfo m_ActorInfo = new ActorInfo();
	public List<ItemInfo> m_ListItemInfo = new List<ItemInfo>();
	public AssetStage m_AseetStage = new AssetStage();

	public void Initialize()
	{
		SaveInfo kSaveInfo = GameMng.Inst.m_SaveInfo;
		m_nStage = kSaveInfo.m_LastStage;
		if (m_nStage <= 0)
			m_nStage = 1;

		Initialize_Stage(m_nStage);
		Initialize_Item();
	}

	public void Initialize_Stage(int nStage)
	{
		m_AseetStage = AssetMng.Inst.GetAssetStage(nStage);
		m_ActorInfo.Initialize(m_AseetStage.m_nPlayerHP);
		m_fDurationTime = 0;
	}

	public void Initialize_Item()
	{
		for (int i = 0; i < AssetMng.Inst.m_AssetItems.Count; i++)
		{
			AssetItem kAss = AssetMng.Inst.m_AssetItems[i];
			ItemInfo kInfo = new ItemInfo();
			kInfo.Initialize(kAss.m_nItemType, kAss.m_fValue);
			m_ListItemInfo.Add(kInfo);
		}
	}

	public int CaluclateScore()
	{
		int curHP = m_ActorInfo.m_HP;
		int maxHP = m_ActorInfo.CalculateMaxHP();
		int nScore = (int)(((float)curHP / maxHP) * Config.DMAX_SCORE);
		if (nScore < Config.DMIN_SCORE)
			nScore = Config.DMIN_SCORE;
		return nScore;
	}

	public void AddDamage()
	{
		m_ActorInfo.AddDamage(m_AseetStage.m_nBulletAttack);
	}

	public void OnUpdate(float fElasedTime)
	{
		m_fDurationTime += fElasedTime;
	}
	public bool IsSucces()
	{
		return m_bSuccess;
	}

	public float GetCurrentPlayerHP()
	{
		float curHP = (float)m_ActorInfo.m_HP;
		int maxHP = m_ActorInfo.CalculateMaxHP();
		float fScore = curHP / maxHP;
		return fScore;
	}
	public float GetCurrentKeepTime()
	{
		float curtime = m_AseetStage.m_fStageKeepTime - m_fDurationTime;
		if (curtime <= 0)
		{
			curtime = 0;
			m_bSuccess = true;
		}
		return curtime;
	}

	// 아이템을 먹었을 때의 결과 처리
	// 리턴값 : 2번 아이템의 처리 결과 유지시간 정보
	public ItemInfo ActionItem(int nItemId)
	{
		ItemInfo kInfo = GetItemInfo(nItemId);
		if (kInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
		{
			m_ActorInfo.m_HP += (int)kInfo.m_ItemValue;
		}
		if (kInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
		{

		}
		return kInfo;
	}

	public ItemInfo GetItemInfo(int id)
	{
		if (id > 0 && id <= m_ListItemInfo.Count)
		{
			return m_ListItemInfo[id - 1];
		}
		return null;
	}
}