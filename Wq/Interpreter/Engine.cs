namespace Wq.Interpreter;

using Wq.WqValue;

public class Engine
{
    private const int InterpreterSteps = 100;

    private readonly List<Interpreter> _interpreters = [];
    private readonly List<WqValue> _results = [];

    public List<WqValue> Start(WqFuncDeclData[] funcDeclData)
    {
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
                _results.Add(_interpreters[index].LastValue);
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
        _interpreters.Add(interpreter);
    }

    private void Clear()
    {
        _interpreters.Clear();
        _results.Clear();
    }
}