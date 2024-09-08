using CatRunner.Cat.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatRunner.Cat.CatFunctions
{
    public static class DynamicVariables
    {
        public static void Clean(Executor executor)
        {
            foreach (var item in executor.LocalScopes[executor.LocalScopes.Count - 1])
            {
                if (executor.CurrentVariables.ContainsKey(item.Key))
                {
                    executor.CurrentVariables.Remove(item.Key);
                }
            }
            executor.LocalScopes.RemoveAt(executor.LocalScopes.Count - 1);
        }
    }
}
