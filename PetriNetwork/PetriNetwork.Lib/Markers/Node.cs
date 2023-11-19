namespace PetriNetwork.Lib.Markers;

public class Node
{
    private double _prevTimeUpdate = 0;
    public double TimeInSystem { get; set; }

    private double _waitingStartTime = -1;
    public double TotalWaitingTime { get; set; }
    public double RepairTime { get; set; }
    public int CycleCount { get; set; }
    
    public void UpdateSystemTime(double currTime)
    {
        TimeInSystem += currTime - _prevTimeUpdate;
    }

    public void UpdateWaitingTime(double currTime)
    {
        _prevTimeUpdate = currTime;
        if (_waitingStartTime == -1)
        {
            _waitingStartTime = currTime;
        }
        else
        {
            TimeInSystem += currTime - _waitingStartTime;
            TotalWaitingTime += currTime - _waitingStartTime;
            _waitingStartTime = -1;
        }
    }
}