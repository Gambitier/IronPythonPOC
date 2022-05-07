using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace IronPythonPOC
{
    public class PythonActivity
    {
        private ScriptEngine engine;
        private ScriptScope scope;
        private ScriptSource source;
        private CompiledCode compiled;
        private dynamic pythonClass;
        private dynamic instance;

        public void CreatePythonActivity(string code, string className = "PyClass")
        {
            //creating engine and stuff
            engine = Python.CreateEngine();
            scope = engine.CreateScope();

            //loading and compiling code
            source = engine.CreateScriptSourceFromString(code, Microsoft.Scripting.SourceCodeKind.Statements);
            compiled = source.Compile();

            //now executing this code (the code should contain a class)
            compiled.Execute(scope);


            instance = engine.Operations.CreateInstance(scope.GetVariable(className));

            //now creating an object that could be used to access the stuff inside a python script
            pythonClass = engine.Operations.Invoke(scope.GetVariable(className));
        }

        public void SetScopeVariable(string variable, dynamic value)
        {
            scope.SetVariable(variable, value);
        }

        public dynamic GetScopeVariable(string variable)
        {
            return scope.GetVariable(variable);
        }
        
        public dynamic GetInstanceVariable(string variable)
        {
            var value = engine.Operations.GetMember(pythonClass, variable);
            return value;
        }

        public void CallMethod(string method, params dynamic[] arguments)
        {
            engine.Operations.InvokeMember(pythonClass, method, arguments);
        }

        public dynamic CallFunction(string method, params dynamic[] arguments)
        {
            return engine.Operations.InvokeMember(pythonClass, method, arguments);
        }
    }
}
