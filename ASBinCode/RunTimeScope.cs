using ASBinCode;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;

namespace ASBinCode
{
    public sealed class RunTimeScope //: IRunTimeScope
    {
        public readonly CSWC swc;
       
        public readonly int offset;
        public readonly int blockId;
        public RunTimeScope(
            CSWC swc,
            //IList<IMember> members,
            SLOT[] memberDataList,
            SLOT[] rtStack,
            int offset, int blockid,
            RunTimeScope parent,
            Dictionary<int, ASBinCode.rtData.rtObject> _static_scope,
            RunTimeValueBase this_pointer,
            RunTimeScopeType type
            //,
            //Dictionary<ClassMethodGetter, Dictionary<ASBinCode.rtData.rtObject, ISLOT>> dictMethods
            )
        {
            this.swc = swc;

            this.stack = rtStack;
            this.offset = offset;
            this.blockId = blockid;
            this.parent = parent;

            this.memberData = memberDataList;
            this.static_objects = _static_scope;
            this.this_pointer = this_pointer;
            this.scopeType = type;
            //this._dictMethods = dictMethods;
        }



        public readonly SLOT[] memberData;
        

        

        public readonly RunTimeScope parent;
        

        public readonly SLOT[] stack;





        public readonly Dictionary<int, ASBinCode.rtData.rtObject> static_objects;
        

        public readonly RunTimeValueBase this_pointer;



        public readonly RunTimeScopeType scopeType;


        


    }

    

}
