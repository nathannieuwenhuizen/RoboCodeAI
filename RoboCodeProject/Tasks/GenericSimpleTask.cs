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
    public class GenericSimpleTask : BTNode
    {
        private Func<bool?> callFunction;
        public GenericSimpleTask(BlackBoard blackBoard, Func<bool?> _callFunction) {
            callFunction = _callFunction;
            this.blackBoard = blackBoard;
        }
        public override BTNodeStatus Tick(){
            callFunction();
            return BTNodeStatus.succes;
        }
    }
}
