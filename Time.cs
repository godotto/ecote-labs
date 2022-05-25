namespace Evalutaion;

public class Time
{
    public float Value { get; }
    public string Unit { get; }

    public Time(float value, string unit)
    {
        Value = value;
        Unit = unit;
    }

    public Time(string timeString)
    {
        if (timeString.EndsWith('s') || timeString.EndsWith('h'))
        {
            Value = Convert.ToSingle(timeString.Substring(0, timeString.Length - 1));
            Unit = timeString.Substring(timeString.Length - 1);
        }
        else
        {
            Value = Convert.ToSingle(timeString.Substring(0, timeString.Length - 3));
            Unit = timeString.Substring(timeString.Length - 3);
        }
    }

    public override string ToString()
    {
        return $"{Value} {Unit}";
    }
}
