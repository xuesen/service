using System;
using System.Collections.Generic;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    public interface IImportService
    {
        dynamic ImportXmlFile(string xmlfile);
    }
}
