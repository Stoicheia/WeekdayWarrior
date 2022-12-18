using System.Collections;
using Entity;
using Game;
using Music;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) Debug.LogError("GameManager missing.");
            return instance;
        }
    }

    // Progress statistics
    private float survivalTime = 0; // Time spent alive, in seconds
    private int gruntsDefeated = 0; // Enemies killed
    //private int bossesDefeated = 0;

    public GameObject playerPrefab;
    public UpgradeSystem upgradeSystem;
    public RhythmManager rhythmManager;
    public GameObject gameOverScreen;
    public OverworldHUD HUD;

    private GameObject playerReference;
    private PlayerManager activePlayer;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerReference = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        activePlayer = playerReference.GetComponent<PlayerManager>();
        upgradeSystem.ActivePlayer = activePlayer;
        StartCoroutine(StartAfter(0.5f));

        EnemyDirector.IsActive = true;
        EnemyGruntController.IsActive = true;
        activePlayer.IsActive = true;

        ExpLevelEntity.OnLevelUp += (i, e) =>
        {
            upgradeSystem.OpenUpgradeScreen(2);
            rhythmManager.FadeToRestAudio();
            EnemyGruntController.IsActive = false;
            activePlayer.IsActive = false;
            EnemyDirector.IsActive = false;
        };

        UpgradeSystem.OnClose += () =>
        {
            rhythmManager.FadeToMainAudio();
            EnemyGruntController.IsActive = true;
            activePlayer.IsActive = true;
            EnemyDirector.IsActive = true;
        };
    }

    private void Update() {
        survivalTime += Time.deltaTime;
        HUD.DisplayStats(activePlayer);
    }

    public void EndGame() {
        // Game over
        Time.timeScale = 0.0f;

        int totalScore = (int)survivalTime * 10 + gruntsDefeated * 5;
        // TODO: leaderboard
        gameOverScreen.SetActive(true);
    }

    public void IncrementGruntsDefeated() {
        gruntsDefeated++;
    }

    IEnumerator StartAfter(float t)
    {
        yield return new WaitForSeconds(t);
        rhythmManager.StartMainAudio();
    }

}
