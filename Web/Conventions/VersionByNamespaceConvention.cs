using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Web.Conventions;
/// <summary>
///  Detects an API version accordingly to namespace
/// </summary>
public class VersionByNamespaceConvention  : IControllerConvention
{
    public bool Apply(IControllerConventionBuilder controller, ControllerModel controllerModel)
    {
        var controllerNamespace = controller.ControllerType.Namespace;
        if (controllerNamespace == null) return false;

        var match = Regex.Match(controllerNamespace, @"\.V(?<version>\d+)(?:_(?<sub>\d+))?$");
        if (!match.Success) return false;

        var major = int.Parse(match.Groups["version"].Value);
        var minor = match.Groups["sub"].Success ? int.Parse(match.Groups["sub"].Value) : 0;

        controller.HasApiVersion(new ApiVersion(major, minor));
        return true;
    }
}