namespace PetriNetwork.Lib.Transitions.Filters;

public interface IMarkerFilter
{
    public object Filter(IEnumerable<object> markers);
}