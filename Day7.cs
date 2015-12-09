using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Typedef for brevity
using GateFunction = System.Func<ushort[], ushort>;

namespace Day7
{
    static class Day7
    {
        private static ushort And(params ushort[] operands)
        {
            if (operands.Length != 2)
                throw new Exception("Expected 2 parameters");

            return (ushort)(operands[0] & operands[1]);
        }
        private static ushort Or(params ushort[] operands)
        {
            if (operands.Length != 2)
                throw new Exception("Expected 2 parameters");

            return (ushort)(operands[0] | operands[1]);
        }
        private static ushort LeftShift(params ushort[] operands)
        {
            if (operands.Length != 2)
                throw new Exception("Expected 2 parameters");

            return (ushort)(operands[0] << operands[1]);
        }
        private static ushort RightShift(params ushort[] operands)
        {
            if (operands.Length != 2)
                throw new Exception("Expected 2 parameters");

            return (ushort)(operands[0] >> operands[1]);
        }
        private static ushort Not(params ushort[] operands)
        {
            if (operands.Length != 1)
                throw new Exception("Expected 1 parameter");

            return (ushort)~operands[0];
        }
        private static ushort Number(params ushort[] operands)
        {
            if (operands.Length != 1)
                throw new Exception("Expected 1 parameter");

            return (ushort)operands[0];
        }

        public static ushort SolvePart1(ref string[] input, string targetWire)
        {
            var wires = new Dictionary<string, GateFunction>();
            var parameters = new Dictionary<string, string[]>();
            var computed = new Dictionary<string, ushort>();

            var delimiters = new[] { ' ', '-', '>' };

            foreach (var line in input)
            {
                var parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                var key = parts.Last();

                if (parts.Length == 2)
                {
                    //Assignment
                    wires.Add(key, new GateFunction(Number));
                    parameters.Add(key, new[] { parts[0] });
                }
                else if (parts.Length == 3)
                {
                    //Unary operator
                    wires.Add(key, new GateFunction(Not));
                    parameters.Add(key, new[] { parts[1] });
                }
                else if (parts.Length == 4)
                {
                    //Binary operator
                    Func<ushort[], ushort> func = null;

                    switch (parts[1])
                    {
                        case "AND":
                            func = new GateFunction(And);
                            break;
                        case "OR":
                            func = new GateFunction(Or);
                            break;
                        case "LSHIFT":
                            func = new GateFunction(LeftShift);
                            break;
                        case "RSHIFT":
                            func = new GateFunction(RightShift);
                            break;
                        default:
                            throw new Exception("Unexpected operator: " + parts[1]);
                    }

                    wires.Add(key, func);
                    parameters.Add(key, new[] { parts[0], parts[2] });
                }
                else
                {
                    throw new Exception("Unexpected input: " + line);
                }
            }

            bool finished = false;

            while (!finished)
            {
                foreach (var wire in wires)
                {
                    var operands = parameters[wire.Key];

                    var array = new ushort[operands.Length];
                    bool error = false;

                    for (int i = 0; i < operands.Length; i++)
                    {
                        if (computed.ContainsKey(operands[i]))
                            array[i] = computed[operands[i]];
                        else if (!ushort.TryParse(operands[i], out array[i]))
                        {
                            error = true;
                            break;
                        }
                    }

                    if (error) continue;

                    var value = wire.Value.Invoke(array);

                    // Uncomment if you want to see the progress
                    //Console.WriteLine("Computed: {0} ({1}({2}) -> {3}", wire.Key, wire.Value.Method.Name, string.Join(",", array), value);

                    if (!computed.ContainsKey(wire.Key))
                        computed.Add(wire.Key, value);
                    else
                        computed[wire.Key] = value;
                }

                if (computed.ContainsKey(targetWire))
                    finished = true;
            }

            return computed[targetWire];
        }
        public static ushort SolvePart2(ref string[] input, string targetWire, string overridedWire, ushort firstResult)
        {
            var wires = new Dictionary<string, GateFunction>();
            var parameters = new Dictionary<string, string[]>();
            var computed = new Dictionary<string, ushort>();

            var delimiters = new[] { ' ', '-', '>' };

            foreach (var line in input)
            {
                var parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                var key = parts.Last();

                // Overriding
                if (key == overridedWire)
                    parts[0] = firstResult.ToString();

                if (parts.Length == 2)
                {
                    //Assignment
                    wires.Add(key, new GateFunction(Number));
                    parameters.Add(key, new[] { parts[0] });
                }
                else if (parts.Length == 3)
                {
                    //Unary operator
                    wires.Add(key, new GateFunction(Not));
                    parameters.Add(key, new[] { parts[1] });
                }
                else if (parts.Length == 4)
                {
                    //Binary operator
                    Func<ushort[], ushort> func = null;

                    switch (parts[1])
                    {
                        case "AND":
                            func = new GateFunction(And);
                            break;
                        case "OR":
                            func = new GateFunction(Or);
                            break;
                        case "LSHIFT":
                            func = new GateFunction(LeftShift);
                            break;
                        case "RSHIFT":
                            func = new GateFunction(RightShift);
                            break;
                        default:
                            throw new Exception("Unexpected operator: " + parts[1]);
                    }

                    wires.Add(key, func);
                    parameters.Add(key, new[] { parts[0], parts[2] });
                }
                else
                {
                    throw new Exception("Unexpected input: " + line);
                }
            }

            bool finished = false;

            while (!finished)
            {
                foreach (var wire in wires)
                {
                    var operands = parameters[wire.Key];

                    var array = new ushort[operands.Length];
                    bool error = false;

                    for (int i = 0; i < operands.Length; i++)
                    {
                        if (computed.ContainsKey(operands[i]))
                            array[i] = computed[operands[i]];
                        else if (!ushort.TryParse(operands[i], out array[i]))
                        {
                            error = true;
                            break;
                        }
                    }

                    if (error) continue;

                    var value = wire.Value.Invoke(array);

                    // Uncomment if you want to see the progress
                    //Console.WriteLine("Computed: {0} ({1}({2}) -> {3}", wire.Key, wire.Value.Method.Name, string.Join(",", array), value);

                    if (!computed.ContainsKey(wire.Key))
                        computed.Add(wire.Key, value);
                    else
                        computed[wire.Key] = value;
                }

                if (computed.ContainsKey(targetWire))
                    finished = true;
            }

            return computed[targetWire];
        }
    }
}
