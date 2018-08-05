using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

using JetBrains.Platform.RdFramework;
using JetBrains.Platform.RdFramework.Base;
using JetBrains.Platform.RdFramework.Impl;
using JetBrains.Platform.RdFramework.Tasks;
using JetBrains.Platform.RdFramework.Util;
using JetBrains.Platform.RdFramework.Text;

using JetBrains.Util.Collections;
using JetBrains.Util.Logging;
using JetBrains.Util.PersistentMap;
using Lifetime = JetBrains.DataFlow.Lifetime;

// ReSharper disable RedundantEmptyObjectCreationArgumentList
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantOverflowCheckingContext


namespace JetBrains.Rider.Model
{
  
  
  public class SampleModel : RdExtBase {
    //fields
    //public fields
    [NotNull] public IRdProperty<string> MyString { get { return _MyString; }}
    [NotNull] public IRdProperty<bool> MyBool { get { return _MyBool; }}
    [NotNull] public IRdProperty<JetBrains.Rider.Model.MyEnum?> MyEnum { get { return _MyEnum; }}
    [NotNull] public IViewableMap<string, string> Data { get { return _Data; }}
    [NotNull] public IRdSignal<JetBrains.Rider.Model.MyStructure> MyStructure { get { return _MyStructure; }}
    
    //private fields
    [NotNull] private readonly RdProperty<string> _MyString;
    [NotNull] private readonly RdProperty<bool> _MyBool;
    [NotNull] private readonly RdProperty<JetBrains.Rider.Model.MyEnum?> _MyEnum;
    [NotNull] private readonly RdMap<string, string> _Data;
    [NotNull] private readonly RdSignal<JetBrains.Rider.Model.MyStructure> _MyStructure;
    
    //primary constructor
    private SampleModel(
      [NotNull] RdProperty<string> myString,
      [NotNull] RdProperty<bool> myBool,
      [NotNull] RdProperty<JetBrains.Rider.Model.MyEnum?> myEnum,
      [NotNull] RdMap<string, string> data,
      [NotNull] RdSignal<JetBrains.Rider.Model.MyStructure> myStructure
    )
    {
      if (myString == null) throw new ArgumentNullException("myString");
      if (myBool == null) throw new ArgumentNullException("myBool");
      if (myEnum == null) throw new ArgumentNullException("myEnum");
      if (data == null) throw new ArgumentNullException("data");
      if (myStructure == null) throw new ArgumentNullException("myStructure");
      
      _MyString = myString;
      _MyBool = myBool;
      _MyEnum = myEnum;
      _Data = data;
      _MyStructure = myStructure;
      _MyString.OptimizeNested = true;
      _MyBool.OptimizeNested = true;
      _MyEnum.OptimizeNested = true;
      _Data.OptimizeNested = true;
      _MyEnum.ValueCanBeNull = true;
      BindableChildren.Add(new KeyValuePair<string, object>("myString", _MyString));
      BindableChildren.Add(new KeyValuePair<string, object>("myBool", _MyBool));
      BindableChildren.Add(new KeyValuePair<string, object>("myEnum", _MyEnum));
      BindableChildren.Add(new KeyValuePair<string, object>("data", _Data));
      BindableChildren.Add(new KeyValuePair<string, object>("myStructure", _MyStructure));
    }
    //secondary constructor
    internal SampleModel (
    ) : this (
      new RdProperty<string>(JetBrains.Platform.RdFramework.Impl.Serializers.ReadString, JetBrains.Platform.RdFramework.Impl.Serializers.WriteString),
      new RdProperty<bool>(JetBrains.Platform.RdFramework.Impl.Serializers.ReadBool, JetBrains.Platform.RdFramework.Impl.Serializers.WriteBool),
      new RdProperty<JetBrains.Rider.Model.MyEnum?>(ReadMyEnumNullable, WriteMyEnumNullable),
      new RdMap<string, string>(JetBrains.Platform.RdFramework.Impl.Serializers.ReadString, JetBrains.Platform.RdFramework.Impl.Serializers.WriteString, JetBrains.Platform.RdFramework.Impl.Serializers.ReadString, JetBrains.Platform.RdFramework.Impl.Serializers.WriteString),
      new RdSignal<JetBrains.Rider.Model.MyStructure>(JetBrains.Rider.Model.MyStructure.Read, JetBrains.Rider.Model.MyStructure.Write)
    ) {}
    //statics
    
    public static CtxReadDelegate<JetBrains.Rider.Model.MyEnum?> ReadMyEnumNullable = new CtxReadDelegate<JetBrains.Rider.Model.MyEnum>(JetBrains.Platform.RdFramework.Impl.Serializers.ReadEnum<JetBrains.Rider.Model.MyEnum>).NullableStruct();
    
    public static CtxWriteDelegate<JetBrains.Rider.Model.MyEnum?> WriteMyEnumNullable = new CtxWriteDelegate<JetBrains.Rider.Model.MyEnum>(JetBrains.Platform.RdFramework.Impl.Serializers.WriteEnum<JetBrains.Rider.Model.MyEnum>).NullableStruct();
    
    protected override long SerializationHash => 2011091388562592936L;
    
    protected override Action<ISerializers> Register => RegisterDeclaredTypesSerializers;
    public static void RegisterDeclaredTypesSerializers(ISerializers serializers)
    {
      serializers.RegisterEnum<JetBrains.Rider.Model.MyEnum>();
      serializers.Register(JetBrains.Rider.Model.MyStructure.Read, JetBrains.Rider.Model.MyStructure.Write);
      
      serializers.RegisterToplevelOnce(typeof(IdeRoot), IdeRoot.RegisterDeclaredTypesSerializers);
    }
    
    //custom body
    //equals trait
    //hash code trait
    //pretty print
    public override void Print(PrettyPrinter printer)
    {
      printer.Println("SampleModel (");
      using (printer.IndentCookie()) {
        printer.Print("myString = "); _MyString.PrintEx(printer); printer.Println();
        printer.Print("myBool = "); _MyBool.PrintEx(printer); printer.Println();
        printer.Print("myEnum = "); _MyEnum.PrintEx(printer); printer.Println();
        printer.Print("data = "); _Data.PrintEx(printer); printer.Println();
        printer.Print("myStructure = "); _MyStructure.PrintEx(printer); printer.Println();
      }
      printer.Print(")");
    }
    //toString
    public override string ToString()
    {
      var printer = new SingleLinePrettyPrinter();
      Print(printer);
      return printer.ToString();
    }
  }
  public static class SolutionSampleModelEx
   {
    public static SampleModel GetSampleModel(this Solution solution)
    {
      return solution.GetOrCreateExtension("sampleModel", () => new SampleModel());
    }
  }
  
  
  public enum MyEnum {
    FirstValue,
    SecondValue
  }
  
  
  public class MyStructure : IPrintable, IEquatable<MyStructure> {
    //fields
    //public fields
    [NotNull] public string ProjectFile {get; private set;}
    [NotNull] public string Target {get; private set;}
    
    //private fields
    //primary constructor
    public MyStructure(
      [NotNull] string projectFile,
      [NotNull] string target
    )
    {
      if (projectFile == null) throw new ArgumentNullException("projectFile");
      if (target == null) throw new ArgumentNullException("target");
      
      ProjectFile = projectFile;
      Target = target;
    }
    //secondary constructor
    //statics
    
    public static CtxReadDelegate<MyStructure> Read = (ctx, reader) => 
    {
      var projectFile = reader.ReadString();
      var target = reader.ReadString();
      return new MyStructure(projectFile, target);
    };
    
    public static CtxWriteDelegate<MyStructure> Write = (ctx, writer, value) => 
    {
      writer.Write(value.ProjectFile);
      writer.Write(value.Target);
    };
    //custom body
    //equals trait
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((MyStructure) obj);
    }
    public bool Equals(MyStructure other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ProjectFile == other.ProjectFile && Target == other.Target;
    }
    //hash code trait
    public override int GetHashCode()
    {
      unchecked {
        var hash = 0;
        hash = hash * 31 + ProjectFile.GetHashCode();
        hash = hash * 31 + Target.GetHashCode();
        return hash;
      }
    }
    //pretty print
    public void Print(PrettyPrinter printer)
    {
      printer.Println("MyStructure (");
      using (printer.IndentCookie()) {
        printer.Print("projectFile = "); ProjectFile.PrintEx(printer); printer.Println();
        printer.Print("target = "); Target.PrintEx(printer); printer.Println();
      }
      printer.Print(")");
    }
    //toString
    public override string ToString()
    {
      var printer = new SingleLinePrettyPrinter();
      Print(printer);
      return printer.ToString();
    }
  }
}
