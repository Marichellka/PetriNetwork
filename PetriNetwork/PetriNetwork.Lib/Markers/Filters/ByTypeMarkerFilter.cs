namespace PetriNetwork.Lib.Markers.Filters;

public class ByTypeMarkerFilter: IMarkerFilter
{
    private readonly Type _markerType;

    public ByTypeMarkerFilter(Type markerType)
    {
        _markerType = markerType;
    }

    public object Filter(IEnumerable<object> markers)
    {
        return markers.Single(x => x.GetType() == _markerType);
    }
}