using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
	public GameUI m_GameUI = null;
	public HubUI m_HubUI = null;
	[HideInInspector] public BattleFSM m_BattleFSM = null;

	void Start()
	{
		InitFSM();
		Initialize();
	}


	public void Initialize()
	{
		m_GameUI.Initialize();
		m_HubUI.Initialize();
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
		if (m_BattleFSM == null)
		{
			InitFSM();
		}
	}

	public void Callback_ReadyEnter()
	{

	}
	public void Callback_WaveEnter()
	{

	}
	public void Callback_GameEnter()
	{

	}
	public void Callback_ResultEnter()
	{

	}
}
