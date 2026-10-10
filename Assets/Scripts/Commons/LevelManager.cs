using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour {

	[Serializable]
	public class Level
	{
		[Serializable]
		public class Enemy
		{
			public ENEMY EnemyType;
			public float TimeToStartSpawnThisEnemy;
		}

		public List<Enemy> ListEnemy;
	}

	public List<Level> ListLevel;

	public float TimeSpawnAllEnemyInCurrentLevel;

	public int CountEnemysInLevel;

	private SpawnerEnemys spawnerEnemys;

	void Start()
	{
		spawnerEnemys = FindObjectOfType<SpawnerEnemys> ();
	}

	public void SelectLevel(int _level)
	{
		foreach (Level.Enemy enemy in ListLevel[_level].ListEnemy) 
		{
			StartCoroutine (spawnerEnemys.CoSpawnEnemy(enemy.EnemyType , enemy.TimeToStartSpawnThisEnemy));
		}
	}
		
	public void TimeSpawnAllEnemysInLevel(int _level)
	{
		CountEnemysInLevel = ListLevel [_level].ListEnemy.Count;
		Level.Enemy enemyLast = ListLevel [_level].ListEnemy [CountEnemysInLevel - 1];
		TimeSpawnAllEnemyInCurrentLevel = enemyLast.TimeToStartSpawnThisEnemy;
	}

}
