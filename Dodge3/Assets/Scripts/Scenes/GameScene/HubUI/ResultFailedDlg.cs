using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultFailedDlg : MonoBehaviour
{
	public GameObject m_ResultFailedDlg = null;

	[SerializeField] Text m_txtStage = null;

	[SerializeField] Button m_btnReStart = null;
	[SerializeField] Button m_btnExit = null;

	void Start()
	{
		m_btnReStart.onClick.AddListener(OnClick_BtnReStart);
		m_btnExit.onClick.AddListener(OnClick_BtnExit);
	}

	public void Initialize()
	{
		SetCurStage();
		Show(true);
	}

	public void Show(bool bShow)
	{
		m_ResultFailedDlg.SetActive(bShow);
	}

	private void SetCurStage()
	{
		GameInfo kGameinfo = GameMng.Inst.m_GameInfo;

		string str = kGameinfo.m_nStage.ToString();
		m_txtStage.text = "Stage " + str;
	}

	public void OnClick_BtnReStart()
	{
		GameScene kGameScene = gameObject.GetComponentInParent<GameScene>();
		Show(false);
		kGameScene.m_HubUI.m_ReadyDlg.Show(true);
		kGameScene.m_BattleFSM.SetReadyState();
	}
	public void OnClick_BtnExit()
	{
		SaveInfo kSaveinfo = GameMng.Inst.m_SaveInfo;

		kSaveinfo.SaveFila();
		SceneManager.LoadScene("MainScene");
	}
}
