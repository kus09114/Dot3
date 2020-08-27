using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubUI : MonoBehaviour
{
	public HPBarUI m_HPBarUI = null;
	public KeepTimeBarUI m_KeepTimeBarUI = null;
	public LifeTimeUI m_LifeTimeUI = null;
	public ReadyDlg m_ReadyDlg = null;
	public StageUI m_StageUI = null;
	public ResultSuccesDlg m_ResultSuccesDlg = null;
	public ResultFailedDlg m_ResultFailedDlg = null;

	public void Initialize()
	{
		//m_ResultSuccesDlg.Initialize();
		//m_ResultFailedDlg.Initialize();
	}

	public void Initialize_ReadyState()
	{
		m_ReadyDlg.Initialize();
	}

	public void Initialize_GameState()
	{
		//m_HPBarUI.Initialize();
		//m_KeepTimeBarUI.Initialize();
	}
}
