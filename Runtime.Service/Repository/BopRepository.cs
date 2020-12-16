using System.Linq;
using NHibernate.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Infrastructure.Hibernate.Data;
using System.Collections.Generic;
using IIMes.Infrastructure.Exception;

namespace IIMes.Services.Runtime.Repository
{
    public class BopRepository : Repository<Bop>
    {
        private readonly IRepository<WoBase> _repWoBase;

        private readonly IRepository<BopBase> _repBopBase;

        private readonly IRepository<BopProcessBom> _repBopProcessBom;

        private readonly IRepository<WoBom> _repWoBom;

        private readonly IRepository<WoFs> _repWoFs;

        private readonly IRepository<Workflow> _repWorkflow;

        public BopRepository(
                                IRepository<BopBase> repBopBase,
                                IRepository<WoBase> repWoBase,
                                IRepository<BopProcessBom> repBopProcessBom,
                                IRepository<WoBom> repWoBom,
                                IRepository<WoFs> repWoFs,
                                IRepository<Workflow> repWorkflow,
                                IDBContext context) : base(context)
        {
            _repBopBase = repBopBase; 
            _repWoBase = repWoBase;            
            _repBopProcessBom = repBopProcessBom;
            _repWoBom = repWoBom;
            _repWoFs = repWoFs;
            _repWorkflow = repWorkflow;
        }
        public Bop GetBop(string wo)
        {
            var wobase = _repWoBase.GetWoBaseByWo(wo);
            if (wobase == null)
            {
                throw new BizException("CHK023");
            }
            var bopbase = _repBopBase.GetBopBaseByWorkflowId(wobase.Workflow.Id);
            if (bopbase == null)
            {
                throw new BizException("CHK024");
            }
            var bopProcessBom = _repBopProcessBom.GetBopProcessBom(bopbase.Id);
            if (bopProcessBom == null)
            {
                throw new BizException("CHK025");
            }
            var woBom = _repWoBom.GetWoBoms(wobase.Id).ToList();
            if (woBom == null)
            {
                throw new BizException("CHK026");
            }            
            var workflow = _repWorkflow.GetWorkflow(wobase.Workflow.Id);
            if (workflow == null)
            {
                throw new BizException("CHK027");
            }            
            var wofs = _repWoFs.GetWoFs(wo).OrderByDescending(p => p.Id).ToList().FirstOrDefault();
            // if (wofs == null)
            // {
            //     throw new BizException("CHK028");
            // }
            var bop = new Bop(wo)
            {
            };
            bop.BopBom = bopProcessBom;
            bop.WoBom = woBom;
            bop.Workflow = workflow;
            bop.Fs = wofs;

            return bop;
        }

        public Bop GetBopByWoAndEq(string wo, int equipmentId)
        {
            var wofs = _repWoFs.GetWoFsByWoAndEq(wo, equipmentId).OrderByDescending(p => p.Id).ToList().FirstOrDefault();

            var bop = new Bop(wo)
            {
            };

            bop.Fs = wofs;
            return bop;
        }
    }

}
