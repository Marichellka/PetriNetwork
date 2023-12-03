namespace PetriNetwork.Lib.Network;

public interface INetworkItem
{
    public string Name { get; }
    public double Mean { get; }
    public void DebugPrint();

    public void UpdateMean();
}