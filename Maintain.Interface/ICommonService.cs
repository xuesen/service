using System.Collections.Generic;
using System.Linq;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface ICommonService
    {
        [Operation]
        STerminal GetTerminal(string terminalcode);

        [Operation]
        SPart GetPart(string partno);

        [Operation]
        SEquipment GetEquipment(string equipmentcode);

        [Operation]
        IQueryable<SSetting> GetSetting(string program);

        [Operation]
        WoBase GetWo(string workOrder);

        [Operation]
        IList<WoFsDetail> GetWoFsDetail(int wofsid);

        [Operation]
        void DeleteWoFsDetail(int id);

        [Operation]
        IList<WoFsSub> GetWoFsSub(int wofsdetailid);

        [Operation]
        SProcess GetProcess(string processcode);

        [Operation]
        SWoStatus GetWoStaus(string name);

        [Operation]
        IList<CommonDTO> GetEquipmentbyLine(int lineid);

        [Operation]
        IList<PartMarketDTO> GetMartketPart();

        [Operation]
        IList<PartMarketDTO> GetCatchPart();

        [Operation]
        IList<BomItemDataDTO> GetBomItemsbyPartid(int partid);

        [Operation]
        IList<CommonWoFsDetailDTO> GetWoFsDetailInfo(int wofsid);

        [Operation]
        IList<EqualWoFsDetailDTO> ChangeEqualWoFsDetail(IList<CommonWoFsDetailDTO> commonwofsdetaildto);

        [Operation]
        bool IsEqualWoFsDetail(IList<EqualWoFsDetailDTO> listA, IList<EqualWoFsDetailDTO> listB);
    }
}