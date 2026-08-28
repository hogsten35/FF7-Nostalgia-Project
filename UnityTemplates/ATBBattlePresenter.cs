using FF7Nostalgia.Core.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ATBBattlePresenter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private TMP_Text playerMpText;
    [SerializeField] private TMP_Text enemyHpText;
    [SerializeField] private Slider playerAtbSlider;
    [SerializeField] private GameObject commandPanel;

    private BattleActor _hero;
    private BattleActor _enemy;
    private BattleEngine _engine;
    private ATBBattleController _timeline;
    private EnemyAI _enemyAI;
    private bool _waitingForPlayerCommand;

    private void Start()
    {
        _hero = new BattleActor("char_kael_001", "Kael", true, 620, 54, 34, 20, 25, 18, 31);
        _enemy = new BattleActor("enemy_marsh_hound", "Marsh Hound", false, 210, 0, 18, 8, 6, 7, 24);

        _engine = new BattleEngine(new[] { _hero }, new[] { _enemy });
        _timeline = new ATBBattleController(new[] { _hero, _enemy });
        _enemyAI = new EnemyAI();

        _timeline.OnActorReady += HandleActorReady;
        _engine.OnBattleEnded += HandleBattleEnded;

        commandPanel.SetActive(false);
        RefreshUI();
    }

    private void Update()
    {
        if (_engine.Result != BattleResult.InProgress) return;

        _timeline.Tick(Time.deltaTime);
        playerAtbSlider.value = _timeline.GetNormalizedGauge(_hero);
        RefreshUI();
    }

    public void Attack() => ExecutePlayerCommand(BattleCommand.Attack(), _enemy);
    public void Fire() => ExecutePlayerCommand(BattleCommand.Fire(), _enemy);
    public void Cure() => ExecutePlayerCommand(BattleCommand.Cure(), _hero);
    public void Defend() => ExecutePlayerCommand(BattleCommand.Defend(), _hero);

    private void HandleActorReady(BattleActor actor)
    {
        if (actor.IsPlayerControlled)
        {
            _waitingForPlayerCommand = true;
            commandPanel.SetActive(true);
            return;
        }

        var command = _enemyAI.ChooseCommand(actor);
        _engine.Execute(actor, command, _hero);
        _timeline.ConsumeTurn(actor);
        RefreshUI();
    }

    private void ExecutePlayerCommand(BattleCommand command, BattleActor target)
    {
        if (!_waitingForPlayerCommand) return;

        if (_engine.Execute(_hero, command, target))
        {
            _timeline.ConsumeTurn(_hero);
            _waitingForPlayerCommand = false;
            commandPanel.SetActive(false);
        }

        RefreshUI();
    }

    private void HandleBattleEnded(BattleResult result)
    {
        commandPanel.SetActive(false);
        Debug.Log($"Battle ended: {result}");
    }

    private void RefreshUI()
    {
        playerHpText.text = $"HP {_hero.CurrentHP}/{_hero.MaxHP}";
        playerMpText.text = $"MP {_hero.CurrentMP}/{_hero.MaxMP}";
        enemyHpText.text = $"HP {_enemy.CurrentHP}/{_enemy.MaxHP}";
    }
}
