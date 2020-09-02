using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfo
{
	public int m_HP = 0;
	public int m_MaxHP = 0;
	public int m_ExtraHP = 0;

	public void Initialize(int nMaxHp)
	{
		m_MaxHP = nMaxHp;
		m_ExtraHP = CalculateAddHP();
		m_HP = nMaxHp + m_ExtraHP;
	}

	public void AddDamage(int nDamage)
	{
		m_HP -= nDamage;
		if (m_HP <= 0)
		{
			GameInfo kGameInfo = GameMng.Inst.m_GameInfo;
			m_HP = 0;
			kGameInfo.m_bSuccess = false;
		}
	}
	public void AddHP(int nHP)
	{
		m_HP += nHP;
	}

	public int CalculateAddHP()
	{
		SaveInfo kSaveInfo = GameMng.Inst.m_SaveInfo;
		return (int)(kSaveInfo.m_AccumulateScore * 0.001f * Config.DMIN_ADD_HP);
	}

	public int CalculateMaxHP()
	{
		return m_MaxHP;
	}
}
