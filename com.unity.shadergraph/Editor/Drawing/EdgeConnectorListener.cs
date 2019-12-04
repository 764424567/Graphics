using UnityEngine;

using UnityEditor.Experimental.GraphView;
using UIEdge = UnityEditor.Experimental.GraphView.Edge;

namespace UnityEditor.ShaderGraph.Drawing
{
    class EdgeConnectorListener : IEdgeConnectorListener
    {
        readonly GraphData m_Graph;
        readonly SearchWindowProvider m_SearchWindowProvider;

        public EdgeConnectorListener(GraphData graph, SearchWindowProvider searchWindowProvider)
        {
            m_Graph = graph;
            m_SearchWindowProvider = searchWindowProvider;
        }

        public void OnDropOutsidePort(UIEdge edge, Vector2 position)
        {
            if(m_Graph == null || m_SearchWindowProvider == null)
                return;

            var draggedPort = (edge.output != null ? edge.output.edgeConnector.edgeDragHelper.draggedPort : null) ?? (edge.input != null ? edge.input.edgeConnector.edgeDragHelper.draggedPort : null);
            m_SearchWindowProvider.connectedPort = (ShaderPort)draggedPort;
            SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), m_SearchWindowProvider);
        }

        public void OnDrop(GraphView graphView, UIEdge edge)
        {
            if(m_Graph == null || m_SearchWindowProvider == null)
                return;
                
            // Horizontal ports
            var leftSlot = edge.output.GetSlot();
            var rightSlot = edge.input.GetSlot();
            if (leftSlot != null && rightSlot != null)
            {
                m_Graph.owner.RegisterCompleteObjectUndo("Connect Edge");
                m_Graph.Connect(leftSlot, rightSlot);
            }

            // Vertical ports
            var outputData = edge.output.userData as PortData;
            var inputData = edge.input.userData as PortData;
            if (outputData != null && inputData != null)
            {
                m_Graph.owner.RegisterCompleteObjectUndo("Connect Edge");
                m_Graph.Connect(outputData, inputData);
            }
        }
    }
}
