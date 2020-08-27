using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
	[SerializeField] Text m_txtValue = null;
	[SerializeField] Image m_imgHPBar = null;

	public void Initialize()
	{

	}

	public void OnUpdate()
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;

		m_imgHPBar.fillAmount = kGameInfo.GetCurrentPlayerHP();
		m_txtValue.text = kGameInfo.m_ActorInfo.m_HP.ToString();
	}
}
