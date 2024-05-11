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
    [Space]

    public List<BrickView> bricks;

    private BrickService brickService;
    private ModsController modController;
    private MockModsFactory modFactory;
    private PaddleCollisionHandler collisionHandler;
    private ScoreController scoreController;

    private void Start()
    {     
        collisionHandler = new PaddleCollisionHandler(paddle.CollisionDetector);
        modFactory = new MockModsFactory(modConfig);
        modController = new(modConfig, modFactory, collisionHandler);

        modController.ModAdded += (ModificatorData data) => Debug.Log($"{data.Type} added");
        modController.ModRemoved += (ModificatorData data) => Debug.Log($"{data.Type} removed");

        moveController.Construct(paddleConfig, paddle, zone);
        ball.Mover.SetSpeed(8f);
        ball.Mover.SetDirection(Vector2.one);

        brickService = new BrickService();

        var brickMap = new Dictionary<BrickView, BrickData>()
        {
            { bricks[0], new BrickData(ModType.None, 1)},
            { bricks[1], new BrickData(ModType.Magnet, 1)},
            { bricks[2], new BrickData(ModType.BallKeeper, 1)},
            { bricks[3], new BrickData(ModType.EnergyBall, 5)},
            { bricks[4], new BrickData(ModType.Expand, 1)}
        };

        brickService.Init(brickMap);

        brickService.OnHitBrick += OnHitBrick;

        scoreController = new ScoreController(brickService, brickConfig, new ScoreData());
        scorePanel.Construct(scoreController);
    }

    private void OnHitBrick(HitBrickData data)
    {
        var index = Random.Range(0, capsules.Length - 1);
        var capsule = capsules[index];

        Instantiate(capsule, data.Position, Quaternion.identity);
    }

    private void Update()
    {
        for (int i = 0; i < modFactory.timers.Count; i++)
        {
            var timer = modFactory.timers[i];
            timer.Update(Time.deltaTime);
        }
    }
}
