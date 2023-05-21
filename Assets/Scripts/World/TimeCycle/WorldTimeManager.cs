using UnityEngine;

public class WorldTimeManager : MonoBehaviour
{
    [SerializeField] private float _timeScale = 1.0f;
    private float _startTime;
    private float _worldTime;
    private int _seconds;
    private int _minutes;
    private int _hours;
    private int _days;

    public static WorldTimeManager Instance;

    public float WorldTime { get => _worldTime; }
    public int Seconds { get => _seconds; }
    public int Minutes { get => _minutes; }
    public int Hours { get => _hours; }
    public int Days { get => _days; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        // Calculate World Time
        _worldTime = (Time.time - _startTime) * _timeScale;

        // Calculate Seconds
        _seconds = Mathf.FloorToInt(_worldTime % 60);

        // Calculate Minutes
        _minutes = Mathf.FloorToInt(_worldTime / 60) % 60;

        // Calculate Hours
        _hours = Mathf.FloorToInt(_worldTime / 3600) % 24;

        // Calculate Days
        _days = Mathf.FloorToInt(_worldTime / 86400);
    }
}
