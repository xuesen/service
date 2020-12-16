// using System.Collections.Generic;
// using System.Linq;
// using IIMes.Infrastructure.Data.Interface;
// using IIMes.Services.Runtime.Model.Process;
// using IIMes.Services.Runtime.Model.Product;
// using NHibernate.Linq;

// namespace IIMes.Services.Runtime.Repository
// {
//     public static class BopRepositoryExtension
//     {
//         public static Bop GetBopByWo(this BopRepository repository, string wo)
//         {
//             var bop = new Bop(wo)
//             {
//                 BopBom = new List<BopProcessBom>
//                 {
//                    new BopProcessBom{Id = 1, BopBaseId = 1, ProcessId = 1, ItemPartId = 10, Qty = 1, Location = "l1"},
//                    new BopProcessBom{Id = 2, BopBaseId = 1, ProcessId = 1, ItemPartId = 20, Qty = 2, Location = "l2"},
//                 },
//                 WoBom = new List<WoBom>()
//                 {
//                     new WoBom{Id = 1, WoBaseId = 0, ProcessId = 1, SubPart = new Part{Id = 10, PartNo = "P11"}, SubPartQty = 1, SubPartGroup = "1", Location = "L1"},
//                     new WoBom{Id = 2, WoBaseId = 0, ProcessId = 1, SubPart = new Part{Id = 11, PartNo = "P12"}, SubPartQty = 1, SubPartGroup = "1", Location = "L1"},
//                     new WoBom{Id = 3, WoBaseId = 0, ProcessId = 1, SubPart = new Part{Id = 20, PartNo = "P21"}, SubPartQty = 2, SubPartGroup = "2", Location = "L2"},
//                     new WoBom{Id = 4, WoBaseId = 0, ProcessId = 1, SubPart = new Part{Id = 21, PartNo = "P22"}, SubPartQty = 2, SubPartGroup = "2", Location = "L2"}
//                 },
//                 Fs = new WoFs()
//                 {
//                     PartFsId = "",
//                     WorkOrder = wo,
//                     PartId = 1,
//                     Side = "A",
//                     EquipmentId = 1,
//                     WoFsDetail = new List<WoFsDetail>(){
//                         new WoFsDetail{ WoFsId = 1, Station = "S1", ItemPartId = 10, ItemCount = 1, Location = "L1", FeederType = "F1", FeederSpec = "Fs1", Sub = new List<WoFsSub>{new WoFsSub{SubPartId = 10}, new WoFsSub{SubPartId = 11}}},
//                         new WoFsDetail{ WoFsId = 2, Station = "S2", ItemPartId = 20, ItemCount = 2, Location = "L2", FeederType = "F2", FeederSpec = "Fs2", Sub = new List<WoFsSub>{new WoFsSub{SubPartId = 20}, new WoFsSub{SubPartId = 21}}}
//                     }
//                 }
//             };
//             return bop;
//         }
//     }
// }
