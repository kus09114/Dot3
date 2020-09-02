using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
	[SerializeField] Text m_txtValue = null;
	[SerializeField] Image m_imgHPBar = null;

	GameScene m_GameScene = null;

	public void Initialize()
	{
		m_GameScene = gameObject.GetComponentInParent<GameScene>();
	}

	public void OnUpdate()
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;

		if (kGameInfo.GetCurrentPlayerHP() <= 0)
		{
			m_GameScene.m_BattleFSM.SetResultState();
		}

		m_imgHPBar.fillAmount = kGameInfo.GetCurrentPlayerHP();
		m_txtValue.text = kGameInfo.m_ActorInfo.m_HP.ToString();
	}
}
