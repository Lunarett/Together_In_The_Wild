using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        SpawnPlayerRandom();
    }

	private void SpawnPlayerRandom()
	{
		float x = Random.Range(-3, 3);
		float y = Random.Range(-3, 3);
		Vector3 randomPosition = new Vector3(x, y, 0);
		GameObject player = PhotonNetwork.Instantiate(Path.Combine("Characters", playerPrefab.name), randomPosition, Quaternion.identity);
	}
}