using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAsset
{
	public int m_nId = 0;
}

public class AssetItem : CAsset
{
	public int m_nItemType = 0;
	public string m_sPrefabName = "";
	public float m_fValue = 0;
	public string m_sDesc = "";
}

public class AssetStage : CAsset
{
	public float m_fFireDelayTime = 0;       // 총알 발사 지연시간
	public float m_fBulletSpeed = 0;         // 총알 속도
	public float m_fStageKeepTime = 0;       // 스테이지 유지시간
	public int m_nPlayerHP = 0;              // 플레이어 체력
	public int m_nBulletAttack = 0;          // 총알 공격력(데미지)
	public float m_fItemAppearDelay = 0;     // 아이템 생성 지연 시간 또는 간격
	public float m_fItemKeepTime = 0;        // 아이템 화면 출력 유지 시간
	public int m_nTurretCount = 0;           // 터렛 갯수
}

public class AssetMng
{
	static AssetMng _instance = null;
	public static AssetMng Inst
	{
		get
		{
			if (_instance == null)
				_instance = new AssetMng();

			return _instance;
		}
	}

	private AssetMng()
	{

		IsInstalled = false;
	}
	//------------------------------------------------------
	public bool IsInstalled { get; set; } // Property : 속성
	public List<AssetStage> m_AssetStages = new List<AssetStage>();
	public List<AssetItem> m_AssetItems = new List<AssetItem>();

	public void Initialize()
	{
		IsInstalled = true;
		Initialize_Item();
		Initialize_Stage();
	}

	public void Initialize_Item()
	{
		List<string[]> dataList = CSVParser.Load2("Assets/Resources/TableData/item.csv");

		for (int i = 1; i < dataList.Count; i++)
		{
			string[] data = dataList[i];

			AssetItem item = new AssetItem();
			item.m_nId = int.Parse(data[0]);
			item.m_nItemType = int.Parse(data[1]);
			item.m_sPrefabName = data[2];
			item.m_fValue = float.Parse(data[3]);
			item.m_sDesc = data[4];

			m_AssetItems.Add(item);
		}
	}
	public void Initialize_Stage()
	{
		List<string[]> dataList = CSVParser.Load2("Assets/Resources/TableData/stage.csv");

		for (int i = 1; i < dataList.Count; i++)
		{
			string[] data = dataList[i];

			AssetStage stage = new AssetStage();
			stage.m_nId = int.Parse(data[0]);
			stage.m_fFireDelayTime = float.Parse(data[1]);
			stage.m_fBulletSpeed = float.Parse(data[2]);
			stage.m_fStageKeepTime = float.Parse(data[3]);
			stage.m_nPlayerHP = int.Parse(data[4]);
			stage.m_nBulletAttack = int.Parse(data[5]);
			stage.m_fItemAppearDelay = float.Parse(data[6]);
			stage.m_fItemKeepTime = float.Parse(data[7]);
			stage.m_nTurretCount = int.Parse(data[8]);

			m_AssetStages.Add(stage);
		}
	}

	public AssetStage GetAssetStage(int nStage)
	{
		if (nStage > 0 && nStage <= m_AssetStages.Count)
		{
			return m_AssetStages[nStage - 1];
		}
		return null;
	}

	public AssetItem GetAssetItem(int id)
	{
		if (id > 0 && id <= m_AssetItems.Count)
		{
			return m_AssetItems[id - 1];
		}
		return null;
	}

	public int GetAssetItemCount()
	{
		return m_AssetItems.Count;
	}
}
