﻿using System.Linq;
using Mono.Cecil;
using Xunit;

public class MethodFinderTest
{
    TypeDefinition typeDefinition;
    ModuleWeaver methodFinder;

    public MethodFinderTest()
    {
        var codeBase = typeof(MethodFinderTest).Assembly.Location;
        var module = ModuleDefinition.ReadModule(codeBase);
        methodFinder = new ModuleWeaver
        {
            ModuleDefinition = module
        };

        typeDefinition = module.Types.First(x => x.Name.EndsWith("MethodFinderTest"));
    }

    [Fact]
    public void WithStringParamTest()
    {
        var definitionToProcess = typeDefinition.NestedTypes.First(x => x.Name == "WithStringParam");
        var methodReference = methodFinder.RecursiveFindEventInvoker(definitionToProcess);
        Assert.NotNull(methodReference);
        Assert.Equal("OnPropertyChanging", methodReference.MethodReference.Name);
    }

    public class WithStringParam
    {
        public void OnPropertyChanging(string propertyName)
        {
        }
    }

    [Fact]
    public void WithStringAndBeforeParamTest()
    {
        var definitionToProcess = typeDefinition.NestedTypes.First(x => x.Name == "WithStringAndBefore");
        var methodReference = methodFinder.RecursiveFindEventInvoker(definitionToProcess);
        Assert.NotNull(methodReference);
        Assert.Equal("OnPropertyChanging", methodReference.MethodReference.Name);
        Assert.Equal(InvokerTypes.Before, methodReference.InvokerType);
    }

    public class WithStringAndBefore
    {
        public void OnPropertyChanging(string propertyName, object before)
        {
        }
    }


    [Fact]
    public void NoMethodTest()
    {
        var definitionToProcess = typeDefinition.NestedTypes.First(x => x.Name == "NoMethod");
        Assert.Null(methodFinder.RecursiveFindEventInvoker(definitionToProcess));
    }

    public class NoMethod
    {
    }
    [Fact]
    public void NoParamsTest()
    {
        var definitionToProcess = typeDefinition.NestedTypes.First(x => x.Name == "NoParams");
        Assert.Null(methodFinder.RecursiveFindEventInvoker(definitionToProcess));
    }

    public class NoParams
    {
        public void OnPropertyChanging()
        {
        }
    }

    [Fact]
    public void WrongParamsTest()
    {
        var definitionToProcess = typeDefinition.NestedTypes.First(x => x.Name == "WrongParams");
        Assert.Null(methodFinder.RecursiveFindEventInvoker(definitionToProcess));
    }

    public class WrongParams
    {
        public void OnPropertyChanging(int propertyName)
        {
        }
    }
}