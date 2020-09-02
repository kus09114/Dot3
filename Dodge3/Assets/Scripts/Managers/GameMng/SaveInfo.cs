using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class SStage
{
	public int m_nStage = 0;
	public int m_nScore = 0;

	public SStage(int stage, int score)
	{
		m_nStage = stage;
		m_nScore = score;
	}
}

public class SaveInfo : MonoBehaviour
{
	public List<SStage> m_Stages = new List<SStage>();

	public int m_LastStage = 1;
	public int m_AccumulateScore = 0;
	public int m_HighestScore = 0;

	public void SaveFila()
	{
		FileStream fs = new FileStream("stage.data", FileMode.Create, FileAccess.Write);
		BinaryWriter bw = new BinaryWriter(fs);

		bw.Write(m_LastStage);
		bw.Write(m_AccumulateScore);
		bw.Write(m_HighestScore);

		bw.Write(m_Stages.Count);
		for (int i = 0; i < m_Stages.Count; i++)
		{
			SStage kStage = m_Stages[i];
			bw.Write(kStage.m_nStage);
			bw.Write(kStage.m_nScore);
		}

		fs.Close();
		bw.Close();
	}

	public void LoadFile()
	{
		m_Stages.Clear();
		try
		{

			FileStream fs = new FileStream("stage.data", FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader(fs);

			m_LastStage = br.ReadInt32();
			m_AccumulateScore = br.ReadInt32();
			m_HighestScore = br.ReadInt32();

			int count = br.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				int stage = br.ReadInt32();
				int score = br.ReadInt32();

				SStage kStudent = new SStage(stage, score);
				m_Stages.Add(kStudent);
			}

			fs.Close();
			br.Close();
		}
		catch (Exception e)
		{
			Debug.Log(e.ToString());
		}
	}

	public void SetStage(int nStage)
	{
		m_LastStage = nStage;
	}
	public void SetLastStage(int nStage)
	{
		m_LastStage = nStage;
	}

	public void SetHighestScore(int score)
	{
		m_HighestScore = (score > m_HighestScore) ? score : m_HighestScore;
	}
	public void AddAccumulateScore(int score)
	{
		m_AccumulateScore += score;
	}
	public void SetStageScore(int nStage, int nScore)
	{
		SetHighestScore(nScore);
		AddAccumulateScore(nScore);

		if (nStage > m_Stages.Count)
		{
			SStage kStage = new SStage(nStage, nScore);
			m_Stages.Add(kStage);
		}
		else
		{
			SStage kStage = m_Stages[nStage - 1];
			kStage.m_nScore = nScore;

			if (kStage.m_nStage != nStage)
			{
				Debug.LogError("kStage.m_nStage != nStage");
			}
		}
	}
}
