using BokPalace.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace BokPalace.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly string? AssemblyName = typeof(ApplicationDbContext).Assembly.FullName;
    public static readonly Assembly CallingAssembly = Assembly.GetCallingAssembly();
    public static readonly DependencyContext? DependencyContext = DependencyContext.Default;
}
