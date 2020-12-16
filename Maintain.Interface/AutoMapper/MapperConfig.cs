using AutoMapper;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;

namespace Maintain.Interface.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SFactory, FactoryDTO>()
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<FactoryDTO, SFactory>()
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SWorkshop, WorkShopDTO>()
                    .ForPath(dto => dto.Factory.Id, (map) => map.MapFrom(m => m.SFactory.Id))
                    .ForPath(dto => dto.Factory.Name, (map) => map.MapFrom(m => m.SFactory.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<WorkShopDTO, SWorkshop>()
                    .ForPath(map => map.SFactory.Id, (dto) => dto.MapFrom(m => m.Factory.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SProductLine, PdLineDTO>()
                    .ForPath(dto => dto.Workshop.Id, (map) => map.MapFrom(m => m.SWorkshop.Id))
                    .ForPath(dto => dto.Workshop.Name, (map) => map.MapFrom(m => m.SWorkshop.Name))
                    .ForPath(dto => dto.Workshop.Factory.Id, (map) => map.MapFrom(m => m.SWorkshop.SFactory.Id))
                    .ForPath(dto => dto.Workshop.Factory.Name, (map) => map.MapFrom(m => m.SWorkshop.SFactory.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<PdLineDTO, SProductLine>()
                    .ForPath(map => map.SWorkshop.Id, (dto) => dto.MapFrom(m => m.Workshop.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            CreateMap<STerminal, TerminalDTO>()
                    .ForPath(dto => dto.Pdline.Id, (map) => map.MapFrom(m => m.SProductLine.Id))
                    .ForPath(dto => dto.Pdline.Name, (map) => map.MapFrom(m => m.SProductLine.Name))
                    .ForPath(dto => dto.Pdline.Workshop.Id, (map) => map.MapFrom(m => m.SProductLine.SWorkshop.Id))
                    .ForPath(dto => dto.Pdline.Workshop.Name, (map) => map.MapFrom(m => m.SProductLine.SWorkshop.Name))
                    .ForPath(dto => dto.Pdline.Workshop.Factory.Id, (map) => map.MapFrom(m => m.SProductLine.SWorkshop.SFactory.Id))
                    .ForPath(dto => dto.Pdline.Workshop.Factory.Name, (map) => map.MapFrom(m => m.SProductLine.SWorkshop.SFactory.Name))
                    .ForPath(dto => dto.Process.Id, (map) => map.MapFrom(m => m.SProcess.Id))
                    .ForPath(dto => dto.Process.Code, (map) => map.MapFrom(m => m.SProcess.Code))
                    .ForPath(dto => dto.Process.Name, (map) => map.MapFrom(m => m.SProcess.Name))
                    .ForPath(dto => dto.Process.Type.Id, (map) => map.MapFrom(m => m.SProcess.SProcessType.Id))
                    .ForPath(dto => dto.Process.Type.Name, (map) => map.MapFrom(m => m.SProcess.SProcessType.Name))
                    .ForPath(dto => dto.Equipment.Id, (map) => map.MapFrom(m => m.SEquipment.Id))
                    .ForPath(dto => dto.Equipment.Name, (map) => map.MapFrom(m => m.SEquipment.Name))
                    .ForPath(dto => dto.Equipment.Type.Id, (map) => map.MapFrom(m => m.SEquipment.SEquipmentType.Id))
                    .ForPath(dto => dto.Equipment.Type.Name, (map) => map.MapFrom(m => m.SEquipment.SEquipmentType.Name))
                    .ForPath(dto => dto.Equipment.Type.Catagory.Id, (map) => map.MapFrom(m => m.SEquipment.SEquipmentType.SEquipmentCatagory.Id))
                    .ForPath(dto => dto.Equipment.Type.Catagory.Name, (map) => map.MapFrom(m => m.SEquipment.SEquipmentType.SEquipmentCatagory.Name))
                    .ForPath(dto => dto.Equipment.Type.Catagory.Setting.Id, (map) => map.MapFrom(m => m.SEquipment.SEquipmentType.SEquipmentCatagory.SSetting.Id))
                    .ForPath(dto => dto.Equipment.Type.Catagory.Setting.Name, (map) => map.MapFrom(m => m.SEquipment.SEquipmentType.SEquipmentCatagory.SSetting.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<TerminalDTO, STerminal>()
                    .ForPath(map => map.SProductLine.Id, (dto) => dto.MapFrom(m => m.Pdline.Id))
                    .ForPath(map => map.SProductLine.SWorkshop.Id, (dto) => dto.MapFrom(m => m.Pdline.Workshop.Id))
                    .ForPath(map => map.SProductLine.SWorkshop.SFactory.Id, (dto) => dto.MapFrom(m => m.Pdline.Workshop.Factory.Id))
                    .ForPath(map => map.SProcess.Id, (dto) => dto.MapFrom(m => m.Process.Id))
                    .ForPath(map => map.SProcess.SProcessType.Id, (dto) => dto.MapFrom(m => m.Process.Type.Id))
                    .ForPath(map => map.SEquipment.Id, (dto) => dto.MapFrom(m => m.Equipment.Id))
                    .ForPath(map => map.SEquipment.SEquipmentType.Id, (dto) => dto.MapFrom(m => m.Equipment.Type.Id))
                    .ForPath(map => map.SEquipment.SEquipmentType.SEquipmentCatagory.Id, (dto) => dto.MapFrom(m => m.Equipment.Type.Catagory.Id))
                    .ForPath(map => map.SEquipment.SEquipmentType.SEquipmentCatagory.SSetting.Id, (dto) => dto.MapFrom(m => m.Equipment.Type.Catagory.Setting.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SFamily, FamilyDTO>()
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<FamilyDTO, SFamily>()
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SProcess, ProcessDTO>()
                    .ForPath(dto => dto.Type.Id, (map) => map.MapFrom(m => m.SProcessType.Id))
                    .ForPath(dto => dto.Type.Name, (map) => map.MapFrom(m => m.SProcessType.Name))
                    .ForPath(dto => dto.Stage.Id, (map) => map.MapFrom(m => m.SStage.Id))
                    .ForPath(dto => dto.Stage.Name, (map) => map.MapFrom(m => m.SStage.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<ProcessDTO, SProcess>()
                    .ForPath(map => map.SProcessType.Id, (dto) => dto.MapFrom(m => m.Type.Id))
                    .ForPath(map => map.SStage.Id, (dto) => dto.MapFrom(m => m.Stage.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SProcessType, ProcessTypeDTO>()
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<ProcessTypeDTO, SProcessType>()
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SStage, StageDTO>()
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<StageDTO, SStage>()
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SEquipmentCatagory, EquipmentCatagoryDTO>()
                    .ForPath(dto => dto.Setting.Id, (map) => map.MapFrom(m => m.SSetting.Id))
                    .ForPath(dto => dto.Setting.Name, (map) => map.MapFrom(m => m.SSetting.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<EquipmentCatagoryDTO, SEquipmentCatagory>()
                    .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.Setting.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SEquipmentType, EquipmentTypeDTO>()
                    .ForPath(dto => dto.Catagory.Id, (map) => map.MapFrom(m => m.SEquipmentCatagory.Id))
                    .ForPath(dto => dto.Catagory.Name, (map) => map.MapFrom(m => m.SEquipmentCatagory.Name))
                    .ForPath(dto => dto.Catagory.Setting.Id, (map) => map.MapFrom(m => m.SEquipmentCatagory.SSetting.Id))
                    .ForPath(dto => dto.Catagory.Setting.Name, (map) => map.MapFrom(m => m.SEquipmentCatagory.SSetting.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<EquipmentTypeDTO, SEquipmentType>()
                    .ForPath(map => map.SEquipmentCatagory.Id, (dto) => dto.MapFrom(m => m.Catagory.Id))
                    .ForPath(map => map.SEquipmentCatagory.SSetting.Id, (dto) => dto.MapFrom(m => m.Catagory.Setting.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            CreateMap<SEquipment, EquipmentDTO>()
                    .ForPath(dto => dto.Type.Id, (map) => map.MapFrom(m => m.SEquipmentType.Id))
                    .ForPath(dto => dto.Type.Name, (map) => map.MapFrom(m => m.SEquipmentType.Name))
                    .ForPath(dto => dto.Type.Catagory.Id, (map) => map.MapFrom(m => m.SEquipmentType.SEquipmentCatagory.Id))
                    .ForPath(dto => dto.Type.Catagory.Name, (map) => map.MapFrom(m => m.SEquipmentType.SEquipmentCatagory.Name))
                    .ForPath(dto => dto.Type.Catagory.Setting.Id, (map) => map.MapFrom(m => m.SEquipmentType.SEquipmentCatagory.SSetting.Id))
                    .ForPath(dto => dto.Type.Catagory.Setting.Name, (map) => map.MapFrom(m => m.SEquipmentType.SEquipmentCatagory.SSetting.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<EquipmentDTO, SEquipment>()
                    .ForPath(map => map.SEquipmentType.Id, (dto) => dto.MapFrom(m => m.Type.Id))
                    .ForPath(map => map.SEquipmentType.SEquipmentCatagory.Id, (dto) => dto.MapFrom(m => m.Type.Catagory.Id))
                    .ForPath(map => map.SEquipmentType.SEquipmentCatagory.SSetting.Id, (dto) => dto.MapFrom(m => m.Type.Catagory.Setting.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));

            // 系统设定表
            CreateMap<SSettingDTO, SSetting>();
            CreateMap<SSetting, SSettingDTO>();
            // 抽检计划
            CreateMap<SSamplingPlan, SSamplingPlanDTO>()
                    .ForMember(dto => dto.TypeName, (map) => map.MapFrom(m => m.SSetting.Name))
                    .ForMember(dto => dto.Type, (map) => map.MapFrom(m => m.SSetting.Id));
            CreateMap<SSamplingPlanDTO, SSamplingPlan>()
                    .ForPath(map => map.SSetting.Name, (dto) => dto.MapFrom(m => m.TypeName))
                    .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.Type));
            // 抽检计划细节
            CreateMap<SSamplingPlanDetail, SSamplingPlanDetailDTO>()
                    .ForMember(dto => dto.SamplingPlanName, (map) => map.MapFrom(m => m.SSamplingPlan.Name))
                    .ForMember(dto => dto.SamplingPlanId, (map) => map.MapFrom(m => m.SSamplingPlan.Id));
            CreateMap<SSamplingPlanDetailDTO, SSamplingPlanDetail>()
                    .ForPath(map => map.SSamplingPlan.Name, (dto) => dto.MapFrom(m => m.SamplingPlanName))
                    .ForPath(map => map.SSamplingPlan.Id, (dto) => dto.MapFrom(m => m.SamplingPlanId));
            // 抽检规则
            CreateMap<SSamplingRule, SSamplingRuleDTO>()
                    .ForMember(dto => dto.TypeName, (map) => map.MapFrom(m => m.SSetting.Name))
                    .ForMember(dto => dto.Type, (map) => map.MapFrom(m => m.SSetting.Id));
            CreateMap<SSamplingRuleDTO, SSamplingRule>()
                    .ForPath(map => map.SSetting.Name, (dto) => dto.MapFrom(m => m.TypeName))
                    .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.Type));
            // 抽检规则细节
            CreateMap<SSamplingRuleDetail, SSamplingRuleDetailDTO>()
                    .ForMember(dto => dto.ActionName, (map) => map.MapFrom(m => m.SSetting.Name))
                    .ForMember(dto => dto.Action, (map) => map.MapFrom(m => m.SSetting.Id))
                    .ForMember(dto => dto.SamplingRuleName, (map) => map.MapFrom(m => m.SSamplingRule.Name))
                    .ForMember(dto => dto.SamplingRuleId, (map) => map.MapFrom(m => m.SSamplingRule.Id))
                    .ForMember(dto => dto.FromSamplePlanName, (map) => map.MapFrom(m => m.SSamplingPlanFrom.Name))
                    .ForMember(dto => dto.FromSamplePlanId, (map) => map.MapFrom(m => m.SSamplingPlanFrom.Id))
                    .ForMember(dto => dto.ToSamplePlanName, (map) => map.MapFrom(m => m.SSamplingPlanTo.Name))
                    .ForMember(dto => dto.ToSamplePlanId, (map) => map.MapFrom(m => m.SSamplingPlanTo.Id));
            CreateMap<SSamplingRuleDetailDTO, SSamplingRuleDetail>()
                    .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.Action))
                    .ForPath(map => map.SSamplingRule.Id, (dto) => dto.MapFrom(m => m.SamplingRuleId))
                    .ForPath(map => map.SSamplingPlanFrom.Id, (dto) => dto.MapFrom(m => m.FromSamplePlanId))
                    .ForPath(map => map.SSamplingPlanTo.Id, (dto) => dto.MapFrom(m => m.ToSamplePlanId));
            // 编码规则
            CreateMap<SBarcodeRule, SBarcodeRuleDTO>()
                        .ForMember(dto => dto.RuleTypeName, (map) => map.MapFrom(m => m.SSetting.Name))
                        .ForMember(dto => dto.RuleType, (map) => map.MapFrom(m => m.SSetting.Id));
            CreateMap<SBarcodeRuleDTO, SBarcodeRule>()
                        .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.RuleType));

            // 打印模板设定
            CreateMap<SPrintTemplate, SPrintTemplateDTO>()
                        .ForMember(dto => dto.PrintKeyName, (map) => map.MapFrom(m => m.SSetting.Name))
                        .ForMember(dto => dto.PrintKey, (map) => map.MapFrom(m => m.SSetting.Id));
            CreateMap<SPrintTemplateDTO, SPrintTemplate>()
                        .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.PrintKey))
                        .ForPath(map => map.SSetting.Name, (dto) => dto.MapFrom(m => m.PrintKeyName));
            // 检查大项
            CreateMap<STestItemType, STestItemTypeDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<STestItemTypeDTO, STestItemType>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 检查小项
            CreateMap<STestItem, STestItemDTO>()
                        .ForPath(dto => dto.NumType.Id, (map) => map.MapFrom(m => m.SSetting.Id))
                        .ForPath(dto => dto.NumType.Name, (map) => map.MapFrom(m => m.SSetting.Name))
                        .ForPath(dto => dto.TestItemType.Id, (map) => map.MapFrom(m => m.STestItemType.Id))
                        .ForPath(dto => dto.TestItemType.Name, (map) => map.MapFrom(m => m.STestItemType.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<STestItemDTO, STestItem>()
                        .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.NumType.Id))
                        .ForPath(map => map.STestItemType.Id, (dto) => dto.MapFrom(m => m.TestItemType.Id))
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 不良类型
            CreateMap<SSymptomType, SSymptomTypeDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<SSymptomTypeDTO, SSymptomType>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 不良现象
            CreateMap<SSymptom, SSymptomDTO>()
                        .ForPath(dto => dto.Setting.Id, (map) => map.MapFrom(m => m.SSetting.Id))
                        .ForPath(dto => dto.Setting.Name, (map) => map.MapFrom(m => m.SSetting.Name))
                        .ForPath(dto => dto.SymptomType.Id, (map) => map.MapFrom(m => m.SSymptomType.Id))
                        .ForPath(dto => dto.SymptomType.Code, (map) => map.MapFrom(m => m.SSymptomType.Code))
                        .ForPath(dto => dto.SymptomType.Name, (map) => map.MapFrom(m => m.SSymptomType.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<SSymptomDTO, SSymptom>()
                        .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.Setting.Id))
                        .ForPath(map => map.SSymptomType.Id, (dto) => dto.MapFrom(m => m.SymptomType.Id))
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 不良原因
            CreateMap<SCause, SCauseDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<SCauseDTO, SCause>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 责任类别
            CreateMap<SDuty, SDutyDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<SDutyDTO, SDuty>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 维修方式
            CreateMap<SRepairType, SRepairTypeDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<SRepairTypeDTO, SRepairType>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 班别群组
            CreateMap<SShiftGroup, ShiftGroupDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<ShiftGroupDTO, SShiftGroup>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 班别
            CreateMap<SShift, ShiftDTO>()
                        .ForPath(dto => dto.ShiftGroup.Id, (map) => map.MapFrom(m => m.SShiftGroup.Id))
                        .ForPath(dto => dto.ShiftGroup.Name, (map) => map.MapFrom(m => m.SShiftGroup.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<ShiftDTO, SShift>()
                        .ForPath(map => map.SShiftGroup.Id, (dto) => dto.MapFrom(m => m.ShiftGroup.Id))
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 部门
            CreateMap<SDepartment, DepartmentDTO>()
                        .ForPath(dto => dto.SDepartment.Id, (map) => map.MapFrom(m => m.Department.Id))
                        .ForPath(dto => dto.SDepartment.Name, (map) => map.MapFrom(m => m.Department.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<DepartmentDTO, SDepartment>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code))
                        .ForMember(map => map.Department, opt =>
                        {
                            opt.PreCondition(dto => (dto.SDepartment != null));
                            opt.MapFrom(dto => dto.SDepartment);
                        });
            CreateMap<LayerDepartmentDTO, SDepartment>();
            // 员工
            CreateMap<SEmployee, EmployeeDTO>()
                        .ForPath(dto => dto.SDepartment.Id, (map) => map.MapFrom(m => m.SDepartment.Id))
                        .ForPath(dto => dto.SDepartment.Name, (map) => map.MapFrom(m => m.SDepartment.Name))
                        .ForPath(dto => dto.SEmployee.Id, (map) => map.MapFrom(m => m.Employee.Id))
                        .ForPath(dto => dto.SEmployee.Name, (map) => map.MapFrom(m => m.Employee.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<EmployeeDTO, SEmployee>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code))
                        .ForMember(map => map.Employee, opt =>
                        {
                            opt.PreCondition(dto => (dto.SEmployee != null));
                            opt.MapFrom(dto => dto.SEmployee);
                        });
            CreateMap<LayerEmployeeDTO, SEmployee>();
            // 物料
            CreateMap<SPart, SPartDTO>()
                        .ForPath(dto => dto.Model.Id, (map) => map.MapFrom(m => m.SFamily.Id))
                        .ForPath(dto => dto.Model.Code, (map) => map.MapFrom(m => m.SFamily.Code))
                        .ForPath(dto => dto.Model.Name, (map) => map.MapFrom(m => m.SFamily.Name))
                        .ForPath(dto => dto.Vendor.Id, (map) => map.MapFrom(m => m.SVendor.Id))
                        .ForPath(dto => dto.Vendor.Code, (map) => map.MapFrom(m => m.SVendor.Code))
                        .ForPath(dto => dto.Vendor.Name, (map) => map.MapFrom(m => m.SVendor.Name))
                        .ForPath(dto => dto.Customer.Id, (map) => map.MapFrom(m => m.SCustomer.Id))
                        .ForPath(dto => dto.Customer.Code, (map) => map.MapFrom(m => m.SCustomer.Code))
                        .ForPath(dto => dto.Customer.Name, (map) => map.MapFrom(m => m.SCustomer.Name))
                        .ForPath(dto => dto.MaterialType.Id, (map) => map.MapFrom(m => m.SSetting.Id))
                        .ForPath(dto => dto.MaterialType.Code, (map) => map.MapFrom(m => m.SSetting.Code))
                        .ForPath(dto => dto.MaterialType.Name, (map) => map.MapFrom(m => m.SSetting.Name));
            CreateMap<SPartDTO, SPart>()
                        .ForPath(map => map.SFamily.Id, (dto) => dto.MapFrom(m => m.Model.Id))
                        .ForPath(map => map.SVendor.Id, (dto) => dto.MapFrom(m => m.Vendor.Id))
                        .ForPath(map => map.SCustomer.Id, (dto) => dto.MapFrom(m => m.Customer.Id))
                        .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.MaterialType.Id));
            // 工艺路线
            CreateMap<SWorkflow, SWorkflowDTO>()
                        .ForPath(dto => dto.WorkflowBaseId, (map) => map.MapFrom(m => m.SWorkflowBase.Id));
            CreateMap<SWorkflowDTO, SWorkflow>()
                        .ForPath(map => map.SWorkflowBase.Id, (dto) => dto.MapFrom(m => m.WorkflowBaseId));

            CreateMap<SWorkflowBase, SWorkflowBaseDTO>()
                        .ForPath(dto => dto.PartId, (map) => map.MapFrom(m => m.SPart.Id))
                        .ForPath(dto => dto.FamilyId, (map) => map.MapFrom(m => m.SFamily.Id));
            CreateMap<SWorkflowBaseDTO, SWorkflowBase>()
                        .ForPath(map => map.SPart.Id, (dto) => dto.MapFrom(m => m.PartId))
                        .ForPath(map => map.SFamily.Id, (dto) => dto.MapFrom(m => m.FamilyId));

            // 工艺清单
            CreateMap<SBopBase, SBopBaseDTO>()
                .ForPath(dto => dto.Lastestedittime, (map) => map.MapFrom(m => m.Udt))
                .ForPath(dto => dto.Workflowid, (map) => map.MapFrom(m => m.SWorkflow.Id))
                .ForPath(dto => dto.Workflowlastversion, (map) => map.MapFrom(m => m.SWorkflow.Version))
                .ForPath(dto => dto.WorkflowBasename, (map) => map.MapFrom(m => m.SWorkflow.SWorkflowBase.Name))
                .ForPath(dto => dto.Part_id, (map) => map.MapFrom(m => m.SPart.Id))
                .ForPath(dto => dto.Part_no, (map) => map.MapFrom(m => m.SPart.PartNo))
                .ForPath(dto => dto.Part_name, (map) => map.MapFrom(m => m.SPart.PartName))
                .ForPath(dto => dto.Family_id, (map) => map.MapFrom(m => m.SFamily.Id))
                .ForPath(dto => dto.Family_code, (map) => map.MapFrom(m => m.SFamily.Code))
                .ForPath(dto => dto.Family_name, (map) => map.MapFrom(m => m.SFamily.Name));
            CreateMap<SBopBaseDTO, SBopBase>()
                .ForPath(map => map.SPart, (dto) => dto.MapFrom(m => m.Part))
                .ForPath(map => map.SFamily.Id, (dto) => dto.MapFrom(m => m.Family_id))
                .ForPath(map => map.SWorkflow.Id, (dto) => dto.MapFrom(m => m.Workflowid))
                .ForPath(map => map.WorkflowVersion, (dto) => dto.MapFrom(m => m.Workflowlastversion));

            // BOM
            CreateMap<SBom, SBomDTO>();
            CreateMap<SBomDTO, SBom>();

            // 工序下 物料清单
            CreateMap<SBopProcessBom, SBopProcessBomDTO>()
                .ForPath(dto => dto.BopBase.Id, (map) => map.MapFrom(m => m.BopBase.Id))
                .ForPath(dto => dto.BopBase.WorkflowVersion, (map) => map.MapFrom(m => m.BopBase.WorkflowVersion))
                .ForPath(dto => dto.Process.Id, (map) => map.MapFrom(m => m.Process.Id))
                .ForPath(dto => dto.Process.Name, (map) => map.MapFrom(m => m.Process.Name))
                .ForPath(dto => dto.ItemPart.Id, (map) => map.MapFrom(m => m.ItemPart.Id))
                .ForPath(dto => dto.ItemPart.PartName, (map) => map.MapFrom(m => m.ItemPart.PartName));
            CreateMap<SBopProcessBomDTO, SBopProcessBom>()
                .ForPath(map => map.BopBase.Id, (dto) => dto.MapFrom(m => m.BopBase.Id))
                .ForPath(map => map.Process.Id, (dto) => dto.MapFrom(m => m.Process.Id))
                .ForPath(map => map.ItemPart.Id, (dto) => dto.MapFrom(m => m.ItemPart.Id));
            // 工序下 质检清单(质检项)
            CreateMap<SBopProcessTestitem, SBopProcessTestitemDTO>()
                .ForPath(dto => dto.TestItem.Id, (map) => map.MapFrom(m => m.STestItem.Id))
                .ForPath(dto => dto.TestItem.Code, (map) => map.MapFrom(m => m.STestItem.Code))
                .ForPath(dto => dto.TestItem.Name, (map) => map.MapFrom(m => m.STestItem.Name))
                .ForPath(dto => dto.TestItem.Point, (map) => map.MapFrom(m => m.STestItem.Point))
                .ForPath(dto => dto.TestItem.Ratio, (map) => map.MapFrom(m => m.STestItem.Ratio))
                .ForPath(dto => dto.TestItem.TestItemType.Id, (map) => map.MapFrom(m => m.STestItem.STestItemType.Id))
                .ForPath(dto => dto.TestItem.TestItemType.Name, (map) => map.MapFrom(m => m.STestItem.STestItemType.Name))
                .ForPath(dto => dto.TestItem.TestItemType.Code, (map) => map.MapFrom(m => m.STestItem.STestItemType.Code));
            CreateMap<SBopProcessTestitemDTO, SBopProcessTestitem>()
                .ForPath(map => map.STestItem.Id, (dto) => dto.MapFrom(m => m.TestItem.Id))
                .ForPath(map => map.STestItem.STestItemType.Id, (dto) => dto.MapFrom(m => m.TestItem.TestItemType.Id));
            // 工序下 质检清单(抽检计划)
            CreateMap<SBopProcessSampling, SBopProcessSamplingDTO>()
                .ForPath(dto => dto.BopBase.Id, (map) => map.MapFrom(m => m.SBopBase.Id))
                .ForPath(dto => dto.SamplingPlan, (map) => map.MapFrom(m => m.SSamplingPlan))
                .ForPath(dto => dto.SamplingRule, (map) => map.MapFrom(m => m.SSamplingRule));
            CreateMap<SBopProcessSamplingDTO, SBopProcessSampling>()
                .ForPath(map => map.SBopBase.Id, (dto) => dto.MapFrom(m => m.BopBase.Id))
                .ForPath(map => map.SSamplingPlan, (dto) => dto.MapFrom(m => m.SamplingPlan))
                .ForPath(map => map.SSamplingRule, (dto) => dto.MapFrom(m => m.SamplingRule));
            // 工序下 打印规则
            CreateMap<SBopProcessLabel, SBopProcessLabelDTO>()
                .ForPath(dto => dto.TriggerId, (map) => map.MapFrom(m => m.SSetting.Id))
                .ForPath(dto => dto.TriggerName, (map) => map.MapFrom(m => m.SSetting.Name))
                .ForPath(dto => dto.PrintTemplate.Id, (map) => map.MapFrom(m => m.PrintTemplate.Id))
                .ForPath(dto => dto.PrintTemplate.Name, (map) => map.MapFrom(m => m.PrintTemplate.Name));
            CreateMap<SBopProcessLabelDTO, SBopProcessLabel>()
                .ForPath(map => map.SSetting.Id, (dto) => dto.MapFrom(m => m.TriggerId))
                .ForPath(map => map.PrintTemplate, (dto) => dto.MapFrom(m => m.PrintTemplate));
            // 参数规格(序号规格)
            CreateMap<SBopSpec, SBopSpecDTO>()
                .ForPath(dto => dto.BopBaseId, (map) => map.MapFrom(m => m.SBopBase.Id))
                .ForPath(dto => dto.ParamValue.Id, (map) => map.MapFrom(m => m.ParamValue.Id))
                .ForPath(dto => dto.ParamValue.RuleName, (map) => map.MapFrom(m => m.ParamValue.RuleName));
            CreateMap<SBopSpecDTO, SBopSpec>()
                .ForPath(map => map.SBopBase.Id, (dto) => dto.MapFrom(m => m.BopBaseId))
                .ForPath(map => map.ParamValue, (dto) => dto.MapFrom(m => m.ParamValue));
            // 参数规格(包装规则)
            CreateMap<SPartPack, SPartPackDTO>()
                .ForPath(dto => dto.BoxBarcodeRule.Id, (map) => map.MapFrom(m => m.BoxBarcodeRule.Id))
                .ForPath(dto => dto.BoxBarcodeRule.RuleName, (map) => map.MapFrom(m => m.BoxBarcodeRule.RuleName))
                .ForPath(dto => dto.CartonBarcodeRule.Id, (map) => map.MapFrom(m => m.CartonBarcodeRule.Id))
                .ForPath(dto => dto.CartonBarcodeRule.RuleName, (map) => map.MapFrom(m => m.CartonBarcodeRule.RuleName))
                .ForPath(dto => dto.PalletBarcodeRule.Id, (map) => map.MapFrom(m => m.PalletBarcodeRule.Id))
                .ForPath(dto => dto.PalletBarcodeRule.RuleName, (map) => map.MapFrom(m => m.PalletBarcodeRule.RuleName));
            CreateMap<SPartPackDTO, SPartPack>()
                .ForPath(map => map.BoxBarcodeRule, (dto) => dto.MapFrom(m => m.BoxBarcodeRule))
                .ForPath(map => map.CartonBarcodeRule, (dto) => dto.MapFrom(m => m.CartonBarcodeRule))
                .ForPath(map => map.PalletBarcodeRule, (dto) => dto.MapFrom(m => m.PalletBarcodeRule));
            // 报废
            CreateMap<SScrapreason, ScrapReasonDTO>()
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<ScrapReasonDTO, SScrapreason>()
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 工单
            CreateMap<WoBase, WoBaseDTO>()
                    .ForPath(dto => dto.Part.Id, (map) => map.MapFrom(m => m.SPart.Id))
                    .ForPath(dto => dto.Part.No, (map) => map.MapFrom(m => m.SPart.PartNo))
                    .ForPath(dto => dto.Part.Name, (map) => map.MapFrom(m => m.SPart.PartName));
            CreateMap<WoBaseDTO, WoBase>();
            // 工单BOM
            CreateMap<WoBom, WoBomDTO>()
                    .ForPath(dto => dto.WoBase.Id, (map) => map.MapFrom(m => m.WoBase.Id))
                    .ForPath(dto => dto.WoBase.WorkOrder, (map) => map.MapFrom(m => m.WoBase.WorkOrder))
                    .ForPath(dto => dto.MainPart.Id, (map) => map.MapFrom(m => m.SMainPart.Id))
                    .ForPath(dto => dto.MainPart.No, (map) => map.MapFrom(m => m.SMainPart.PartNo))
                    .ForPath(dto => dto.MainPart.Name, (map) => map.MapFrom(m => m.SMainPart.PartName))
                    .ForPath(dto => dto.SubPart.Id, (map) => map.MapFrom(m => m.SSubPart.Id))
                    .ForPath(dto => dto.SubPart.No, (map) => map.MapFrom(m => m.SSubPart.PartNo))
                    .ForPath(dto => dto.SubPart.Name, (map) => map.MapFrom(m => m.SSubPart.PartName))
                    .ForPath(dto => dto.Process.Id, (map) => map.MapFrom(m => m.SProcess.Id))
                    .ForPath(dto => dto.Process.Name, (map) => map.MapFrom(m => m.SProcess.Name))
                    .ForPath(dto => dto.Process.Type.Id, (map) => map.MapFrom(m => m.SProcess.SProcessType.Id))
                    .ForPath(dto => dto.Process.Type.Name, (map) => map.MapFrom(m => m.SProcess.SProcessType.Name))
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<WoBomDTO, WoBom>()
                    .ForPath(map => map.WoBase.Id, (dto) => dto.MapFrom(m => m.WoBase.Id))
                    .ForPath(map => map.SMainPart.Id, (dto) => dto.MapFrom(m => m.MainPart.Id))
                    .ForPath(map => map.SSubPart.Id, (dto) => dto.MapFrom(m => m.SubPart.Id))
                    .ForPath(map => map.SSubPart.Version, (dto) => dto.MapFrom(m => m.Version))
                    .ForPath(map => map.SProcess.Id, (dto) => dto.MapFrom(m => m.Process.Id))
                    .ForPath(map => map.SProcess.SProcessType.Id, (dto) => dto.MapFrom(m => m.Process.Type.Id))
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            // 工单状态
            CreateMap<SWoStatus, SWoStatusDTO>()
                    .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                    .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<SWoStatusDTO, SWoStatus>()
                    .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
        }
    }
}