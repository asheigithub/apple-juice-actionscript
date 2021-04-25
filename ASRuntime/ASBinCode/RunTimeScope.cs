using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{
    public sealed class RunTimeScope //: IRunTimeScope
    {
        
        public int blockId;
        public RunTimeScope(

            HeapSlot[] memberDataList,
            
            int blockid,
            RunTimeScope parent,
           
            RunTimeValueBase this_pointer,
            RunTimeScopeType type
            )
        {
            
            
            this.blockId = blockid;
            this.parent = parent;

            this.memberData = memberDataList;

            this.this_pointer = this_pointer;
            this.scopeType = type;

        }



        public readonly HeapSlot[] memberData;
        

        

        public readonly RunTimeScope parent;
        
        public readonly RunTimeValueBase this_pointer;



        public readonly RunTimeScopeType scopeType;


        


    }

    

}
