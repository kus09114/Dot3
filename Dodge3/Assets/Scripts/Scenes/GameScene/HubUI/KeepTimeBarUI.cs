using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepTimeBarUI : MonoBehaviour
{
	[SerializeField] Text m_txtValue = null;
	[SerializeField] Image m_imgKeepTimeBar = null;

	public void Initialize()
	{

	}

	public void OnUpdate()
	{
		GameInfo kGameInfo = GameMng.Inst.m_GameInfo;

		m_imgKeepTimeBar.fillAmount = kGameInfo.GetCurrentKeepTime() / kGameInfo.m_AseetStage.m_fStageKeepTime;
		int time = (int)kGameInfo.GetCurrentKeepTime();
		m_txtValue.text = time.ToString();

	}
}
