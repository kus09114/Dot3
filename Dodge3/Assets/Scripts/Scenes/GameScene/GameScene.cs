using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
	public GameUI m_GameUI = null;
	public HubUI m_HubUI = null;
	[HideInInspector] public BattleFSM m_BattleFSM = null;

	void Awake()
	{
		AssetMng.Inst.Initialize();

		GameMng.Inst.LoadFile();
		InitFSM();
	}

	void Start()
	{
		GameMng.Inst.Initialize();
		GameMng.Inst.SetGameScene(this);
		Initialize();
	}

	public void Initialize()
	{
		m_BattleFSM.SetReadyState();
	}

	public void InitFSM()
	{
		if (m_BattleFSM == null)
		{
			m_BattleFSM = new BattleFSM();
			m_BattleFSM.Initialize(Callback_ReadyEnter, Callback_WaveEnter, Callback_GameEnter, Callback_ResultEnter);
		}
	}

	void Update()
	{
		if (m_BattleFSM != null)
		{
			m_BattleFSM.OnUpdate();

			m_HubUI.m_HPBarUI.OnUpdate();
			m_HubUI.m_StageUI.OnUpdate();

			if (m_BattleFSM.IsGameState())
			{
				GameMng.Inst.OnUpdate(Time.deltaTime);
				m_HubUI.m_LifeTimeUI.OnUpdate();
				m_HubUI.m_KeepTimeBarUI.OnUpdate();
			}
		}
	}

	public void Callback_ReadyEnter()
	{
		Initialize_ReadyState();
	}
	public void Callback_WaveEnter()
	{

	}
	public void Callback_GameEnter()
	{
		Initialize_GameState();
	}
	public void Callback_ResultEnter()
	{
		Initialize_ResultState();
	}

	private void Initialize_ReadyState()
	{

		GameMng.Inst.InitStart();
		m_HubUI.Initialize_ReadyState();
		m_GameUI.Initialize_ReadyState();
	}
	private void Initialize_GameState()
	{
		m_HubUI.Initialize_GameState();
		m_GameUI.Initialize_GameState();
	}
	private void Initialize_ResultState()
	{
		m_HubUI.Initialize_ResultState();
		m_GameUI.Initialize_ResultState();
	}

	private void OnApplicationQuit()
	{
		Debug.Log("[GameScene] App Quit......");
		try
		{
			GameMng.Inst.SaveFile();
		}
		catch (System.Exception e)
		{
			Debug.Log("[_Error_Quit]" + e.ToString());
		}
	}
}
