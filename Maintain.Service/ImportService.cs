using IIMes.Services.Maintain.Interface;
// using IIMes.Services.Maintain.Model;

namespace IIMes.Services.Maintain.Services
{
    public partial class ImportService : IImportService
    {
        public ImportService()
                            // ISnoService snoService,
                            // IPublishEndpoint publishEndpoint
        {
            // _snoService = snoService;
            // this.publishEndpoint = publishEndpoint;
        }

        // xml数据导入
        public dynamic ImportXmlFile(string xmlfile)
        {
            var obj = DataMapper.Instance().XmlStringToObj(xmlfile);

            return obj;
        }
    }
}
