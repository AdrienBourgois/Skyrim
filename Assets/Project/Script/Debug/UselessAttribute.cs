using System;

/// <summary>
/// Simple Attribute to mark useless elements that can be removed safely because they have any call/invocation/use/...
/// </summary>

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class UselessAttribute : Attribute {

}
