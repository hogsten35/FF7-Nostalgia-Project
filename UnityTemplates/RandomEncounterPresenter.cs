using FF7Nostalgia.Core.Field;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class RandomEncounterPresenter : MonoBehaviour
{
    [SerializeField] private FieldPlayerController player;
    [SerializeField] private string battleSceneName = "Battle_Prototype";
    [SerializeField] private bool encountersEnabled = true;

    private EncounterMeter _meter;
    private bool _transitioning;

    private void Awake()
    {
        _meter = new EncounterMeter(18f, 42f);
    }

    private void Update()
    {
        if (!encountersEnabled || _transitioning || player == null)
            return;

        if (_meter.AddDistance(player.DistanceMovedThisFrame))
        {
            _transitioning = true;
            // Replace this immediate scene load with a swirl/fade transition later.
            SceneManager.LoadScene(battleSceneName);
        }
    }
}
