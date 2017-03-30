using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class ClassMemberFinder
    {
        public static ClassMember find(
            Class cls,string name,
            Class finder
            
            )
        {
            for (int i = 0; i < cls.classMembers.Count; i++)
            {
                if (cls.classMembers[i].name == name)
                {
                    var member = cls.classMembers[i];
                    if (!member.isPublic)
                    {
                        if (finder != null)
                        { 
                            if (member.isInternal)
                            {
                                if (finder.package == cls.package)
                                {
                                    return cls.classMembers[i];
                                }
                            }
                            else if (member.isPrivate)
                            {
                                if (finder == cls)
                                {
                                    return cls.classMembers[i];
                                }
                            }
                        }
                    }
                    else
                    {
                        return cls.classMembers[i];
                    }
                }
            }

            return null;
        }

    }
}
