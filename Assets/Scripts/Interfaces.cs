public interface IWeapon
{
    void Fire();
}

public interface IFlightPattern
{
    float speed { get; set; }
    void DoFlightPattern();
}