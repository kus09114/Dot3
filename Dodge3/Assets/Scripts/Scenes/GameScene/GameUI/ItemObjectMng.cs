using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectMng : MonoBehaviour
{
	public List<CItemObj> m_Items = null;
	public List<FXSerialize> m_FxList = null;

	public List<Transform> m_Postions = null;

	private float m_ItemKeepTime = 2.0f;
	private float m_ItemAppearDelay = 5.0f;

	private bool m_bCreateItem = false;

	public void Initialize(float fKeepTime, float fItemAppearDelay)
	{
		m_ItemKeepTime = fKeepTime;
		m_ItemAppearDelay = fItemAppearDelay;
		m_bCreateItem = true;
		StartCoroutine("Enumfunc_ItemCreate");
	}

	IEnumerator Enumfunc_ItemCreate()
	{
		float fKeepTime = m_ItemKeepTime;
		float fDelay = m_ItemAppearDelay;

		HideItems();

		while (m_bCreateItem)
		{
			yield return new WaitForSeconds(fDelay - fKeepTime);

			if (!m_bCreateItem)
				break;
			int nAssId = 0;
			int idx = MakeRandomItemObjectID(ref nAssId) - 1;
			CItemObj kItem = m_Items[idx];
			kItem.Initialize(nAssId, MakeRandomPos());

			yield return new WaitForSeconds(fKeepTime);
			kItem.Show(false);

			fDelay = MakeRandomDelay(m_ItemAppearDelay);
		}

		yield return null;
	}

	int MakeRandomItemObjectID(ref int rAssId)
	{
		int nItemCount = AssetMng.Inst.GetAssetItemCount();
		int id = Random.Range(1, nItemCount + 1);
		AssetItem kAssItem = AssetMng.Inst.GetAssetItem(id);

		rAssId = id;
		Debug.LogFormat("ItmeType = {0}", kAssItem.m_nItemType);
		return kAssItem.m_nItemType;
	}

	float MakeRandomDelay(float fDelay)
	{
		int nValue = Random.Range(-2, 2);
		fDelay += nValue;
		return fDelay;
	}

	public void HideItems()
	{
		for (int i = 0; i < m_Items.Count; i++)
		{
			m_Items[i].Show(false);
		}
	}

	public void HideItem(GameObject gameObject)
	{
		gameObject.SetActive(false);
	}

	public void SetIsCreateItem(bool bCreate)
	{
		m_bCreateItem = bCreate;
	}

	public void ActionEffect(int id)
	{
		if(id > 0 && id <= m_FxList.Count)
		{
			m_FxList[id - 1].Play();
		}
	}

	public void HideEffect(int id)
	{
		if (id > 0 && id <= m_FxList.Count)
		{
			m_FxList[id - 1].Show(false);
		}
	}

	public Vector3 MakeRandomPos()
	{
		Vector3 vMin = m_Postions[0].position;
		Vector3 vMax = m_Postions[1].position;

		float x = Random.Range(vMin.x, vMax.x);
		float z = Random.Range(vMin.z, vMax.z);

		return new Vector3(x, 0.5f, z);
	}
}