namespace PetriNetwork.Lib.Network;

public interface INetworkItem
{
    public string Name { get; }
    public void DebugPrint();
}