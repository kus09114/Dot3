using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepTimeBarUI : MonoBehaviour
{
	[SerializeField] Text m_txtValue = null;
	[SerializeField] Image m_imgKeepTimeBar = null;

	GameScene m_GameScene = null;

	public void Initialize()
	{
		m_GameScene = gameObject.GetComponentInParent<GameScene>();
	}

	public void OnUpdate()
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;

		if(kGameInfo.GetCurrentKeepTime() <= 0)
		{
			m_GameScene.m_BattleFSM.SetResultState();
		}

		m_imgKeepTimeBar.fillAmount = kGameInfo.GetCurrentKeepTime() / kGameInfo.m_AseetStage.m_fStageKeepTime;
		int time = (int)kGameInfo.GetCurrentKeepTime();
		m_txtValue.text = time.ToString();
	}
}
