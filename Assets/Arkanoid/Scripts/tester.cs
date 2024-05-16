using Arkanoid.Ball;
using Arkanoid.Bricks;
using Arkanoid.PowerUPs;
using Arkanoid.Gameplay;
using Arkanoid.Gameplay.Mods;
using Arkanoid.Input;
using Arkanoid.Paddle;
using Arkanoid.Paddle.FX.Configs;
using System.Collections.Generic;
using UnityEngine;
using Arkanoid.UI;
using Arkanoid.Gameplay.Data;
using Arkanoid.Paddle.FX.Laser;
using Arkanoid.Infrastracture.AssetService;
using Arkanoid.Infrastracture;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Arkanoid;
using Arkanoid.Levels;

public class tester : MonoBehaviour
{
    public BallView ball;
    public PaddleView paddle;
    public PaddleConfig paddleConfig;
    public TouchZone zone;
    public PaddleMobileMoveController moveController;
    public ModsConfig modConfig;
    public PowerUpView[] capsules;
    public BrickConfig brickConfig;
    public ScorePanel scorePanel;
    public Projectile projectile;
    public LaserGun laserGun;
    [Space]

    public List<BrickView> bricks;

    private BrickService brickService;
    private ModsController modController;
    private MockModsFactory modFactory;
    private ScoreController scoreController;
    private ProjectileFactory projectileFactory;
    private AssetProvider assetProvider;


    private void Start()
    {
        var gameDataProvider = new GameDataProvider(new JsonSaveLoadService(new NewtonJsonSerializator()), null, null);

        gameDataProvider.Load();

        Debug.Log(gameDataProvider.Data.CurrentLevelIndex);
        gameDataProvider.Save();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Destroy(go);
        //    assetProvider.Release(reference);
        //}
    }

    //private void OnHitBrick(HitBrickData data)
    //{
    //    var index = Random.Range(0, capsules.Length - 1);
    //    var capsule = capsules[index];
    //    Instantiate(capsule, data.Position, Quaternion.identity);
    //}

    //private void Update()
    //{
    //    for (int i = 0; i < modFactory.timers.Count; i++)
    //    {
    //        var timer = modFactory.timers[i];
    //        timer.Update(Time.deltaTime);
    //    }
    //}

}
