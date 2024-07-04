namespace Wq.Interpreter;

using System.Diagnostics;
using Wq.Value;

public class Engine
{
    private const int InterpreterSteps = 100;

    private readonly List<Interpreter> _interpreters = [];
    private readonly List<WqValue> _results = [];
    private LaunchMode _launchMode;

    public List<WqValue> Start(WqFuncDeclData[] funcDeclData, LaunchMode launchMode = LaunchMode.Release)
    {
        Debug.Assert(launchMode != LaunchMode.Invalid);

        _launchMode = launchMode;

        Clear();
        AddInterpreter(new Interpreter(funcDeclData));

        while (_interpreters.Count != 0)
        {
            Step();
            RemoveHalted();
        }

        return _results;
    }

    private void RemoveHalted()
    {
        for (var index = _interpreters.Count - 1; index >= 0; index--)
            if (_interpreters[index].Halted)
            {
                _results.Add(_interpreters[index].LastWqValue);
                _interpreters.RemoveAt(index);
            }
    }

    private void Step()
    {
        foreach (var interpreter in _interpreters)
            interpreter.Step(InterpreterSteps);
    }

    private void AddInterpreter(Interpreter interpreter)
    {
        interpreter.LaunchMode = _launchMode;
        _interpreters.Add(interpreter);
    }

    private void Clear()
    {
        _interpreters.Clear();
        _results.Clear();
    }
}