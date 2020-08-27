using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng
{
	static GameMng _instance = null;
	public static GameMng Inst
	{
		get
		{
			if (_instance == null)
				_instance = new GameMng();

			return _instance;
		}
	}

	public GameInfo m_GameInfo = new GameInfo();
	public SaveInfo m_SaveInfo = new SaveInfo();

	private GameScene m_GameScene = null;

	public void Initialize()
	{
		Application.runInBackground = true;
	}

	public void InitStart()
	{
		m_GameInfo.Initialize();
	}

	public void SetGameScene (GameScene kGameScene)
	{
		m_GameScene = kGameScene;
	}
	public GameScene GetGameScene() { return m_GameScene; }

	public void OnUpdate(float fElasedTime)
	{
		m_GameInfo.OnUpdate(fElasedTime);
	}

	public void SaveFile()
	{
		m_SaveInfo.SaveFila();
	}
	public void LoadFile()
	{
		m_SaveInfo.LoadFile();
	}
}
