using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteLoader : MarshalByRefObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyFile"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public T GetInstance<T>(string assemblyFile, string typeName) where T : class
        {
            var assembly = Assembly.LoadFrom(assemblyFile);
            var type = assembly.GetType(typeName);
            if (type == null) return null;
            return Activator.CreateInstance(type) as T;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="typeName"></param>
        /// <param name="methodName"></param>
        public void ExecuteMothod(string assemblyFile, string typeName, string methodName)
        {
            if (!File.Exists(assemblyFile))
            {
                assemblyFile = FindFileAuto(assemblyFile);
            }
            var assembly = Assembly.LoadFrom(assemblyFile);
            var type = assembly.GetType(typeName);
            var obj = Activator.CreateInstance(type);
            Expression<Action> lambda = Expression.Lambda<Action>(Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
            lambda.Compile()();
        }

        private string FindFileAuto(string assemblyFile)
        {
            string fileName = assemblyFile;
            string rootPath = Path.GetDirectoryName(assemblyFile);
            string name = Path.GetFileName(assemblyFile);
            foreach (var path in AppDomain.CurrentDomain.SetupInformation.PrivateBinPath.Split(';'))
            {
                fileName = Path.Combine(rootPath, path, name);
                if (File.Exists(fileName))
                {
                    break;
                }
            }
            return fileName;
        }
    }

}
