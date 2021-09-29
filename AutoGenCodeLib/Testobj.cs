using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace as3runtime
{
    
}


namespace AutoGenCodeLib
{
    


    class RefB
    {
        public int V;

        public RefB(object ffff ,System.DateTime time)
        {
            Console.WriteLine(ffff);
            Console.WriteLine(time);
        }

        public void LLLL(string a, string b, int c)
        {
            
        }

        public Guid LLLL(string a, float c)
        {
            return new Guid();
        }

        public override string ToString()
        {
            return "RefB";
        }

        public static void MMM(float a)
        {
            
        }

    }

    public class MethodInvokeResult
    {
        public object value;
        public bool isVoid;
    }

    public sealed class MethodInvoker
    {
        private object instance;
        private string key;
        internal MethodInfo findmethod;
        public MethodInvoker(object instance, string key)
        {
            this.instance = instance;
            this.key = key;
        }


        public static MethodInvokeResult Invoke(string typeName, string funName, object[] param)
        {
            try
            {
                Type type = Type.GetType(typeName);
                
                Type[] types = null;
                if (param != null)
                {
                    types = new Type[param.Length];
                    for (int i = 0; i < types.Length; i++)
                    {
                        if (param[i] != null)
                        {
                            types[i] = param[i].GetType();
                        }
                        else
                        {
                            types[i] = typeof(void);
                        }
                    }
                }

                var binder = new MyBinder();
               
                var method =
                         type.GetMethod(funName, System.Reflection.BindingFlags.Public | BindingFlags.Static, binder,
                         types, null);

                if (method == null)
                {
                    throw new NotSupportedException("静态方法的重载没有找到");
                }

                var result = method.Invoke(null, BindingFlags.Default,binder, param,null);
                MethodInvokeResult invokeResult = new MethodInvokeResult();
                invokeResult.value = result;
                if (method.ReturnType == typeof(void))
                {
                    invokeResult.isVoid = true;
                }
                else
                {
                    invokeResult.isVoid = false;
                }

                return invokeResult;
            }
            catch (Exception ex)
            {

                throw new NotSupportedException(ex.ToString(), ex);
            }
        }

        public MethodInvokeResult Invoke(object[] param)
        {

            try
            {
                MyBinder binder = null;
                if (findmethod == null)
                {
                    Type[] types = null;
                    if (param != null)
                    {
                        types = new Type[param.Length];
                        for (int i = 0; i < types.Length; i++)
                        {
                            if (param[i] != null)
                            {
                                types[i] = param[i].GetType();                               
                            }
                            else
                            {
                                types[i] = typeof(void);
                            }
                        }
                    }

                    binder = new MyBinder();

                    var method =
                             instance.GetType().GetMethod(key, System.Reflection.BindingFlags.Public | BindingFlags.Instance, binder,
                             types, null);

                    if (method == null)
                    {
                        throw new NotSupportedException("方法的重载没有找到");
                    }
                    findmethod = method;
                }

                var result= findmethod.Invoke(instance, BindingFlags.Default, binder, param,null );
                MethodInvokeResult invokeResult = new MethodInvokeResult();
                invokeResult.value = result;
                if (findmethod.ReturnType == typeof(void))
                {
                    invokeResult.isVoid = true;
                }
                else
                {
                    invokeResult.isVoid = false;
                }

                return invokeResult;
            }
            catch (Exception ex)
            {

                throw new NotSupportedException(ex.ToString(), ex);
            }

           

        }


    }

    public class FindMemberResult
    {
        public FindMemberResult(bool m, object r)
        {
            isMethod = m;
            result = r;
        }

        public bool isMethod;
        public object result;
    }


    public sealed class ReflectUtil
    {
        public static object CreateInstance(string typeName,object[] args)
        {
            try
            {
                
                Type type = Type.GetType(typeName);

                //Assembly asm2 = type.Assembly;
                Object obj2 =
                    Activator.CreateInstance(type,BindingFlags.Default, new MyBinder(), args,null);
                    //asm2.CreateInstance(typeName, true, BindingFlags.Default, null, args, null, null);
                    

                return obj2;
            }
            catch (Exception ex)
            {

                throw new NotSupportedException(ex.ToString(), ex);
            }

        }

        

        public  static FindMemberResult FindMember(object instance,string key)
        {
            var members= instance.GetType().GetMember(key);

            if (members.Length == 0)
            {
                throw new NotSupportedException( instance.GetType().ToString()+" 不存在成员 " + key );
            }
            if (members.Length > 0)
            {
                var m = members[0];
                if (m is System.Reflection.PropertyInfo)
                {
                    PropertyInfo property = (PropertyInfo)m;
                    return new FindMemberResult(false,  property.GetValue(instance, null));

                }
                else if (m is FieldInfo)
                {
                    FieldInfo field = (FieldInfo)m;
                    return new FindMemberResult( false, field.GetValue(instance));

                }
                else if (m is MethodInfo)
                {
                    var r= new FindMemberResult(true,new MethodInvoker( instance,key));
                    if (members.Length == 1)
                    {
                        ((MethodInvoker)r.result).findmethod = (MethodInfo)m;
                    }

                    return r;
                }
            }

            throw new NotSupportedException("反射出了奇怪的东西");
        }

        public static void SetMember(object instance, string key, object value)
        {
            var members = instance.GetType().GetMember(key);

            if (members.Length == 0)
            {
                throw new NotSupportedException(instance.GetType().ToString() + " 不存在成员 " + key);
            }
            if (members.Length > 0)
            {
                var m = members[0];
                if (m is System.Reflection.PropertyInfo)
                {
                    try
                    {
                        PropertyInfo property = (PropertyInfo)m;
                        property.SetValue(instance, value, null);
                    }
                    catch (Exception ex)
                    {

                        throw new NotSupportedException(ex.ToString());
                    }
                    

                }
                else if (m is FieldInfo)
                {
                    try
                    {
                        FieldInfo field = (FieldInfo)m;
                        field.SetValue(instance, value);
                    }
                    catch (Exception ex)
                    {
                        throw new NotSupportedException(ex.ToString());
                    }

                }
                else if (m is MethodInfo)
                {
                    throw new NotSupportedException(m.ToString()+" 不可赋值");

                }
            }
        }

    }


    class MyBinder : Binder
    {
        public MyBinder() : base()
        {
        }
        private class BinderState
        {
            public object[] args;
        }
        public override FieldInfo BindToField(
            BindingFlags bindingAttr,
            FieldInfo[] match,
            object value,
            System.Globalization.CultureInfo culture
            )
        {
            if (match == null)
                throw new ArgumentNullException("match");
            // Get a field for which the value parameter can be converted to the specified field type.
            for (int i = 0; i < match.Length; i++)
                if (ChangeType(value, match[i].FieldType, culture) != null)
                    return match[i];
            return null;
        }
        public override MethodBase BindToMethod(
            BindingFlags bindingAttr,
            MethodBase[] match,
            ref object[] args,
            ParameterModifier[] modifiers,
            System.Globalization.CultureInfo culture,
            string[] names,
            out object state
            )
        {
            // Store the arguments to the method in a state object.
            BinderState myBinderState = new BinderState();
            object[] arguments = new System.Object[args.Length];
            args.CopyTo(arguments, 0);
            myBinderState.args = arguments;
            state = myBinderState;
            if (match == null)
                throw new ArgumentNullException();
            // Find a method that has the same parameters as those of the args parameter.
            for (int i = 0; i < match.Length; i++)
            {
                if (match[i].IsGenericMethodDefinition)
                    continue;

                // Count the number of parameters that match.
                int count = 0;
                ParameterInfo[] parameters = match[i].GetParameters();
                // Go on to the next method if the number of parameters do not match.
                if (args.Length != parameters.Length)
                    continue;
                // Match each of the parameters that the user expects the method to have.
                for (int j = 0; j < args.Length; j++)
                {
                    // If the names parameter is not null, then reorder args.
                    if (names != null)
                    {
                        if (names.Length != args.Length)
                            throw new ArgumentException("names and args must have the same number of elements.");
                        for (int k = 0; k < names.Length; k++)
                            if (String.Compare(parameters[j].Name, names[k].ToString()) == 0)
                                args[j] = myBinderState.args[k];
                    }
                    // Determine whether the types specified by the user can be converted to the parameter type.
                    //if ( ChangeType(args[j], parameters[j].ParameterType, culture) != null)
                    //    count += 1;
                    //else
                    //    break;

                    if (args[j] == null && !parameters[j].ParameterType.IsValueType)
                    {
                        count += 1;
                    }
                    else if (ChangeType(args[j], parameters[j].ParameterType, culture) != null)
                    {
                        count += 1;
                    }
                    else if (parameters[j].ParameterType.IsInstanceOfType(args[j]))
                    {
                        count += 1;
                    }
                    else
                        break;

                }
                // Determine whether the method has been found.
                if (count == args.Length)
                    return match[i];
            }
            return null;
        }

        public override object ChangeType(object value, Type type, System.Globalization.CultureInfo culture)
        {


            // Determine whether the value parameter can be converted to a value of type myType.
            if (value !=null && CanConvertFrom(value.GetType(), type))
                // Return the converted object.
                return Convert.ChangeType(value, type);
            else
                // Return null.
                return null;

        }

        public override void ReorderArgumentArray(
            ref object[] args,
            object state
            )
        {
            // Return the args that had been reordered by BindToMethod.
            ((BinderState)state).args.CopyTo(args, 0);
        }
        public override MethodBase SelectMethod(
            BindingFlags bindingAttr,
            MethodBase[] match,
            Type[] types,
            ParameterModifier[] modifiers
            )
        {
            if (match == null)
                throw new ArgumentNullException("match");
            for (int i = 0; i < match.Length; i++)
            {
                // Count the number of parameters that match.
                int count = 0;
                ParameterInfo[] parameters = match[i].GetParameters();
                // Go on to the next method if the number of parameters do not match.
                if (types.Length != parameters.Length)
                    continue;
                // Match each of the parameters that the user expects the method to have.
                for (int j = 0; j < types.Length; j++)
                    // Determine whether the types specified by the user can be converted to parameter type.
                    if ((types[j].Equals(parameters[j].ParameterType)))
                        count += 1;
                    else if (CanConvertFrom(types[j], parameters[j].ParameterType))
                    {
                        count += 1;
                    }
                    else if (typeof(void).Equals(types[j]) && !parameters[j].ParameterType.IsValueType)
                    {
                        count += 1;
                    }
                    else
                        break;
                // Determine whether the method has been found.
                if (count == types.Length)
                    return match[i];
            }
            return null;
        }
        public override PropertyInfo SelectProperty(
            BindingFlags bindingAttr,
            PropertyInfo[] match,
            Type returnType,
            Type[] indexes,
            ParameterModifier[] modifiers
            )
        {
            if (match == null)
                throw new ArgumentNullException("match");
            for (int i = 0; i < match.Length; i++)
            {
                // Count the number of indexes that match.
                int count = 0;
                ParameterInfo[] parameters = match[i].GetIndexParameters();
                // Go on to the next property if the number of indexes do not match.
                if (indexes.Length != parameters.Length)
                    continue;
                // Match each of the indexes that the user expects the property to have.
                for (int j = 0; j < indexes.Length; j++)
                    // Determine whether the types specified by the user can be converted to index type.
                    if ((indexes[j].Equals(parameters[j].ParameterType)))
                        count += 1;
                    else
                        break;
                // Determine whether the property has been found.
                if (count == indexes.Length)
                    // Determine whether the return type can be converted to the properties type.
                    if ((returnType.Equals(match[i].PropertyType)))
                        return match[i];
                    else
                        continue;
            }
            return null;
        }
        // Determines whether type1 can be converted to type2. Check only for primitive types.
        private bool CanConvertFrom(Type type1, Type type2)
        {
            if (type1.IsPrimitive && type2.IsPrimitive)
            {
                TypeCode typeCode1 = Type.GetTypeCode(type1);
                TypeCode typeCode2 = Type.GetTypeCode(type2);
                // If both type1 and type2 have the same type, return true.
                if (typeCode1 == typeCode2)
                    return true;
                // Possible conversions from Char follow.
                if (typeCode1 == TypeCode.Char)
                    switch (typeCode2)
                    {
                        case TypeCode.UInt16: return true;
                        case TypeCode.UInt32: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Byte follow.
                if (typeCode1 == TypeCode.Byte)
                    switch (typeCode2)
                    {
                        case TypeCode.Char: return true;
                        case TypeCode.UInt16: return true;
                        case TypeCode.Int16: return true;
                        case TypeCode.UInt32: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from SByte follow.
                if (typeCode1 == TypeCode.SByte)
                    switch (typeCode2)
                    {
                        case TypeCode.Int16: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from UInt16 follow.
                if (typeCode1 == TypeCode.UInt16)
                    switch (typeCode2)
                    {
                        case TypeCode.UInt32: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Int16 follow.
                if (typeCode1 == TypeCode.Int16)
                    switch (typeCode2)
                    {
                        case TypeCode.Int32: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from UInt32 follow.
                if (typeCode1 == TypeCode.UInt32)
                    switch (typeCode2)
                    {
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Int32 follow.
                if (typeCode1 == TypeCode.Int32)
                    switch (typeCode2)
                    {
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from UInt64 follow.
                if (typeCode1 == TypeCode.UInt64)
                    switch (typeCode2)
                    {
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Int64 follow.
                if (typeCode1 == TypeCode.Int64)
                    switch (typeCode2)
                    {
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Single follow.
                if (typeCode1 == TypeCode.Single)
                    switch (typeCode2)
                    {
                        case TypeCode.Double: return true;
                        default: return false;
                    }


                //***不安全的将double转为float
                if (typeCode1 == TypeCode.Double)
                    switch (typeCode2)
                    {
                        case TypeCode.Int32: return true;
                        case TypeCode.Single: return true;
                        default: return false;
                    }


            }
            return false;
        }

    }



}

