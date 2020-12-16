using System.Collections.Generic;
using System.Linq;
// using IIMes.Services.Maintain.Model;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Hibernate.Data;
using IIMes.Infrastructure.Hibernate.MSSQL;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class PartFsService : IPartFsService
    {
        private readonly IImportService _importService;
        private readonly IRepository<SPart> _repPart;
        private readonly IRepository<WoBase> _repWoBase;
        private readonly IRepository<SPartFs> _repPartFs;
        private readonly IRepository<SPartFsDetail> _repPartFsDetail;
        private readonly IRepository<SPartFsSub> _repPartFsSub;
        private readonly IRepository<SBomItem> _repBomItem;
        private readonly IRepository<SWoStatus> _repWoStatus;
        private readonly ICommonService _commonService;

        public PartFsService(
                            IImportService importService,
                            IRepository<SPart> repPart,
                            IRepository<WoBase> repWoBase,
                            IRepository<SPartFs> repPartFs,
                            IRepository<SPartFsDetail> repPartFsDetail,
                            IRepository<SPartFsSub> repPartFsSub,
                            IRepository<SBomItem> repBomItem,
                            IRepository<SWoStatus> repWoStatus,
                            ICommonService commonService)
                            // ISnoService snoService,
                            // IPublishEndpoint publishEndpoint
        {
            _repPart = repPart;
            _repWoBase = repWoBase;
            _repPartFs = repPartFs;
            _repPartFsDetail = repPartFsDetail;
            _repPartFsSub = repPartFsSub;
            _repBomItem = repBomItem;
            _importService = importService;
            _repWoStatus = repWoStatus;
            _commonService = commonService;
            // _snoService = snoService;
            // this.publishEndpoint = publishEndpoint;
        }

        public object CheckPartWo(int partid)
        {
            var wipstatusid = _repWoStatus.GetWoStaus("WIP").Id;
            var wobaselst = _repWoBase.CheckPartWo(partid, wipstatusid);

            return wobaselst;
        }

        public IList<PartFsDetailInfoDTO> GetPartFsDetail(PartFsDTO partfsdto)
        {
            IList<PartFsDetailInfoDTO> partfsdetailinfolst = new List<PartFsDetailInfoDTO>();

            if (partfsdto != null)
            {
                // PartFsDTO partfsdto = new PartFsDTO();
                // partfsdto = request.ToObject<PartFsDTO>();
                var partfs = _repPartFs.GetPartFs(partfsdto);
                if (partfs == null)
                {
                    // 机型料站无数据
                    throw new BizException("CHK009");
                }

                var partfsno = partfs.Id;
                var editor = partfs.EmployeeCode;
                var cdt = partfs.CreateTime;
                var partfsdetaillst = _repPartFsDetail.GetPartFsDetail(partfsno);

                foreach (var item in partfsdetaillst)
                {
                    var part = _repPart.Get(item.ItemPartId);
                    // 替代料
                    // var wofssubs =  _repWoFsSub.GetWoFsSub(item.WoFsId).ToList();
                    PartFsDetailInfoDTO partfsdetaildto = new PartFsDetailInfoDTO
                    {
                        PartFsDetail = new SPartFsDetail(),
                        Editor = new LayerEditorDTO()
                    };
                    partfsdetaildto.PartFsDetail = item;
                    partfsdetaildto.ItemPartNo = part.PartNo;
                    partfsdetaildto.Editor.Name = editor;
                    partfsdetaildto.Update_time = cdt;

                    partfsdetailinfolst.Add(partfsdetaildto);
                }
            }

            return partfsdetailinfolst;
        }

        public bool CheckPartFs(PartFsDTO partfsdto)
        {
            var ret = false;
            if (partfsdto != null)
            {
                var partfs = _repPartFs.GetPartFs(partfsdto);
                if (partfs != null)
                {
                    // 机型料站有数据
                    ret = true;
                }
            }

            return ret;
        }

        [Transactional]
        public int ImportPartFs(PartFsDTO partfsdto)
        {
            var ret = 0;
            if (partfsdto != null)
            {
                // PartFsDTO partfsdto = new PartFsDTO();
                // partfsdto = request.ToObject<PartFsDTO>();
                // 转换xml文件为业务对象
                var partfsdetail = _importService.ImportXmlFile(partfsdto.XmlFile);
                // 检查导入文件的物料是否在系统的物料BOM中
                CheckBOM(partfsdetail, partfsdto.PartId);
                // 如果要导入的物料下，partfs中有数据，要先删除partfs，partfsdetail，partfssub
                DeleteOldPartFsData(partfsdto);
                // 保存数据：机型料站对应数据库表：S_PART_FS
                var partfsid = SavePartFs(partfsdto);
                // 保存数据：S_PART_FS_DETAIL
                SavePartFsDetail(partfsid, partfsdetail);
            }

            return ret;
        }

        // 如果要导入的物料下，partfs中有数据，要先删除partfs，partfsdetail，partfssub
        [Transactional]
        private void DeleteOldPartFsData(PartFsDTO partfsdto)
        {
            var partfs = _repPartFs.GetPartFs(partfsdto);
            if (partfs != null)
            {
                var partfsdetaillst = _repPartFsDetail.GetPartFsDetail(partfs.Id).ToList();
                if (partfsdetaillst.Count > 0)
                {
                    for (var i = 0; i < partfsdetaillst.Count; i++)
                    {
                        var partfssublst = _repPartFsSub.Query().Where(p => p.PartFsDetailId == partfsdetaillst[i].Id).ToList();
                        if (partfssublst.Count > 0)
                        {
                            for (var j = 0; j < partfssublst.Count; j++)
                            {
                                _repPartFsSub.Delete(partfssublst[j]);
                            }
                        }

                        _repPartFsDetail.Delete(partfsdetaillst[i]);
                    }
                }

                _repPartFs.Delete(partfs);
            }
        }

        // 与物料BOM进行比对，检查料站里的物料是否都在物料BOM中，保证料站表与BOM数据一致
        private void CheckBOM(IList<PartFsDetailDTO> data, int partid)
        {
            if (data != null)
            {
                var bomitemlst = _commonService.GetBomItemsbyPartid(partid);
                IList<PartFsDetailDTO> partfsdetaillst = data;
                foreach (var item in partfsdetaillst)
                {
                    var flag = false;
                    foreach (var bomitem in bomitemlst)
                    {
                        SPartFsDetail partFsDetail = new SPartFsDetail();
                        var itempart = _commonService.GetPart(item.ItemPartNo);
                        if (itempart == null)
                        {
                            // 导入物料不存在
                            throw new BizException("CHK011", item.ItemPartNo);
                        }

                        if (itempart != null && itempart.Id == bomitem.ItemPartId)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag == false)
                    {
                        // 导入物料不在物料BOM中
                        throw new BizException("CHK010", item.ItemPartNo);
                    }
                }
            }
        }

        [Transactional]
        private int SavePartFs(PartFsDTO partfsdto)
        {
            SPartFs partfs = new SPartFs
            {
                PartId = partfsdto.PartId,
                Side = partfsdto.Side,
                EquipmentId = partfsdto.EquipmentId,
                EmployeeCode = partfsdto.EmployeeCode,
                CreateTime = ((Repository<SPartFs>)_repPartFs).GetDate<SPartFs>()
            };

            _repPartFs.Add(partfs);
            var ret = _repPartFs.GetPartFs(partfsdto).Id;
            return ret;
        }

        [Transactional]
        private void SavePartFsDetail(int partfsid, dynamic data)
        {
            if (data != null)
            {
                IList<PartFsDetailDTO> partfsdetaillst = data;
                foreach (var item in partfsdetaillst)
                {
                    SPartFsDetail partFsDetail = new SPartFsDetail
                    {
                        PartFsId = partfsid,
                        Station = item.Station,
                        ItemPartId = _commonService.GetPart(item.ItemPartNo).Id
                    };
                    var locations = "";
                    for (var i = 0; i < item.Location.Count; i++)
                    {
                        locations += item.Location[i] + ",";
                    }

                    partFsDetail.Location = locations.Substring(0, locations.Length - 1);
                    partFsDetail.ItemCount = item.Location.Count;
                    partFsDetail.FeederType = item.FeedingType;
                    partFsDetail.FeederSpec = "";
                    _repPartFsDetail.Add(partFsDetail);
                }
            }
        }
    }
}
