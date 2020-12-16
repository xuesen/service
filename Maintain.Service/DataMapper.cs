using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using IIMes.Services.Maintain.Interface;
using Newtonsoft.Json;

namespace IIMes.Services.Maintain.Services
{
    public class DataMapper
    {
        // private static readonly Logger logger = LogManager.GetLogger("DataMapper");
        // string moduleName = "partfs";

        // <summary>
        // Get instance of DataMapper
        // </summary>
        // <returns></returns>
        public static DataMapper Instance()
        {
            return theSingleton;
        }

        private static DataMapper theSingleton = new DataMapper();

        private DataMapper()
        {
        }

/*
        public string ClientIp { get; set; }
        public string Site { get; set; }
        public string PrimaryKey { get; set; }
        public string MainSitedFields { get; set; }
        public string SubSitedFields { get; set; }
        public JArray CategoryField { get; set; }
        public JArray Categories { get; set; }
        public List<string> LstMainFields { get; set; }
        public Dictionary<string, Dictionary<string, string>> DicMainList { get; set; }
        public Dictionary<string, string> DicList { get; set; } */

        // xml转json,json转object
        public IList<PartFsDetailDTO> XmlStringToObj(string xml)
        {
            // xml转json
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonstr = JsonConvert.SerializeXmlNode(doc);
            jsonstr = RemoveAt(jsonstr);
            // 转成对象
            IList<PartFsDetailDTO> ret = new List<PartFsDetailDTO>();
            ret = JsonToObject(jsonstr);

            return ret;
        }

        // 去掉xml转成json后key里的@符号
        private static string RemoveAt(string json)
        {
        Regex reg = new Regex("\"@([^ \"]*)\"\\s*:\\s*\"(([^ \"]+\\s*)*)\"");
        string strPatten = "\"$1\":\"$2\"";
        return reg.Replace(json, strPatten);
        }

        private IList<PartFsDetailDTO> JsonToObject(string jsonstr)
        {
            // var DynamicObject = JsonConvert.DeserializeObject<dynamic>(jsonstr);
            ProgramDTO programDTO = JsonConvert.DeserializeObject<ProgramDTO>(jsonstr);
            IList<PlaceItem> placeitems = programDTO.Program.Core.Place.Place;
            IList<FeederItem> feederitems = programDTO.Program.Machine.Feeder.Feeder;

            IList<PartFsDetailDTO> partfslst = new List<PartFsDetailDTO>();
            for (var i = 0; i < feederitems.Count; i++)
            {
                PartFsDetailDTO partFsDetail = new PartFsDetailDTO
                {
                    Location = new List<string>()
                };
                bool flag = false;
                for (var j = 0; j < placeitems.Count; j++)
                {
                    if (placeitems[j].CompoName == feederitems[i].CompoName)
                    {
                        flag = true;
                        partFsDetail.Station = feederitems[i].HoleNo.No;
                        partFsDetail.ItemPartNo = feederitems[i].CompoName;
                        partFsDetail.FeedingType = feederitems[i].FeederType;
                        partFsDetail.Location.Add(placeitems[j].PlaceId);
                    }
                }

                if (flag == true)
                {
                    partfslst.Add(partFsDetail);
                }
            }

/*
        /// <summary>
        /// load configuration of module
        /// </summary>
        private void LoadConfig()
        {
            ClientIp = ConfigurationManager.AppSettings.Get("BindingIP");
            string path = Assembly.GetExecutingAssembly().Location;
            int idx = path.LastIndexOf("\\bin");
            path = path.Substring(0, idx);
            string cfgFile = path + "\\config\\" + moduleName + ".json";
            using (StreamReader r = new StreamReader(cfgFile))
            {
                string json = r.ReadToEnd();
                JObject items = (JObject)JsonConvert.DeserializeObject(json);

                Site = (string)items.GetValue("site");
                CategoryField = (JArray)items.GetValue("categoryField");
                Categories = (JArray)items.GetValue("category");
                //string[] arrCategory = categories.Split(',');

                DicMainList = new Dictionary<string, Dictionary<string, string>>();
                JObject main = (JObject)items.GetValue("main");
                if (main.ContainsKey("tableFields"))
                {
                    string tableFields = main.GetValue("tableFields").ToString();
                    LstMainFields = new List<string>();
                    string[] arrtableFields = tableFields.Split(',');
                    foreach (string str in arrtableFields)
                    {
                        LstMainFields.Add(str);
                    }
                    foreach (var tone in Categories)
                    {
                        string cate = (string)tone;
                        if (main.ContainsKey(cate))
                        {
                            JObject mainCate = (JObject)main.GetValue(cate);
                            Dictionary<string, string> tmpDic = new Dictionary<string, string>();
                            foreach (string str in arrtableFields)
                            {
                                if (mainCate.ContainsKey(str))
                                {
                                    tmpDic.Add(str, (string)mainCate.GetValue(str));
                                }
                            }
                            DicMainList.Add(cate, tmpDic);
                        }
                    }
                }
                if (main.ContainsKey("primaryKey"))
                {
                    PrimaryKey = (string)main.GetValue("primaryKey");
                }

            }
        }

           //读取partfs.json配置文件
            LoadConfig();

            string cateName = GetCategoryName(Categories);
            Dictionary<string, string> dicMain = null;
            if (!string.IsNullOrEmpty(cateName) && DicMainList.ContainsKey(cateName))
            {
                dicMain = DicMainList[cateName];
            }
            DicList = dicMain; */
            return partfslst;
        }

/*
        private IList<PartFsDetailDTO> ConvertObj(dynamic obj)
        {
            var ret = new List<PartFsDetailDTO>();
            PartFsDetailDTO detaildto = new PartFsDetailDTO();
            //var itemkey = "";
            foreach (string key in DicList.Keys)
            {
                if (key == "STATION")
                {
                    if (DicList[key].Contains("||"))
                    {
                        string [] arry = DicList[key].Split("||");
                        for (var i = 0; i < arry.Length; i++)
                        {
                            var str = obj[arry[i]];
                        }
                        //ret = key;
                        break;
                    }
                }

            }

            return ret;
        }

        private string GetMainKey(string val, Dictionary<string, string> dicMain)
        {
            string ret = "";
            foreach (string key in dicMain.Keys)
            {
                if (dicMain[key].Equals(val))
                {
                    ret = key;
                    break;
                }
            }
            return ret;
        }

        private string GetCategoryName(JArray categories)
        {
            string categoryName = "";
            foreach (string one in categories)
            {
                if (one.IndexOf(Site) != -1)
                {
                    categoryName = one;
                    break;
                }
            }
            return categoryName;
        }

         private IList<Parts> Convert(Dictionary<string, object> item)
        {
            var ret = new List<Parts>();
            var temp = (Dictionary<string, object>)item["partdetail"];

            var ptObj = new Parts()
            {
                //PartId = partId,//Int32.Parse(item["code"].ToString()),
                Pn = item["partnumber"].ToString(),
                PnCust = item["customerpn"].ToString(),
                CustName = item["customer"].ToString(),
                PType = item["parttype"].ToString(),
                Description = item["customerdesc"].ToString(),
                Descr = "",//item["desc"].ToString(),
                Category = item["category"].ToString(),
                Commodity = item["commodity"].ToString(),
                Editor = item["editor"].ToString(),
                Cdt = new DateTime(),
                Udt = new DateTime()//(long)item["udt"],
            };
            ptObj.PartsDescrs = new List<PartsDescr>();
            foreach (var pv in temp)
            {
                PartsDescr pd = new PartsDescr()
                {
                    //PartId = partId,
                    Attribute = pv.Key,
                    Value = pv.Value.ToString()
                };
                ptObj.PartsDescrs.Add(pd);
            }
            ret.Add(ptObj);
            return ret;
        }

 */
    }
}
