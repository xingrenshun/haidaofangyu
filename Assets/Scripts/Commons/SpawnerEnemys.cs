using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemys : MonoBehaviour {

	public GameObject[] EnemyObjects;

	public Vector3[] SpawnPosition;
		

	public IEnumerator CoSpawnEnemy(ENEMY _enemyType , float _TimeToStartSpawnThisEnemy)
	{
		yield return new WaitForSeconds (_TimeToStartSpawnThisEnemy);
		GameObject EnemyClone = (GameObject) Instantiate (EnemyObjects[(int) _enemyType] , SpawnPosition[Random.Range(0, SpawnPosition.Length)] ,transform.rotation);
	}
}
