using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{

    /// <summary>
    /// Selector runs input nodes and returns succes if one input node returns success
    /// </summary>
    public class CheckNode : BTNode
    {
        private BTNode[] inputNodes;
        private Func<bool> checkFunction;
        public CheckNode(BlackBoard blackBoard, Func<bool> _checkFunction,  params BTNode[] input)
        {
            checkFunction = _checkFunction;
            this.blackBoard = blackBoard;
            inputNodes = input;
        }
        public override BTNodeStatus Tick()
        {
            if (!checkFunction() )
            {
                return BTNodeStatus.succes;
            }
            foreach (BTNode node in inputNodes)
            {
                BTNodeStatus result = node.Tick();

                switch (result)
                {
                    case BTNodeStatus.failed:
                        return BTNodeStatus.failed;
                    case BTNodeStatus.running:
                        return BTNodeStatus.running;
                    case BTNodeStatus.succes:
                        //only at succes it moves on to the next task
                        break;
                    default:
                        break;
                }
            }

            return BTNodeStatus.succes;
        }
    }
}
