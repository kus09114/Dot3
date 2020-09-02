using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSuccesDlg : MonoBehaviour
{
	public GameObject m_ResultSuccesDlg = null;

	[SerializeField] Text m_txtStage = null;
	[SerializeField] Text m_txtScore = null;
	[SerializeField] Text m_txtTotal = null;

	[SerializeField] Button m_btnReStart = null;
	[SerializeField] Button m_btnNext = null;
	[SerializeField] Button m_btnExit = null;

	void Start()
	{
		m_btnReStart.onClick.AddListener(OnClick_BtnReStart);
		m_btnNext.onClick.AddListener(OnClick_BtnNext);
		m_btnExit.onClick.AddListener(OnClick_BtnExit);
	}

	public void Initialize()
	{
		Setting();
		SetCurStage();
		SetCurScore();
		SetCurTotal();
		Show(true);
	}

	public void Show(bool bShow)
	{
		m_ResultSuccesDlg.SetActive(bShow);
	}

	private void Setting()
	{
		GameInfo kGameinfo = GameMng.Inst.m_GameInfo;
		SaveInfo kSaveinfo = GameMng.Inst.m_SaveInfo;

		kSaveinfo.SetStageScore(kGameinfo.m_nStage, kGameinfo.CaluclateScore());
	}

	private void SetCurStage()
	{
		GameInfo kGameinfo = GameMng.Inst.m_GameInfo;

		string str = kGameinfo.m_nStage.ToString();
		m_txtStage.text = "Stage " + str;
	}
	private void SetCurScore()
	{
		SaveInfo kSaveinfo = GameMng.Inst.m_SaveInfo;

		string str = kSaveinfo.m_HighestScore.ToString();
		m_txtScore.text = "Score : " + str;
	}
	private void SetCurTotal()
	{
		SaveInfo kSaveinfo = GameMng.Inst.m_SaveInfo;

		string str = kSaveinfo.m_AccumulateScore.ToString();
		m_txtTotal.text = "Total : " + str;
	}

	public void OnClick_BtnReStart()
	{
		GameScene kGameScene = gameObject.GetComponentInParent<GameScene>();

		kGameScene.m_BattleFSM.SetReadyState();
	}
	public void OnClick_BtnNext()
	{
		GameInfo kGameinfo = GameMng.Inst.m_GameInfo;
		SaveInfo kSaveinfo = GameMng.Inst.m_SaveInfo;

		kSaveinfo.SetLastStage(kGameinfo.m_nStage + 1);

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
