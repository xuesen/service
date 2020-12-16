using System.Data.Common;
using System.Linq;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using IIMes.Infrastructure.Data.Interface;
using Consumer.TestLog.DomainObject;
using Consumer.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.TestLog.Consumer
{
    public class TestLogConsumer : BaseKafkaConsumer
    {
        private IRepository<DomainObject.TestLog> _repTestLog;
        private IRepository<DomainObject.TestDefect> _repTestDefect;

        public TestLogConsumer(
                               ILogger<TestLogConsumer> logger,
                               IOptions<KafkaConsumerOptions> options,
                               IServiceProvider sp
                               //    IRepository<DomainObject.TestLog> repTestLog,
                               //    IRepository<DomainObject.TestDefect> repTestDefect
                               ) : base(logger, options, sp)
        {
            // _repTestLog = repTestLog;
            // _repTestDefect = repTestDefect;
        }
        public override void DoConsume(string message)
        {
            try
            {
                using (var scope = _sp.CreateScope())
                {
                    _repTestLog = scope.ServiceProvider.GetService<IRepository<DomainObject.TestLog>>();
                    _repTestDefect = scope.ServiceProvider.GetService<IRepository<DomainObject.TestDefect>>();
                    ParseAoiCSV(message);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, string.Empty);
            }
        }

        private void ParseAoiCSV(string message)
        {
            /*Sample:
                PCB编号,产品名称,面,测试结果,机器编号,条码,PCB Ng 总数,误判Ng数量,确认NG数量,操作员,班次,线别,元件数量,屏蔽,子板编号,维修,工单,大板条码,关联条码,关联条码类型,PcName,AoiTestTime,AoiResult,NumberTime,TrackId,AppendMOnce,BlockQty,PCBTestDate,PCBTestTime
                2020-07-13 10:35:20.000,13352-AA,A,NG,7120,20200713 103520,2,1,1,admin,,,612,10,0,False,,20200713103520,,,ALD7032-PC,6.25,NG,20200713103520,A,,1,2020-07-13,10:35:20

                PCB编号,元件编号,元件位置,角度,X坐标,Y坐标,标准名称,料号,元件名称,初判NG,确认NG,Result,NeedYJPos,PcbBarcode,BigPCBCode,BlockId,AoiResult,PackageName,FalseCall
                2020-07-13 10:35:20.000,13,R0001,0,253.0266,43.974,,R0805-001,R0603-010,虚焊,缺件,NG,,20200713 103520,20200713103520,0,NG,R0805,NG
                2020-07-13 10:35:20.000,17,R0002,0,251.5892,42.9287,,R0402-001,R0201-004,多件,,OK,,20200713 103520,20200713103520,0,NG,R0201,False
            */
            using (StringReader sr = new StringReader(message))
            {
                string line = "";
                int index = 0;
                char separator = ',';
                DateTime testTime = DateTime.MinValue;
                int logId = 0;
                DomainObject.TestLog log = new DomainObject.TestLog();

                //逐行读取字符串的内容
                while ((line = sr.ReadLine()) != null)
                {
                    if (index == 1) //测试结果主记录
                    {
                        string[] arrLine = line.Split(separator);
                        testTime = Convert.ToDateTime(arrLine[27] + " " + arrLine[28]); //PCBTestDate,PCBTestTime

                        log.Type = "AOI";
                        log.Model = arrLine[1]; //产品名称
                        log.BigPcbBarcode = arrLine[17]; //大板条码
                        log.Side = arrLine[2]; //面
                        log.Result = arrLine[3]; //测试结果
                        log.EquipmentCode = arrLine[4]; //机器编号
                        log.ShiftCode = arrLine[10]; //班次
                        log.ProductLineCode = arrLine[11]; //线别
                        log.PcName = arrLine[20]; //PcName
                        log.TestTime = testTime;
                        log.Editor = arrLine[9]; //操作员

                        logId = AddTestLog(log);
                    }
                    else if (index > 3) //测试结果明细记录
                    {
                        if (log.Result == "NG")
                        {
                            string[] arrLine = line.Split(separator);
                            if (arrLine[11] == "NG") //Result
                            {
                                TestDefect defect = new TestDefect();
                                defect.LogId = logId;
                                defect.Type = log.Type;
                                defect.PartNo = arrLine[7]; //料号
                                defect.ComponentCode = arrLine[1]; //元件编号
                                defect.ComponentName = arrLine[8]; //元件名称
                                defect.ComponentLocation = arrLine[2]; //元件位置
                                defect.Defect = arrLine[10]; //确认NG
                                defect.TestTime = log.TestTime;
                                defect.Editor = string.Empty;

                                AddTestDefect(defect);
                            }
                        }
                    }
                    index++;
                }
                sr.Close();
            }
        }

        private int AddTestLog(DomainObject.TestLog log)
        {
            var id = _repTestLog.Add(log);
            return (int)id;
        }

        private void AddTestDefect(TestDefect defect)
        {
            _repTestDefect.Add(defect);
        }
    }
}
