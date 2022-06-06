using Clean.Domain.Contracts.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Resources.Permissions.Commons
{
    public class AllPermissions : IPermission
    {
        public AllPermissions()
        {
            Permissions = new List<string>();
            string? @namespace = typeof(AllPermissions).Namespace;
            string permissionNameSpace = @namespace?.Remove(@namespace.LastIndexOf('.')) ?? string.Empty;
            var assemblies = typeof(AllPermissions).Assembly;

            var types = assemblies.GetTypes();

            foreach (var type in types)
            {
                if (type.Namespace != permissionNameSpace)
                    continue;

                var properties = type.GetProperties();

                foreach (var @property in properties)
                {
                    List<string>? propertyValues = property.GetValue(@property) as List<string>;

                    if (propertyValues == null)
                        continue;

                    foreach (var propertyValue in propertyValues)
                        Permissions.Add(propertyValue);
                }
            }
        }

        public IList<string> Permissions { get; set; }
    }
}
