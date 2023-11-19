﻿using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Markers.Filters;
using PetriNetwork.Lib.Markers.Queues;
using PetriNetwork.Lib.Network;
using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;
using PetriNetwork.Lib.Transitions.ConflictResolvers;
using PetriNetwork.Lib.Transitions.DelayProviders;
using PetriNetwork.Lib.Transitions.Processors;

namespace PetriNetwork.RepairWorkshop;

static class Program
{
    public static void Main()
    {
        ByTypeMarkerFilter nodeFilter = new ByTypeMarkerFilter(typeof(Node));
        ByTypeMarkerFilter intFilter = new ByTypeMarkerFilter(typeof(int));

        // Node creation
        Position startP = new Position("Node start", new List<object>(){ 1 });
        Transition creationT = new Transition(
            "Creation", new ExponentialDelayProvider(10.25), 
            processor:new NodeCreationProcessor(new ErlangDelayProvider(22, 242)));
        
        creationT.ArcsIn.Add(new ArcIn(startP, creationT));
        startP.ArcsIn.AddRange(creationT.ArcsIn);
        creationT.ArcsOut.Add(new ArcOut(startP, creationT), intFilter);

        // Repair
        Position repairQueueP = new Position("Repair Queue", new PriorityQueue<object>(new NodePrioritySelector()));
        creationT.ArcsOut.Add(new ArcOut(repairQueueP, creationT), nodeFilter);
        Position repairStationP = new Position("Repair Station", new List<object>() { 1 });
        Transition repairT = new Transition("Repair", new NodeRepairDelayProvider());
        
        repairT.ArcsIn.Add(new ArcIn(repairStationP, repairT));
        repairStationP.ArcsIn.AddRange(repairT.ArcsIn);
        repairT.ArcsOut.Add(new ArcOut(repairStationP, repairT), intFilter);
        repairT.ArcsIn.Add(new ArcIn(repairQueueP, repairT));
        
        // Control
        Position controlQueueP = new Position("Control Queue");
        repairT.ArcsOut.Add(new ArcOut(controlQueueP, repairT), nodeFilter);
        Position controlStationP = new Position("Control Station", new List<object>() { 1 });
        Transition controlT = new Transition("Control", new ConstantDelayProvider(6));
        
        controlT.ArcsIn.Add(new ArcIn(controlStationP, controlT));
        controlStationP.ArcsIn.AddRange(controlT.ArcsIn);
        controlT.ArcsIn.Add(new ArcIn(controlQueueP, controlT));
        controlT.ArcsOut.Add(new ArcOut(controlStationP, controlT), intFilter);
        
        // Exit 
        Position checkedP = new Position("Checked");
        controlT.ArcsOut.Add(new ArcOut(checkedP, controlT), nodeFilter);
        Position repairedP = new Position("Repaired");
        Transition exitT = new Transition("Exit", new ConstantDelayProvider(0));
        
        exitT.ArcsIn.Add(new ArcIn(checkedP, exitT));
        checkedP.ArcsIn.AddRange(exitT.ArcsIn);
        exitT.ArcsOut.Add(new ArcOut(repairedP, exitT), nodeFilter);

        // Return
        Transition returnT = new Transition("Return", new ConstantDelayProvider(0));
        
        returnT.ArcsIn.Add(new ArcIn(checkedP, returnT));
        checkedP.ArcsIn.AddRange(returnT.ArcsIn);
        returnT.ArcsOut.Add(new ArcOut(repairQueueP, returnT), nodeFilter);

        // network build
        List<Transition> transitions = new() { creationT, repairT, controlT, exitT, returnT };
        List<Position> positions = new()
            { startP, repairQueueP, repairStationP, controlQueueP, controlStationP, checkedP, repairedP };
        List<Arc> arcs = new List<Arc>();
        foreach (var transition in transitions)
        {
            arcs.AddRange(transition.ArcsIn);
            arcs.AddRange(transition.ArcsOut.Keys);
        }

        ByNodeCycleConflictResolver conflictResolver = new ByNodeCycleConflictResolver(returnT);
        Network petriNet = new Network(transitions, positions, arcs, conflictResolver);
    }
}