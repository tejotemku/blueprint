using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlueprintBackend.Models
{
    public class Project
    {
        string id;
        string projectName;
        DateTime lastModified;
        string projectFile;
    }
}
