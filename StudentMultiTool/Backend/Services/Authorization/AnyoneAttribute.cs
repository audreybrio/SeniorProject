using System;

namespace StudentMultiTool.Backend.Services.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AnyoneAttribute: Attribute
    {

    }
}
