
public enum Operation
{
    ADD,
    ADD_PERCENT,
    SUB,
    SUB_PERCENT,
    MUL,
    DIV,
}

public enum Stats
{
    HEALTH,
    DAMAGE,
    SPEED
}

public struct CharacterData
{

    private float _Health;
    private float _Speed;
    private float _Damage;

    public CharacterData(float health, float speed, float damage)
    {
        _Health = health;
        _Speed = speed;
        _Damage = damage;
    }

    public void SetStats(Stats s, float value)
    {
        switch (s)
        {
            case Stats.HEALTH:
                _Health = value;
                break;
            case Stats.DAMAGE:
                _Damage = value;
                break;
            case Stats.SPEED:
                _Speed = value;
                break;
        }
    }

    public float GetStats(Stats s)
    {
        switch (s)
        {
            case Stats.HEALTH:
                return _Health;
                break;
            case Stats.DAMAGE:
                return _Damage;
                break;
            case Stats.SPEED:
                return _Speed;
                break;
            default:
                return 0;
        }
    }

    public override string ToString()
    {
        return $"Health:{_Health}|Speed:{_Speed}|Damage:{_Damage}";
    }
}
















///------------





