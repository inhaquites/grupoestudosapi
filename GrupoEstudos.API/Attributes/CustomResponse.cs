using Microsoft.AspNetCore.Mvc;
using GrupoEstudos.API.Controllers.Shared;

namespace GrupoEstudos.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class CustomResponseAttribute : ProducesResponseTypeAttribute
{
    public CustomResponseAttribute(int statusCode) : base(typeof(CustomResult), statusCode) { }
}
