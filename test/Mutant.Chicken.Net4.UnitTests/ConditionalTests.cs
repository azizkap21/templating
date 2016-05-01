﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutant.Chicken.Core;
using Mutant.Chicken.Core.Expressions.Cpp;

namespace Mutant.Chicken.Net4.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConditionalTests : TestBase
    {
        [TestMethod]
        public void VerifyIfEndifTrueCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection {["VALUE"] = true};
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifTrueCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #else
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection { ["VALUE"] = true };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifTrueConditionContainsTabs()
        {
            string value = @"Hello
    #if " + "\t" + @" (VALUE)
value
    #else
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection { ["VALUE"] = true };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifTrueConditionQuotedString()
        {
            string value = @"Hello
    #if (""Hello" + "\t" + @"There"" == VALUE)
value
    #else
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection { ["VALUE"] = "Hello\tThere" };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifTrueConditionLiteralFirst()
        {
            string value = @"Hello
    #if (3 > VALUE)
value
    #else
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifTrueConditionLiteralAgainst()
        {
            string value = @"Hello
    #if(3 > VALUE)
value
    #else
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifTrueConditionAgainstIf()
        {
            string value = @"Hello
    #if(VALUE)
value
    #else
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection { ["VALUE"] = true };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifFalseCondition()
        {
            string value = @"Hello
    #if VALUE
value
    #else
other
    #endif
There";
            string expected = @"Hello
other
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection { ["VALUE"] = false };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifEndifTrueFalseCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifEndifTrueTrueCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifEndifFalseTrueCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #endif
There";
            string expected = @"Hello
other
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseEndifTrueFalseCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #else
other2
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseEndifFalseTrueCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #else
other2
    #endif
There";
            string expected = @"Hello
other
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseEndifFalseFalseCondition()
        {
            string value = @"Hello
    #if VALUE
value
    #elseif VALUE2
other
    #else
other2
    #endif
There";
            string expected = @"Hello
other2
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyNestedIfTrueTrue()
        {
            string value = @"Hello
    #if (VALUE)
        #if (VALUE2)
value
        #else
other
        #endif
    #else
other2
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifElseEndifTrueTrueCondition()
        {
            string value = @"Hello
        #if (VALUE)
value
        #elseif (VALUE2)
other
        #else
other2
        #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifElseEndifTrueFalseCondition()
        {
            string value = @"Hello
        #if (VALUE)
value
        #elseif (VALUE2)
other
        #else
other2
        #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifElseEndifFalseTrueCondition()
        {
            string value = @"Hello
        #if (VALUE)
value
        #elseif (VALUE2)
other
        #else
other2
        #endif
There";
            string expected = @"Hello
other
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifElseEndifFalseFalseCondition()
        {
            string value = @"Hello
        #if VALUE
value
        #elseif VALUE2
other
        #else
other2
        #endif
There";
            string expected = @"Hello
other2
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifEndifTrueFalseFalseCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #elseif (VALUE3)
other2
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true,
                ["VALUE2"] = false,
                ["VALUE3"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifEndifFalseTrueFalseCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #elseif (VALUE3)
other2
    #endif
There";
            string expected = @"Hello
other
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = true,
                ["VALUE3"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseifElseifEndifFalseFalseTrueCondition()
        {
            string value = @"Hello
    #if (VALUE)
value
    #elseif (VALUE2)
other
    #elseif (VALUE3)
other2
    #endif
There";
            string expected = @"Hello
other2
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false,
                ["VALUE2"] = false,
                ["VALUE3"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueEqualsCondition()
        {
            string value = @"Hello
    #if (VALUE == 2)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2L
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueNotEqualsCondition()
        {
            string value = @"Hello
    #if (VALUE != 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 4
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueGreaterThanCondition()
        {
            string value = @"Hello
    #if (VALUE > 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 4
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifOperandStealing()
        {
            string value = @"Hello
    #if ((VALUE == 3) == true)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 3L
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifOperandStealing2()
        {
            string value = @"Hello
    #if (!VALUE == true)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueGreaterThanOrEqualToCondition()
        {
            string value = @"Hello
    #if (VALUE >= 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 3
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifFalseGreaterThanOrEqualToCondition()
        {
            string value = @"Hello
    #if (VALUE >= 3)
value
    #endif
There";
            string expected = @"Hello
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueLessThanCondition()
        {
            string value = @"Hello
    #if (VALUE < 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueLessThanOrEqualToCondition()
        {
            string value = @"Hello
    #if (VALUE <= 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 3
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueNotCondition()
        {
            string value = @"Hello
    #if (!VALUE)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = false
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueNotNotCondition()
        {
            string value = @"Hello
    #if (!!VALUE)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = true
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueAndCondition()
        {
            string value = @"Hello
    #if (VALUE < 3 && VALUE > 0)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueXorCondition()
        {
            string value = @"Hello
    #if (VALUE < 3 ^ VALUE == 7)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueAndAndCondition()
        {
            string value = @"Hello
    #if (VALUE < 3 && VALUE < 4 && VALUE < 5)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueOrCondition()
        {
            string value = @"Hello
    #if (VALUE == 6 || VALUE < 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueOrOrCondition()
        {
            string value = @"Hello
    #if (VALUE == 6 || VALUE == 7 || VALUE < 3)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueOrAndCondition()
        {
            string value = @"Hello
    #if (VALUE == 6 || (VALUE != 7 && VALUE < 3))
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueAndOrCondition()
        {
            string value = @"Hello
    #if ((VALUE != 7 && VALUE < 3) || VALUE == 6)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueBitwiseAndEqualsCondition()
        {
            string value = @"Hello
    #if (VALUE & 0xFFFF == 2)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueBitwiseOrEqualsCondition()
        {
            string value = @"Hello
    #if (VALUE | 0xFFFD == 0xFFFF)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueShlCondition()
        {
            string value = @"Hello
    #if (VALUE << 1 == 8)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 4
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueShrCondition()
        {
            string value = @"Hello
    #if (VALUE >> 1 == 2)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 4
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfEndifTrueGroupedCondition()
        {
            string value = @"Hello
    #if ((VALUE == 2) && (VALUE2 == 3))
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection
            {
                ["VALUE"] = 2L,
                ["VALUE2"] = 3L
            };
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifConditionUsesNull()
        {
            string value = @"Hello
    #if (VALUE2 == null)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifConditionUsesFalse()
        {
            string value = @"Hello
    #if (!false)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifConditionUsesDouble()
        {
            string value = @"Hello
    #if (1.2 < 2.5)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfElseEndifConditionUsesFalsePositiveHex()
        {
            string value = @"Hello
    #if (0xChicken == null)
value
    #endif
There";
            string expected = @"Hello
value
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyIfNoCondition()
        {
            string value = @"Hello
    #if
value
    #endif
There";
            string expected = @"Hello
There";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", false, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyConditionAtEnd()
        {
            string value = @"Hello
    #if (1.2 < 2.5)";
            string expected = @"Hello
";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyExcludeNestedCondition()
        {
            string value = @"Hello
    #if false
        #if true
            #if true
            #endif
        #endif
        #if true
            #if true
            #endif
        #endif
    #endif";
            string expected = @"Hello
";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyExcludeNestedConditionInNonTakenBranch()
        {
            string value = @"Hello
    #if true
    #else
        #if true
            #if true
            #endif
        #endif
        #if true
            #if true
            #endif
        #endif
    #endif";
            string expected = @"Hello
";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            bool changed = processor.Run(input, output, 28);
            Verify(Encoding.UTF8, output, changed, value, expected);
        }

        [TestMethod]
        public void VerifyEmitStrayToken()
        {
            string value = @"Hello
    #endif
    #else
    #elseif foo";
            string expected = @"Hello
    #endif
    #else
    #elseif foo";

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            MemoryStream input = new MemoryStream(valueBytes);
            MemoryStream output = new MemoryStream();

            IOperationProvider[] operations = { new Conditional("#if", "#else", "#elseif", "#endif", true, true, CppStyleEvaluatorDefinition.CppStyleEvaluator) };
            VariableCollection vc = new VariableCollection();
            EngineConfig cfg = new EngineConfig(vc);
            IProcessor processor = Processor.Create(cfg, operations);

            //Changes should be made
            processor.Run(input, output, 28);
            //Override the change indication - the stream was technically mutated in this case,
            //  pretend it's false because the inputs and outputs are the same
            Verify(Encoding.UTF8, output, false, value, expected);
        }
    }
}
