namespace PetriNetwork.Lib.Markers.Filters;

public interface IMarkerFilter
{
    public object Filter(IEnumerable<object> markers);
}