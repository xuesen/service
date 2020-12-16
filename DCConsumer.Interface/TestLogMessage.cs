using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Consumer.Interface
{
    public class CustomJsonNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var result = new StringBuilder();
            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (i == 0)
                {
                    result.Append(char.ToLower(c));
                }
                else
                {
                    if (char.IsUpper(c))
                    {
                        result.Append('_');
                        result.Append(char.ToLower(c));
                    }
                    else
                    {
                        result.Append(c);
                    }
                }
            }
            return result.ToString();
        }
    }
    public class DataPayload
    {
        public string FileDate { get; set; }
        public long FileTime { get; set; }
        public string Folder { get; set; }
        public string Pack { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public long ModifyTime { get; set; }
        public string Content { get; set; }
        public bool Compress { get; set; }
        public long CompressSize { get; set; }
        public string Checksum { get; set; }
        public string Host { get; set; }
    }

    public class SchemaFiled
    {
        public string Field { get; set; }
        public string Type { get; set; }
        public bool Optional { get; set; }
    }

    public class DataSchema
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<SchemaFiled> Fields { get; set; }
    }

    public class DataMessage
    {
        public DataPayload Payload { get; set; }
        public DataSchema Schema { get; set; }
        public DataMessage()
        {
            Schema = new DataSchema()
            {
                Name = "dcagent_value",
                Type = "struct",
                Fields = new List<SchemaFiled>()
                    {
                        new SchemaFiled(){Field = "file_date", Type = "string", Optional = true},
                        new SchemaFiled(){Field = "file_time", Type =  "int64", Optional = true},
                        new SchemaFiled(){Field = "folder", Type = "string", Optional = true},
                        new SchemaFiled(){Field = "pack", Type = "string", Optional = true},
                        new SchemaFiled(){Field = "name", Type = "string", Optional = true},
                        new SchemaFiled(){Field = "size", Type =  "int64", Optional = true},
                        new SchemaFiled(){Field = "modify_time", Type =  "int64", Optional = true},
                        new SchemaFiled(){Field = "content", Type = "string", Optional = true},
                        new SchemaFiled(){Field = "compress", Type = "boolean", Optional = true},
                        new SchemaFiled(){Field = "compress_size", Type = "int64", Optional = true},
                        new SchemaFiled(){Field = "checksum", Type = "string", Optional = true},
                        new SchemaFiled(){Field = "host", Type = "string", Optional = true},
                    }
            };
        }
    }
}