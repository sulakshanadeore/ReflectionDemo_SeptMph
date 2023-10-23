using System;
using System.Reflection;

namespace ReflectionDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //runtime--information of the assembly at runtime 

            Assembly assembly = Assembly.LoadFile("D:\\NewDrive\\MphasisSept\\Calculator\\Calculator\\bin\\Debug\\Calculator.dll");

            Module[] modules=assembly.GetModules();
            foreach (var modulename in modules)
            {
                Console.WriteLine(modulename.Name);
                Console.WriteLine("------------------");
                Console.WriteLine("Types in the module are:");
                Type[] typearr=modulename.GetTypes();//class[]
                foreach (var classnames in typearr)
                {
                    Console.WriteLine(classnames.Name);
                    
                    Console.WriteLine($"Is Public={classnames.IsPublic}");
                    Console.WriteLine($"Is Class={classnames.IsClass}");
                    Console.WriteLine($"Is Abstract={classnames.IsAbstract}");


                    Console.WriteLine("------------------");
                    Console.WriteLine("Members in the class");

                    MemberInfo[] memberInfo= classnames.GetMembers();
                    foreach (var item in memberInfo)
                    {
                        Console.WriteLine(item.Name);
                        Console.WriteLine(item.MemberType);//it tells whether its a method/property
                                              Console.WriteLine();
                    }
                    MethodInfo[] methods=classnames.GetMethods();
                    Console.WriteLine("Method Info");
                    foreach (var method in methods)
                    {
                        Console.WriteLine(method.Name);
                        ParameterInfo[] parameterInfos=method.GetParameters();
                        foreach (var pinfo in parameterInfos)
                        {
                            Console.WriteLine(pinfo.Name);
                            Console.WriteLine(  pinfo.ParameterType);
                            Console.WriteLine(pinfo.Position);

                        }
                        


                    }

                    if (classnames.Name == "GeneralFunctions")
                    {
                        Type t = assembly.GetType("Calculator.GeneralFunctions");
                        object obj = Activator.CreateInstance(t);//creating object of class
                       object ans= t.InvokeMember("Add", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, obj,new object[] {10,10 });
                        Console.WriteLine(ans);
                        Console.WriteLine(  "calling clear method ");
                        t.InvokeMember("Clear", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, obj, null);



                    }


                }


            }



            //PrintNameOfAssembly(assembly);
            //Type typeassembly=assembly.GetType();
            //Console.WriteLine(typeassembly);




            Console.Read();

        }

        private static void PrintNameOfAssembly(Assembly assembly)
        {
            Console.WriteLine($"FullName={assembly.FullName}");
            Console.WriteLine($"GetName()= {assembly.GetName()}");
        }
    }
}
