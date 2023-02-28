using UnityEngine;

public class PlayerSpawners : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int numLives = 3; // number of lives the player has
    [SerializeField] private float respawnTimer = 3.0f; // time to wait before respawning
    private GameObject playerInstance;
    private GameObject playerParent;

    private void Awake()
    {
        playerParent = GameObject.Find("Player");
        playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity, playerParent.transform);
    }

    private void Update()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if(playerInstance == null && numLives > 0)
        {
            respawnTimer -= Time.deltaTime;

            if(respawnTimer <= 0)
            {
                numLives--;
                respawnTimer = 3.0f;
                playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity, playerParent.transform);
            }
        }
    }

    private void OnGUI()
    {
        if(numLives > 0 || playerInstance!= null)
        {
            GUI.skin.label.fontSize = 36;
            GUI.Label(new Rect(10, 5, 250, 100), "Lives left: " + numLives);
        }
        else
        {
            GUI.skin.label.fontSize = 50;
            GUI.Label(new Rect( Screen.width / 2 - 50 , Screen.height / 2 - 25, 300, 150), "Game Over");
        }
    }
}